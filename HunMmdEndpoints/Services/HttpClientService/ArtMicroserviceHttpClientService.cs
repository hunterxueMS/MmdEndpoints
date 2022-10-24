namespace HunMmdEndpoints.Services.HttpClientService;

public class ArtMicroserviceHttpClientService : BaseHttpClientService
{
    public ArtMicroserviceHttpClientService(ILogger<ArtMicroserviceHttpClientService> logger,
        HttpClient httpClient) : base(logger, httpClient) { }

    internal override List<string> GetSwaggerJsonPath()
    {
        return new List<string>()
        {
            "swagger/v1/swagger.json",
        };
    }
}

