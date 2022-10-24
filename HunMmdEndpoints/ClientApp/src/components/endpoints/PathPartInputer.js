import React, { useRef } from 'react';
import { useState } from 'react';
import { useEndpointStore } from "../../store/EndpointStore";

//fix me, how to clear the text from parent component MmdEndpoint.js once the ServiceTypeSelector is changed.
export function PathPartInputer(props) {
    const setPath = useEndpointStore((state) => state.setPath);
    const path = useEndpointStore((state) => state.path);

    const onKeyUp = (event) => {
        if (event.key === 'Enter') {
            props.onSearch(path);
        }
    };

    const ref = useRef(null);
    //useEffect(() => {
    //    ref.current.value = "";
    //}, [props.pathPath]);

    return (
        <div className="input-group mb-3">
            <div className="input-group-prepend">
                <span className="input-group-text" id="basic-addon1">Path:</span>
            </div>
            <input type="text" ref={ref} className="form-control" placeholder="Path to Search" aria-label="Path To Search" aria-describedby="basic-addon1"
                onChange={(e) => setPath(e.target.value)}
                onKeyUp={onKeyUp}
                value={ path }
                />
        </div>
    );
}

PathPartInputer.displayName = 'PathPartInputer';
