import React, { Component } from 'react';
import ReactMarkdown from 'react-markdown'
import { Prism as SyntaxHighlighter } from 'react-syntax-highlighter';
import { dark } from 'react-syntax-highlighter/dist/esm/styles/prism';

export class About extends Component {
    static displayName = About.name;

    render() {
        return (
            <div className="container">
                <h1>MMD Endpoint Locator</h1>
                <div className="row">
                    <div className="col-4">
                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Author: </span>
                            </div>
                            <p className="form-control">Hunter Xue</p>
                        </div>

                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text">Email: &nbsp;</span>
                            </div>
                            <p className="form-control">hunterxue@microsoft.com</p>
                        </div>
                    </div>

                </div>
                <MarkdownToHtml />
            </div>
        );
    }
}

export function MarkdownToHtml() {
    const markdownStr1 = `
# Why need this project
Most of the time, in MMD system, we may have http call between microserivce to/from monolith, or microservices to/from each other.

But, on the call site, we only pass part of the url endpoint, and even worse, due to the nature of dotnet core, we define path of an endpoint on both class and method level.
So it's really a painful experience to find the right target method across the http call.

I create this project to try to easy the pain when navigating http call between different services.

e.g.
`;


    const codeString = `
// With this system, you can serarch path: \`tenants/{tenantId}/manageddevices\`, and system can auto convert  path to endpoint search term,
// and copy the search term into the clipboard

// Client code demo
using var httpClient = this.monolithHttpClientFactory.CreateAuthenticatedHttpClientToMonolith();

// url path to search
var results =
await httpClient.GetAsync<ODataCollection<MMD.Device.Contracts.Monolith.Device>>(
    requestUri: $"tenants/{tenantId}/manageddevices");

// Server code demo(can be TM || Partner || Customer || Devices)
[RoutePrefix("api/v1.0/tenants")]
public class TenantsV1Controller : ServiceController{
    [HttpGet]
    [Route("{tenantId}/manageddevices", Name = GetManagedDevicesRoute)]
    [SwaggerOperation("GetTenantManagedDevices")]
    public async Task<HttpResponseMessage> GetTenantManagedDevices(
    string tenantId)
    {
        //...
    }
}
`;



    const markdownStr2 = `
# How to use
1. Select the target service you want
2. Copy a part of path from your http client code and paste it into the input box
3. Click the endpoint you are visiting, transformed search term is copied to your clipboard automatically.
4. Go back to VS and use [ctrl + shift + f] to search the auto copied search term.
5. NOTE: if it's [tenant management || Device] service. instead of global search, use symbol search: [ctrl + t] + searchTerm

> **NOTE**: API Schema will be upgraded and cached in background for one hour. The system parse the published swagger schema from PPE env.
> **NOTE**: Currently, only support 4 types of api schemas, but it can be extended in the future.

# Next step
In the ideal world, this feature should not be developed as a web SPA, instead, it should be defined as native VS plugin, so that we don't have to leave the IDE at all. But I have
no experience of plugin development and I do want to have some learning chance for web client side development, which can be helpful to actual client side PR review as well.
`;
    return (
        <div>
            <ReactMarkdown children={markdownStr1}>
            </ReactMarkdown>
            <SyntaxHighlighter language="csharp" style={dark}>
                {codeString}
            </SyntaxHighlighter>
            <ReactMarkdown children={markdownStr2}>
            </ReactMarkdown>
        </div>

    )
}
