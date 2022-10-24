namespace HunMmdEndpoints.Services.HttpClientService;

public class UmMicroserviceHttpClientService : BaseHttpClientService
{
    public UmMicroserviceHttpClientService(ILogger<UmMicroserviceHttpClientService> logger,
        HttpClient httpClient) : base(logger, httpClient) { }

    internal override List<string> GetSwaggerJsonPath()
    {
        return new List<string>()
        {
            "swagger/v1/swagger.json",
        };
    }
}
