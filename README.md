# MMD endpoint explorer and locator. 


Most of the time, in MMD system, we have http call between Microservice to/from Monolith, or Microservices to/from each other.

But, on the call site, we only pass part of the URL endpoint, and even worse, due to the nature of dotnet core, we define path segment of an endpoint on both class and method level.  So, it's really a painful experience to find the right target method across http call.

We create this project to try to ease the pain when navigating http call between different services.

E.g.

```cs
// With this system, you can serarch path: `tenants/{tenantId}/manageddevices`, and system can auto convert  path to endpoint search term,
// and copy the search term into the clipboard

// Client code demo
using var httpClient = this.monolithHttpClientFactory.CreateAuthenticatedHttpClientToMonolith();

// url path to search
var results =
await httpClient.GetAsync&gt;(
    requestUri: $"tenants/{tenantId}/manageddevices");

// Server code demo(can be TM || Partner || Customer || Devices)
[RoutePrefix("api/v1.0/tenants")]
public class TenantsV1Controller : ServiceController{
    [HttpGet]
    [Route("{tenantId}/manageddevices", Name = GetManagedDevicesRoute)]
    [SwaggerOperation("GetTenantManagedDevices")]
    public async Task GetTenantManagedDevices(
    string tenantId)
    {
        //...
    }
}
```

# How to use

[Online Environment](https://hunmmdendpoints.azurewebsites.net/endpoints), do bookmark it and use it often. 

> If you find something to improvement, welcome to join the project and raise PR or issues. 


- Select the target service you want
- Copy a part of path from your http client code and paste it into the input box
- Click the endpoint you are visiting, transformed search term is copied to your clipboard automatically.
- Go back to VS and use [ctrl + shift + f] to search the auto copied [searchTerm].

**NOTE**: if it's [tenant management || device] service. instead of global search, use symbol search: [ctrl + t] + [searchTerm]

**NOTE**: API Schema will be upgraded and cached in background for one hour. The system parse the published swagger schema from PPE env. 

**NOTE**: Currently, only support 4 types of api schemas, but it can be extended in the future.

# Next step
In the ideal world, this feature should not be developed as a web SPA, instead, it should be defined as native VS plugin, so that we don't have to leave the IDE at all. 

But we have no experience of plugin development and we do want to have some learning chance for web client side development, which can be helpful to actual client side PR review as well.