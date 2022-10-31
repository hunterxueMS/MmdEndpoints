using HunMmdEndpoints.Utils;

namespace HunMmdEndpoints.Services.BackgroundServices;

public class BackgroundStartupCheckService : BackgroundService
{
    private readonly StartupHealthCheck _healthCheck;
    private readonly IEndpointModelService _endpointModelService;
    private readonly ILogger<BackgroundStartupCheckService> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public BackgroundStartupCheckService(StartupHealthCheck healthCheck, IEndpointModelService endpointModelService,
        ILogger<BackgroundStartupCheckService> logger, IHostApplicationLifetime applicationLifetime
        )
    {
        _healthCheck = healthCheck;
        _endpointModelService = endpointModelService;
        _logger = logger;
        _applicationLifetime = applicationLifetime;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //try
        //{
        //    // Simulate the effect of a long-running task.
        //    foreach (MMDServiceType serviceType in Enum.GetValues<MMDServiceType>())
        //    {
        //        //preload the cache
        //        await _endpointModelService!.getEndpoints(serviceType);
        //    }
        //}
        //catch (Exception e)
        //{
        //    _logger.LogError(e, "fail to preload services, stop application now");
        //    _applicationLifetime.StopApplication();
        //}
        await Task.CompletedTask;
        _healthCheck.StartupCompleted = true;
    }
}