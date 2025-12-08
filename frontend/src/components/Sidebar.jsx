import { NavLink } from "react-router-dom";
import { Home, Users, Calendar, UserCog, FileText } from "lucide-react";

export default function Sidebar() {
    const menuItems = [
        { name: "Inicio", icon: Home, path: "/dashboard" },
        { name: "Pacientes", icon: Users, path: "/pacientes" },
        { name: "Calendario", icon: Calendar, path: "/schedulepage" },
        { name: "Profesionales", icon: UserCog, path: "/profesionales" },
        { name: "Reportes", icon: FileText, path: "/reportes" },
    ];

    return (
        <div className="w-64 h-screen bg-white border-r flex flex-col justify-between">

            {/* Logo */}
            <div>
                <div className="h-20 flex items-center px-6 border-b">
                    <h1 className="text-xl font-bold text-blue-600">ClinicPass</h1>
                </div>

                {/* Menu */}
                <nav className="mt-6 flex flex-col gap-1">
                    {menuItems.map((item) => (
                        <NavLink
                            key={item.name}
                            to={item.path}
                            className={({ isActive }) =>
                                `flex items-center gap-3 px-6 py-3 cursor-pointer
                 transition rounded-r-full
                 ${isActive ? "bg-blue-50 text-blue-600 font-medium" : "text-gray-700 hover:bg-gray-100"}`
                            }
                        >
                            <item.icon size={20} />
                            {item.name}
                        </NavLink>
                    ))}
                </nav>
            </div>

            {/* Usuario abajo */}
            <div className="border-t p-4 flex items-center gap-3">
                <div className="w-10 h-10 bg-blue-600 text-white rounded-full flex items-center justify-center">
                    D
                </div>
                <div>
                    <p className="font-medium text-gray-700">Carlos</p>
                    <p className="text-sm text-gray-400">Administrador</p>
                </div>
            </div>
        </div>
    );
}
