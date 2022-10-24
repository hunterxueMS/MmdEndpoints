using HunMmdEndpoints.Http;
using HunMmdEndpoints.Services;
using HunMmdEndpoints.Services.HttpClientService;
using Microsoft.Net.Http.Headers;
using Polly;

namespace HunMmdEndpoints.Configurations;

public static class ServiceConfigurations
{
    public static void AddHttpClientServices(this IServiceCollection services)
    {
        services.AddTransient<HttpClientLogDelegatingHandler>();

        services.AddHttpClient<MonolithPartnerHttpClientService>(httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.BaseAddress = new Uri("https://preprod-mwaas-services-partnerapi.trafficmanager.net/swagger");
        }).AddHttpMessageHandler<HttpClientLogDelegatingHandler>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            return handler;
        })
        .AddTransientHttpErrorPolicy(p => PolicyHandler.WaitAndRetry())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.Timeout())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.CircuitBreaker(p));

        services.AddHttpClient<MonolithCustomerHttpClientService>(httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.BaseAddress = new Uri("https://preprod-mwaas-services-customerapi.trafficmanager.net/swagger");
        }).AddHttpMessageHandler<HttpClientLogDelegatingHandler>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            return handler;
        })
        .AddTransientHttpErrorPolicy(p => PolicyHandler.WaitAndRetry())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.Timeout())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.CircuitBreaker(p));

        services.AddHttpClient<DeviceMicroserviceHttpClientService>(httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.BaseAddress = new Uri("http://mmd-ppe-na01-device.trafficmanager.net/swagger");
        }).AddHttpMessageHandler<HttpClientLogDelegatingHandler>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            return handler;
        })
        .AddTransientHttpErrorPolicy(p => PolicyHandler.WaitAndRetry())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.Timeout())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.CircuitBreaker(p));

        services.AddHttpClient<TenantManagementMicroserviceHttpClientService>(httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.BaseAddress = new Uri("https://mmd-tm-preprod-nam.trafficmanager.net");
        }).AddHttpMessageHandler<HttpClientLogDelegatingHandler>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            return handler;
        })
        .AddTransientHttpErrorPolicy(p => PolicyHandler.WaitAndRetry())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.Timeout())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.CircuitBreaker(p));

        services.AddHttpClient<OperationMicroserviceHttpClientService>(httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.BaseAddress = new Uri("https://mmd-support-preprod-nam.trafficmanager.net");
        }).AddHttpMessageHandler<HttpClientLogDelegatingHandler>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            return handler;
        })
        .AddTransientHttpErrorPolicy(p => PolicyHandler.WaitAndRetry())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.Timeout())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.CircuitBreaker(p));

        services.AddHttpClient<ArtMicroserviceHttpClientService>(httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.BaseAddress = new Uri("https://mmd-preprod-nam-art.trafficmanager.net");
        }).AddHttpMessageHandler<HttpClientLogDelegatingHandler>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            return handler;
        })
        .AddTransientHttpErrorPolicy(p => PolicyHandler.WaitAndRetry())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.Timeout())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.CircuitBreaker(p));

        services.AddHttpClient<UmMicroserviceHttpClientService>(httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.BaseAddress = new Uri("https://mmd-um-preprod-nam.trafficmanager.net");
        }).AddHttpMessageHandler<HttpClientLogDelegatingHandler>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            return handler;
        })
        .AddTransientHttpErrorPolicy(p => PolicyHandler.WaitAndRetry())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.Timeout())
        .AddTransientHttpErrorPolicy(p => PolicyHandler.CircuitBreaker(p));
    }

    public static void AddMMDServices(this IServiceCollection services)
    {
        services.AddSingleton<IEndpointModelService, EndpointModelServiceImpl>();
    }
}
