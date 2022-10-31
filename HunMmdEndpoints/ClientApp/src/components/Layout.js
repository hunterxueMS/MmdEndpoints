import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { useAccountStore } from "../store/AccountStore";
import { useAuthEffect } from "../auth/authHook";
import { useAuthStore } from "../auth/authStore";

const Layout = (props) => {
    const getGraphMeAndSet = useAccountStore((state) => state.getGraphMeAndSet);
    const isAuthenticated = useAuthStore((state) => state.isAuthenticated);

    useAuthEffect();

    React.useEffect(() => {
        if (isAuthenticated) {
            getGraphMeAndSet();
        }
    }, [isAuthenticated, getGraphMeAndSet]);
    return (
        <div>
            <NavMenu />
            <Container>
                {props.children}
            </Container>
        </div>
    );
}
export { Layout };
