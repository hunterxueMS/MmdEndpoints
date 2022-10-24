import React, { useState } from 'react';
import { useEndpointStore } from "../../store/EndpointStore";

export function ServiceTypeSelector(props) {
    const setServiceType = useEndpointStore((state) => state.setServiceType);
    const serviceType = useEndpointStore((state) => state.serviceType);
    const options = [
        { value: 'tm', text: 'TenantManagment 🧵' },
        { value: 'device', text: 'Device&Profile 📺' },
        { value: 'partner', text: 'Partner 🎭' },
        { value: 'customer', text: 'Customer 🥝' },
        { value: 'operation', text: 'Operation 👀'},
        { value: 'art', text: 'ART 🏺'},
        { value: 'um', text: 'UM 🧠'},
    ];

    const onSelectionChange = (event) => {
        setServiceType(event.target.value);
        props.onServiceTypeChanged();
    };
    return (
        <select value={serviceType} onChange={onSelectionChange}>
            {options.map(option => (
                <option key={option.value} value={option.value}>
                    {option.text}
                </option>
            ))}
        </select>
    );
}

ServiceTypeSelector.displayName = 'ServiceTypeSelector';
