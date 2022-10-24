namespace HunMmdEndpoints.Services.HttpClientService;

public abstract class BaseHttpClientService
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    public BaseHttpClientService(ILogger logger,
        HttpClient httpClient)
    {
        this._logger = logger;
        this._httpClient = httpClient;
    }

    internal abstract List<string> GetSwaggerJsonPath();

    public async Task<string[]?> GetSchemas()
    {
        var tasks = GetSwaggerJsonPath().Select(async (path) => await _httpClient.GetStringAsync(path));
        return await Task.WhenAll(tasks);
    }
}
