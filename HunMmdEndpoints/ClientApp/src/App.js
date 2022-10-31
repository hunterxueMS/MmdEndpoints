import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import { handleLogin } from "./auth/helpers";
import { InteractionStatus } from "@azure/msal-browser";
import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import './custom.css';

const App = () => {
    const { instance, inProgress } = useMsal();
    const isAuthenticated = useIsAuthenticated();
    React.useEffect(() => {
        if (!isAuthenticated && inProgress === InteractionStatus.None) {
            handleLogin(instance);
        }
    });
    return (
        <Layout>
            <Routes>
                {AppRoutes.map((route, index) => {
                    const { element, ...rest } = route;
                    return <Route key={index} {...rest} element={element} />;
                })}
            </Routes>
        </Layout>
    );
}

App.displayName = App.name;
export default App;
