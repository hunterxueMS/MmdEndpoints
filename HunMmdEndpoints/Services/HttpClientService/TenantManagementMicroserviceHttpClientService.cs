namespace HunMmdEndpoints.Services.HttpClientService;

public class TenantManagementMicroserviceHttpClientService : BaseHttpClientService
{
    public TenantManagementMicroserviceHttpClientService(ILogger<TenantManagementMicroserviceHttpClientService> logger,
        HttpClient httpClient) : base(logger, httpClient) { }

    internal override List<string> GetSwaggerJsonPath()
    {
        return new List<string>()
        {
            "swagger/v1/swagger.json",
        };
    }
}
