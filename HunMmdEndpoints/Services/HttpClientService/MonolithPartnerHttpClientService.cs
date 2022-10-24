namespace HunMmdEndpoints.Services.HttpClientService;

public class MonolithPartnerHttpClientService : BaseHttpClientService
{
    public MonolithPartnerHttpClientService(ILogger<MonolithPartnerHttpClientService> logger,
        HttpClient httpClient) : base(logger, httpClient) { }

    internal override List<string> GetSwaggerJsonPath()
    {
        return new List<string>()
        {
            "swagger/docs/v1.0/",
            //"swagger/docs/v2.0/",// swagger v2 is not exported in mWaaS
        };
    }
}
