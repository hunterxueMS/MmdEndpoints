import { AccountInfo, IPublicClientApplication, SilentRequest } from "@azure/msal-browser";
import { graphScopes, mentorshipScopes } from "./msalConfig";
import { setAxiosInterceptors } from "../services/interceptors";
import { graphHttpClient } from "../services/graph/client";
import { endpointApiHttpClient } from "../services/endpoints/client";

async function handleLogin(instance) {
    try {
        await instance.loginRedirect({ scopes: graphScopes });
    } catch (e) {
        console.log(e);
    }
}

async function requestAccessToken(instance, accounts) {
    const graphSilentRequest = {
        scopes: graphScopes,
        account: accounts[0],
    };

    const mentorshipSilentRequest = {
        scopes: mentorshipScopes,
        account: accounts[0],
    };

    let graphAccessToken = "";
    let endpointAccessToken = "";

    // Silently acquires an access token
    try {
        const response = await instance.acquireTokenSilent(graphSilentRequest);
        graphAccessToken = response.accessToken;
        setAxiosInterceptors(graphAccessToken, graphHttpClient);
    } catch (e) {
        console.log(e);
    }

    try {
        const response = await instance.acquireTokenSilent(mentorshipSilentRequest);
        endpointAccessToken = response.accessToken;
        setAxiosInterceptors(endpointAccessToken, endpointApiHttpClient);
    } catch (e) {
        console.log(e);
    }

    return [graphAccessToken, endpointAccessToken];
}

export { handleLogin, requestAccessToken };
