using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using System.Net;

namespace HunMmdEndpoints.Http;

public static class PolicyHandler
{
    public static IAsyncPolicy<HttpResponseMessage> WaitAndRetry(int retryCount = 2) =>
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public static IAsyncPolicy<HttpResponseMessage> Timeout(int seconds = 2) =>
        Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(seconds));
    public static IAsyncPolicy<HttpResponseMessage> CircuitBreaker(PolicyBuilder<HttpResponseMessage> p,
        int handledEventsAllowedBeforeBreaking = 5, int durationOfBreakSecs = 30) => p.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking, TimeSpan.FromSeconds(durationOfBreakSecs));
}

