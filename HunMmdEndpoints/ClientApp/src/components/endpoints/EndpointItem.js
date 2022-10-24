import React, { useEffect, useState, useRef } from 'react';
import OverlayTrigger from 'react-bootstrap/OverlayTrigger';
import Popover from 'react-bootstrap/Popover';
export function EndpointItem(props) {
    const onItemClicked = (event, item) => {
        event.preventDefault();
        props.onItemClicked(item);
    };
    const renderParameters = (parameters) => {
        if (parameters !== null && parameters.length > 0) {
            return (
                <ul className="list-group list-group-horizontal">
                    {
                        parameters.map(param =>
                            <li className="text-muted list-group-item" key={param.name}>
                            {param.name}
                            </li>
                        )
                    }
                </ul>
            )
        } else { return <ul></ul> }
    };

    const popover = (
        props.endpoint.summary === null ? <p></p> :
            (<Popover id="popover-basic">
                <Popover.Header as="h3">{props.endpoint.operationId}</Popover.Header>
                <Popover.Body>
                    {props.endpoint.summary}
                </Popover.Body>
            </Popover>)
    );
    return (
        <OverlayTrigger trigger="hover" placement="right" overlay={popover}>
            <a href="#" className="list-group-item list-group-item-action" onClick={(event) => onItemClicked(event, props.endpoint)}
            >
                <div className="d-flex w-100 justify-content-between">
                    <h5 className="mb-1">{props.endpoint.operationId} &nbsp; &nbsp; &nbsp;<span className="text-primary">{props.endpoint.method}</span></h5>
                    {renderParameters(props.endpoint.parameters)}
                </div>
                <p className="mb-1">{props.endpoint.path}</p>
            </a>
        </OverlayTrigger >
    );
}

EndpointItem.displayName = 'EndpointItem';
