

import Layout from '../Layout';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import Home from "../../pages/Home";
import Login from "../../pages/Login";
import LogOut from "../../pages/LogOut";
import Infos from "../../pages/Infos";
import CreateUser from "../../pages/CreateUser/createUser.jsx"
import ViewIndicators from "../ViewData/viewIndicators.jsx";

const router = createBrowserRouter([
    {
        element: <Layout />,
        children: [
            {
                path: "/",
                element: <Home />,
            },
            {
                path: "/login",
                element: <Login />,
            },
            {
                path: "/create-account",
                element: <CreateUser />,
            },
            {
                path: "/infos",
                element: <Infos />,
            },
            {
                path: "/view-indicators",
                element: <ViewIndicators />,
            },
            {
                path: "/logout",
                element: <LogOut />,
            }
        ]
    }
]);

export default function Router() {
    return (
        <RouterProvider router={router} />
    );
}
