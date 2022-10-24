import create from "zustand";


const useEndpointStore = create((set) => ({
    path: "",
    setPath: (newPath) => set({ path: newPath }),
    serviceType: "partner", //default to partner service
    setServiceType: (newType) => set({ serviceType: newType}),
}));

export { useEndpointStore };
