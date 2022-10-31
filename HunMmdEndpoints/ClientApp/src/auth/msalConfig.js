//import { Configuration } from "@azure/msal-browser";

//export const msalConfig = {
//    auth: {
//        clientId: "33a0cb9f-9369-44c9-8e35-4057a5871123",
//        authority: "https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47", // This is a URL (e.g. https://login.microsoftonline.com/{your tenant ID})
//        redirectUri: process.env.AAD_REDIRECT_URL,
//        postLogoutRedirectUri: "/",
//    },
//    cache: {
//        cacheLocation: "localStorage", // Configures cache location. "sessionStorage" is more secure, but "localStorage" gives you SSO between tabs.
//        storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
//    },
//};

//// Add scopes here for ID token to be used at Microsoft identity platform endpoints.
//export const graphScopes = ["User.Read"];
//export const mentorshipScopes = ["api://f43de2c2-b0a8-495c-a9ea-fb82d95ddd1e/access_as_user"]


import { Configuration } from "@azure/msal-browser";

export const msalConfig = {
    auth: {
        clientId: "ffc25fff-5e5e-4088-8c08-0c570d86fa49",
        authority: "https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47", // This is a URL (e.g. https://login.microsoftonline.com/{your tenant ID})
        redirectUri: "https://localhost:44473",
        postLogoutRedirectUri: "/",
    },
    cache: {
        cacheLocation: "localStorage", // Configures cache location. "sessionStorage" is more secure, but "localStorage" gives you SSO between tabs.
        storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
    },
};

// Add scopes here for ID token to be used at Microsoft identity platform endpoints.
export const graphScopes = ["User.Read"];
export const mentorshipScopes = ["api://38eaab4d-8185-4247-a957-358c6e377f7d/Endpoints.Read"]
