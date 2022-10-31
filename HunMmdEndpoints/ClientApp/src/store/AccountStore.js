import create from "zustand";
import { getMe } from "../services/graph/client";

const useAccountStore = create((set) => ({
    id: "",
    jobTitle: "",
    officeLocation: "",
    mail: "",
    getGraphMeAndSet: async () => {
        const me = await getMe();
        set({
            ...me,
        });
    },
}));

export { useAccountStore };
