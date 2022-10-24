namespace HunMmdEndpoints.Services.HttpClientService;

public class DeviceMicroserviceHttpClientService : BaseHttpClientService
{
    public DeviceMicroserviceHttpClientService(ILogger<DeviceMicroserviceHttpClientService> logger,
        HttpClient httpClient) : base(logger, httpClient) { }

    internal override List<string> GetSwaggerJsonPath()
    {
        return new List<string>()
        {
            "swagger/v1/swagger.json",
        };
    }
}
