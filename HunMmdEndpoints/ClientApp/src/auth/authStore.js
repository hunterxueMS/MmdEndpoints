import create from "zustand";

const useAuthStore = create((set) => ({
    graphAccessToken: "",
    endpointAccessToken: "",
    isAuthenticated: false,

    setGraphAccessToken: (accessToken) => set({ graphAccessToken: accessToken }),
    setEndpointAccessToken: (accessToken ) => set({ endpointAccessToken: accessToken }),
    setIsAuthenticated: (isAuthenticated) => set({ isAuthenticated }),
}));

export { useAuthStore };
