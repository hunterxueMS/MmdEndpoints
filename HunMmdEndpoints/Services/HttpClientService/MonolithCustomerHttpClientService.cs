namespace HunMmdEndpoints.Services.HttpClientService;

public class MonolithCustomerHttpClientService : BaseHttpClientService
{
    public MonolithCustomerHttpClientService(ILogger<MonolithCustomerHttpClientService> logger,
        HttpClient httpClient) : base(logger, httpClient) { }

    internal override List<string> GetSwaggerJsonPath()
    {
        return new List<string>()
        {
            "swagger/docs/v1.0/",
            "swagger/docs/v2.0/",
        };
    }
}
