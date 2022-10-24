namespace HunMmdEndpoints.Services.HttpClientService;

public class OperationMicroserviceHttpClientService : BaseHttpClientService
{
    public OperationMicroserviceHttpClientService(ILogger<OperationMicroserviceHttpClientService> logger,
        HttpClient httpClient) : base(logger, httpClient) { }

    internal override List<string> GetSwaggerJsonPath()
    {
        return new List<string>()
        {
            "swagger/v1/swagger.json",
        };
    }
}

