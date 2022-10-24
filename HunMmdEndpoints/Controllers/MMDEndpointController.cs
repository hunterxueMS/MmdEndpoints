using HunMmdEndpoints.Services;
using HunMmdEndpoints.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HunMmdEndpoints.Controllers
{
    [ApiController]
    [Route("mmd")]
    public class MMDEndpointController : ControllerBase
    {
        private readonly ILogger<MMDEndpointController> _logger;
        private readonly IEndpointModelService endpointModelService;

        public MMDEndpointController(ILogger<MMDEndpointController> logger, IEndpointModelService endpointModelService)
        {
            _logger = logger;
            this.endpointModelService = endpointModelService;
        }

        [HttpGet]
        public async Task<List<EndpointModelItem>> Get([FromQuery(Name = "serviceType")] MMDServiceType serviceType,
            [FromQuery(Name = "pathInput")] string? pathInput = null
            )
        {
            _logger.LogDebug("Get endpoints, serviceType={serviceType}, pathInput={pathInput}", serviceType.ToString(), pathInput);
            return await endpointModelService.getEndpoints(serviceType, pathInput);
        }
    }
}