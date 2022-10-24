import React, { useState } from 'react';
import { EndpointItem } from './EndpointItem';

export function EndpointList(props) {
    const onItemClicked = (item) => {
        const operationId = item.operationId;
        const notifyTip = (msg) => {
            props.notifyTip(msg);
        };
        let textToCopy = "";
        let message = "";
        if (props.serviceType === 'tm' || props.serviceType === 'device') {
            textToCopy = "# " + operationId;
            message = `Symbol search: [ctrl + t]; paste [${textToCopy}]`;
        } else {
            textToCopy = `[SwaggerOperation("${operationId}")]`;
            message = `Global search: [ctrl + shift + f]; paste [${textToCopy}]`;
        }
        navigator.clipboard.writeText(textToCopy);
        notifyTip(message);
    };

    return (
        <div className="list-group">
            {props.endpoints.map(endpoint =>
                <EndpointItem endpoint={endpoint} key={endpoint.operationId + ":" + endpoint.path} onItemClicked={onItemClicked} />
            )}
        </div>
    );
}

EndpointList.displayName = 'EndpointList';
