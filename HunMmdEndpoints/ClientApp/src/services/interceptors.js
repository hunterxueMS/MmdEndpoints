import { AxiosInstance } from "axios";

export const setAxiosInterceptors = (accessToken, instance) => {
    instance.interceptors.request.use((request) => {
        request.headers["Authorization"] = `Bearer ${accessToken}`;
        return request;
    });
};
