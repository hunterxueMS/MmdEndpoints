import { MmdEndpoints } from "./components/endpoints/MmdEndpoints";
import { About } from "./components/About";

const AppRoutes = [
    {
        path: '/endpoints',
        element: <MmdEndpoints />
    },
    {
        path: '/about',
        element: <About />
    },
    {
        index: true,
        element: <MmdEndpoints />
    },
];

export default AppRoutes;
