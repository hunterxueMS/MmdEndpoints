import React, { useState, useCallback, useEffect } from 'react';
import { EndpointList } from './EndpointList';
import { NoContentPlaceholder } from './NoContentPlaceholder';
import { PathPartInputer } from './PathPartInputer';
import { ServiceTypeSelector } from './ServiceTypeSelector';
import { MmdToast } from './MmdToast';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Stack from 'react-bootstrap/Stack';
import debounce from "lodash/debounce";
import { useEndpointStore } from "../../store/EndpointStore";

export function MmdEndpoints(props) {
    const [endpoints, setEndpoints] = useState([]);
    const [loading, setLoading] = useState(true);
    const [searchPhrase, setSearchPhrase] = useState("");

    const [showToast, setShowToast] = useState(false);
    const [notification, setNotification] = useState(null);

    const setPath = useEndpointStore((state) => state.setPath);
    const path = useEndpointStore((state) => state.path);
    const serviceType = useEndpointStore((state) => state.serviceType);

    const populateEndpoints = async (pathPart, serviceType) => {
        setLoading(true);
        let url = 'mmd';
        url += '?serviceType=' + serviceType;
        if (pathPart !== null && pathPart !== '') url += '&pathInput=' + pathPart;
        // attach headers
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'X-ClientId': 'mmd-client',
            }
        };

        const response = await fetch(url, requestOptions);
        const data = await response.json();
        setEndpoints(data)
        setLoading(false);
    };

    //const debouncePopulated = useCallback(
    //    debounce(populateEndpoints, 300)
    //);

    useEffect(() => {
        //debouncePopulated(searchPhrase, serviceType);
        populateEndpoints(searchPhrase, serviceType);
    }, [searchPhrase, serviceType]);

    const onServiceTypeChanged = () => {
        setSearchPhrase("");
        setPath("");
    };

    const onSearch = (path) => {
        setSearchPhrase(path);
    }

    const notifyTip = (msg) => {
        setShowToast(true);
        setNotification(msg);
        setTimeout(() => {
            setShowToast(false);
            setNotification(null);
        }, 5000);
    };
    const onToastClose = () => {
        setShowToast(false);
    };
    const renderToast = () => {
        return (<div className="position-fixed bottom-0 end-0">
            {
                showToast ? <MmdToast message={notification} subTitle="search item auto-copied to clipboard" mainTitle={serviceType} onToastClose={onToastClose} /> : <span></span>
            }
        </div>);
    };

    return (
        <Container>
            <Row>
                <Col md="4">
                    <ServiceTypeSelector onServiceTypeChanged={onServiceTypeChanged}/>
                </Col>
                <Col md="8">
                    <PathPartInputer onSearch={onSearch} path={path} setPath={ setPath }/>
                </Col>
            </Row>

            <br />
            <hr />
            <br />

            <Row>
                {
                    loading ?
                        <NoContentPlaceholder />
                        : (
                            <Stack>
                                <EndpointList endpoints={endpoints} serviceType={serviceType} notifyTip={notifyTip} />
                            </Stack>
                        )
                }
            </Row>


            {
                renderToast()
            }
        </Container>
    );
};

MmdEndpoints.displayName = 'MmdEndpoints';
