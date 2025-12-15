import { Outlet } from "react-router-dom";
import Topbar from "./Topbar";
import Sidebar from "./Sidebar";

export default function Layout() {
    return (
        <div className="flex h-screen">
            {/* Sidebar */}
            <Sidebar />

            {/* Contenido derecho */}
            <div className="flex flex-col flex-1">

                {/* Topbar */}
                <Topbar />

                {/* Contenido de la página */}
                <main className="p-6 flex-1 bg-gray-50">
                    <Outlet />
                </main>
            </div>
        </div>
    );
}

