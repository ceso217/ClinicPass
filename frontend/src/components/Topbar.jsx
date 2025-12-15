import { Bell } from "lucide-react";

export default function Topbar() {
    const fecha = new Date().toLocaleDateString("es-AR", {
        weekday: "long",
        day: "numeric",
        month: "long",
        year: "numeric",
    });

    return (
        <div className="h-20 bg-white border-b px-8 flex items-center justify-between">

            {/* Bienvenida */}
            <div>
                <p className="text-lg font-semibold">Bienvenido, Carlos</p>
                <p className="text-sm text-gray-500">{fecha}</p>
            </div>

            {/* Notificaciones y usuario */}
            <div className="flex items-center gap-6">
                {/* Campana */}
                <button className="relative">
                    <Bell className="text-gray-600" size={24} />
                    <span className="absolute -top-1 -right-1 w-3 h-3 bg-red-500 rounded-full"></span>
                </button>

                {/* Usuario */}
                <div className="flex items-center gap-3">
                    <div className="w-10 h-10 bg-blue-600 rounded-full text-white font-semibold flex items-center justify-center">
                        D
                    </div>
                    <div>
                        <p className="font-medium text-gray-700">Dr. Carlos Administrador</p>
                        <p className="text-sm text-gray-400">Administrador</p>
                    </div>
                </div>
            </div>
        </div>
    );
}
