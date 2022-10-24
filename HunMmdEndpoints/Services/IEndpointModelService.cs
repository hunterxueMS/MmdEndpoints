using HunMmdEndpoints.Utils;

namespace HunMmdEndpoints.Services;

public interface IEndpointModelService
{
	/// <summary>
	/// get endpoints with searchpath 
	/// </summary>
	/// <param name="serviceType"></param>
	/// <param name="pathPart"></param>
	/// <returns></returns>
	Task<List<EndpointModelItem>> getEndpoints(MMDServiceType serviceType, string? pathPart = null);
}
