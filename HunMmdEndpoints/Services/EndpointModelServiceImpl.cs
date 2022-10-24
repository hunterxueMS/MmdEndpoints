using HunMmdEndpoints.Exceptions;
using HunMmdEndpoints.Models;
using HunMmdEndpoints.Services.HttpClientService;
using HunMmdEndpoints.Utils;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HunMmdEndpoints.Services;

public class EndpointModelServiceImpl : IEndpointModelService
{
    private readonly ILogger<EndpointModelServiceImpl> _logger;
    private readonly TenantManagementMicroserviceHttpClientService _tenantManagementMicroserviceHttpClientService;
    private readonly OperationMicroserviceHttpClientService _operationMicroserviceHttpClientService;
    private readonly ArtMicroserviceHttpClientService _artMicroserviceHttpClientService;
    private readonly UmMicroserviceHttpClientService _umMicroserviceHttpClientService;
    private readonly DeviceMicroserviceHttpClientService _deviceMicroserviceHttpClientService;
    private readonly MonolithCustomerHttpClientService _monolithConsumerHttpClientService;
    private readonly MonolithPartnerHttpClientService _monolithPartnerHttpClientService;

    private readonly TimeSpan refreshInterval = TimeSpan.FromHours(1);

    // this Local Function builds options that will trigger a refresh of the cache entry immediately on expiry
    private MemoryCacheEntryOptions GetOptions()
    {
        //ensure the cache item expires exactly, (and not lazily on the next access)
        var options = new LazyCacheEntryOptions()
            .SetAbsoluteExpiration(refreshInterval, ExpirationMode.ImmediateEviction);

        // as soon as it expires, re-add it to the cache
        options.RegisterPostEvictionCallback((keyEvicted, value, reason, state) =>
        {
            // don't re-add if running out of memory or it was forcibly removed
            if (reason == EvictionReason.Expired || reason == EvictionReason.TokenExpired)
            {
                _appCache.GetOrAddAsync((string)keyEvicted,
                    async (_) =>
                    {
                        var newItem = await getEndpointModel((MMDServiceType)Enum.Parse(typeof(MMDServiceType), (string)keyEvicted));
                        if (newItem.Count() == 0) return value;
                        return newItem;
                    }, GetOptions()); //calls itself to get another set of options!
            }
        });
        return options;
    }

    private readonly IAppCache _appCache;

    public EndpointModelServiceImpl(
        ILogger<EndpointModelServiceImpl> logger,
        TenantManagementMicroserviceHttpClientService tenantManagementMicroserviceHttpClientService,
        DeviceMicroserviceHttpClientService deviceMicroserviceHttpClientService,
        MonolithCustomerHttpClientService monolithCustomerHttpClientService,
        MonolithPartnerHttpClientService monolithPartnerHttpClientService,
        OperationMicroserviceHttpClientService operationMicroserviceHttpClientService,
        ArtMicroserviceHttpClientService artMicroserviceHttpClientService,
        UmMicroserviceHttpClientService umMicroserviceHttpClientService,
        IAppCache appCache
        )

    {
        _logger = logger;
        _tenantManagementMicroserviceHttpClientService = tenantManagementMicroserviceHttpClientService;
        _deviceMicroserviceHttpClientService = deviceMicroserviceHttpClientService;
        _monolithConsumerHttpClientService = monolithCustomerHttpClientService;
        _monolithPartnerHttpClientService = monolithPartnerHttpClientService;
        _operationMicroserviceHttpClientService = operationMicroserviceHttpClientService;
        _artMicroserviceHttpClientService = artMicroserviceHttpClientService;
        _umMicroserviceHttpClientService = umMicroserviceHttpClientService;
        this._appCache = appCache;
    }

    internal async Task<string[]?> downloadSchemaFile(MMDServiceType serviceType)
    {
        _logger.LogInformation("downloadSchemaFile serviceType={serviceType}", serviceType);
        try
        {
            switch (serviceType)
            {
                case MMDServiceType.customer:
                    return await _monolithConsumerHttpClientService.GetSchemas();
                case MMDServiceType.partner:
                    return await _monolithPartnerHttpClientService.GetSchemas();
                case MMDServiceType.device:
                    return await _deviceMicroserviceHttpClientService.GetSchemas();
                case MMDServiceType.tm:
                    return await _tenantManagementMicroserviceHttpClientService.GetSchemas();
                case MMDServiceType.operation:
                    return await _operationMicroserviceHttpClientService.GetSchemas();
                case MMDServiceType.art:
                    return await _artMicroserviceHttpClientService.GetSchemas();
                case MMDServiceType.um:
                    return await _umMicroserviceHttpClientService.GetSchemas();
                default:
                    throw new ServiceTypeInvalidException("invalid schema type: " + serviceType.ToString());
            }
        }
        catch (ServiceTypeInvalidException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new SchemaDownloadFailureException("fail to download scheme for serivceType: " + serviceType.ToString(), e);
        }
    }

    private List<EndpointModelItem> parseEndpoints(string? schemaJson)
    {
        List<EndpointModelItem> res = new();
        if (schemaJson is null) return res;
        try
        {
            EndpointModelResp? model = JsonSerializer.Deserialize<EndpointModelResp>(schemaJson!);
            if (model is null) return res;
            if (model.RawPaths is null) return res;
            JsonElement rawPaths = (JsonElement)model.RawPaths;
            foreach (var rawPath in rawPaths.EnumerateObject())
            {
                string path = rawPath.Name;
                JsonElement methodItems = rawPath.Value;
                foreach (var methodItem in methodItems.EnumerateObject())
                {
                    string method = methodItem.Name;
                    JsonElement methodObj = methodItem.Value;
                    string operationId = methodObj.GetProperty("operationId").GetString()!;

                    string? summary = null;
                    if (methodObj.TryGetProperty("summary", out JsonElement summaryEl))
                    {
                        summary = summaryEl.GetString();
                    }
                    List<Parameter> parameters = new();
                    if (methodObj.TryGetProperty("parameters", out JsonElement parametersEl))
                    {
                        foreach (var parameterEl in parametersEl.EnumerateArray())
                        {
                            Parameter? p = null;
                            if (parameterEl.TryGetProperty("name", out JsonElement paramNameEl))
                            {
                                string? paramName = paramNameEl.GetString();
                                p = new Parameter(paramName!);
                            }
                            if (parameterEl.TryGetProperty("description", out JsonElement paramDescEl))
                            {
                                string? paramDesc = paramDescEl.GetString();
                                if (p != null) p.Desc = paramDesc;
                            }
                            if (p != null) parameters.Add(p);
                        }
                    }
                    EndpointModelItem item = new EndpointModelItem(path, operationId, method: method);
                    item.Summary = summary;
                    item.Parameters = parameters;
                    res.Add(item);
                }
            }
        }
        catch (Exception e)
        {
            throw new SchemaParseFailureException("fail to parse downloade schema", e);
        }
        return res;
    }


    private async Task<List<EndpointModelItem>> getEndpointModel(MMDServiceType serviceType)
    {
        List<EndpointModelItem> res = new();
        try
        {
            string[]? schemaStrs = await downloadSchemaFile(serviceType);
            if (schemaStrs is null || schemaStrs.Length == 0)
            {
                throw new SchemaDownloadFailureException("schema is empty, maybe download failed, serivceType:" + serviceType.ToString(), null);
            }
            foreach (string schemaStr in schemaStrs)
            {
                res.AddRange(parseEndpoints(schemaStr));
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "fail to getEndpointModel for service: {serviceType}", serviceType);
            res = new();
        }
        return res;
    }

    public async Task<List<EndpointModelItem>> getEndpoints(MMDServiceType serviceType, string? pathPart = null)
    {
        var res = await _appCache.GetOrAddAsync(serviceType.ToString(),
            () => getEndpointModel(serviceType),
            GetOptions());
        if (pathPart != null)
        {
            return (from item in res
                    where item.Path.Contains(pathPart, StringComparison.OrdinalIgnoreCase)
                    select item
                    )
                   .ToList();
        }
        return res;
    }
}
