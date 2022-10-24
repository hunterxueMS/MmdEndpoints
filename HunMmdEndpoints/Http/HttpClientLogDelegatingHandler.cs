namespace HunMmdEndpoints.Http;


public class HttpClientLogDelegatingHandler : DelegatingHandler
{
    private readonly ILogger<HttpClientLogDelegatingHandler> _logger;
    public HttpClientLogDelegatingHandler(ILogger<HttpClientLogDelegatingHandler> logger)
    {
        _logger = logger;
    }
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await safeDumpRequest(request);
        var response = await base.SendAsync(request, cancellationToken);
        safeDumpResponse(response);
        return response;
    }

    private void safeDumpResponse(HttpResponseMessage? response)
    {
        if (response is null)
        {
            _logger.LogDebug("<<< Response IS NULL");
            return;
        }
        //_logger.LogDebug("  <--" + response.ToString());
        //if (response.Content != null)
        //{
        //    _logger.LogDebug("  <--" + await response.Content.ReadAsStringAsync());
        //}
    }

    private async Task safeDumpRequest(HttpRequestMessage request)
    {

        if (request is null)
        {
            _logger.LogDebug(">>> Request IS NULL");
            return;
        }
        _logger.LogDebug("  -->" + request.ToString());
        if (request.Content != null)
        {
            _logger.LogDebug("  -->" + await request.Content.ReadAsStringAsync());
        }
    }
}

