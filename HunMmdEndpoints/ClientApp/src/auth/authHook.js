import React from "react";
import { InteractionStatus } from "@azure/msal-browser";
import { requestAccessToken } from "./helpers";
import { useAuthStore } from "./authStore";
import { useIsAuthenticated } from "@azure/msal-react";
import { useMsal } from "@azure/msal-react";

function useAuthEffect() {
    const { instance, accounts, inProgress } = useMsal();
    const isAuthenticated = useIsAuthenticated();
    const graphAccessToken = useAuthStore((state) => state.graphAccessToken);
    const endpointAccessToken = useAuthStore((state) => state.endpointAccessToken);
    const setGraphAccessToken = useAuthStore((state) => state.setGraphAccessToken);
    const setEndpointAccessToken = useAuthStore((state) => state.setEndpointAccessToken);
    const setIsAuthenticated = useAuthStore((state) => state.setIsAuthenticated);

    React.useEffect(() => {
        if (
            isAuthenticated &&
            inProgress === InteractionStatus.None &&
            (graphAccessToken.length === 0 || endpointAccessToken.length === 0)
        ) {
            requestAccessToken(instance, accounts)
                .then((tokens) => {
                    if (tokens === null || tokens.length < 2) {
                        setIsAuthenticated(false);
                        return;
                    }
                    setGraphAccessToken(tokens[0]);
                    setEndpointAccessToken(tokens[1]);
                    if (tokens[0].length > 0 && tokens[1].length > 0) {
                        setIsAuthenticated(true);
                    }
                })
                .catch((e) => {
                    console.log(e);
                    setIsAuthenticated(false);
                });
        }

        if (
            (!isAuthenticated && inProgress === InteractionStatus.None) ||
            graphAccessToken.length === 0 ||
            endpointAccessToken.length === 0
        ) {
            setIsAuthenticated(false);
        }
    }, [
        inProgress,
        instance,
        accounts,
        graphAccessToken,
        endpointAccessToken,
        isAuthenticated,
        setGraphAccessToken,
        setEndpointAccessToken,
        setIsAuthenticated,
    ]);
}

export { useAuthEffect };
