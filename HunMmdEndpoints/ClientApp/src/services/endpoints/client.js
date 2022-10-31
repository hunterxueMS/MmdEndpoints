import axios from "axios";

export const endpointApiHttpClient = axios.create({
    baseURL: "/mmd",
    headers: {
        "Content-Type": "application/json",
        'X-ClientId': 'mmd-client',
    },
});

export const getEndpoints = async (pathInput, serviceType) => {
    let url = '';
    url += '?serviceType=' + serviceType;
    if (pathInput !== null && pathInput !== '') url += '&pathInput=' + pathInput;
    const resp = await endpointApiHttpClient.get(url);
    return resp.data;
};