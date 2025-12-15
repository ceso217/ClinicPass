import { Search, Filter, Plus, Mail, Phone } from "lucide-react";

export default function Profesionales() {
    const profesionales = [
        {
            id: 1,
            nombre: "Dr. Carlos Fernández",
            dni: "28.456.789",
            especialidad: "Cardiología",
            contactoTel: "3624-111222",
            contactoMail: "cfernandez@clinicpass.com",
            rol: "Profesional",
            estado: "Activo",
        },
        {
            id: 2,
            nombre: "Dra. María González",
            dni: "32.789.456",
            especialidad: "Pediatría",
            contactoTel: "3624-333444",
            contactoMail: "mgonzalez@clinicpass.com",
            rol: "Profesional",
            estado: "Activo",
        },
        {
            id: 3,
            nombre: "Dr. Roberto Sánchez",
            dni: "25.123.456",
            especialidad: "Traumatología",
            contactoTel: "3624-555666",
            contactoMail: "rsanchez@clinicpass.com",
            rol: "Profesional",
            estado: "Activo",
        },
        {
            id: 4,
            nombre: "Dra. Ana Martínez",
            dni: "35.234.567",
            especialidad: "Dermatología",
            contactoTel: "3624-777888",
            contactoMail: "amartinez@clinicpass.com",
            rol: "Profesional",
            estado: "Inactivo",
        },
    ];

    const StatusBadge = ({ estado }) => {
        const activo = estado === "Activo";
        return (
            <span
                className={`px-3 py-1 text-sm rounded-full flex items-center gap-1 
        ${activo ? "bg-green-100 text-green-700" : "bg-red-100 text-red-700"}`}
            >
                ● {estado}
            </span>
        );
    };

    return (
        <div className="p-6 w-full">
            {/* HEADER */}
            <div className="flex items-center justify-between mb-4">
                <h1 className="text-2xl font-semibold">Gestión de Profesionales</h1>

                <button className="flex items-center gap-2 bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition">
                    <Plus size={18} />
                    Nuevo Profesional
                </button>
            </div>

            <p className="text-gray-500 mb-4">{profesionales.length} profesionales activos</p>

            {/* BUSCADOR + FILTRO */}
            <div className="flex items-center gap-3 mb-6">
                <div className="flex items-center bg-white shadow-sm border rounded-lg px-3 py-2 w-full">
                    <Search size={18} className="text-gray-400" />
                    <input
                        type="text"
                        placeholder="Buscar por nombre, DNI, especialidad o email..."
                        className="ml-2 outline-none w-full"
                    />
                </div>

                <button className="flex items-center gap-2 border px-4 py-2 rounded-lg shadow-sm hover:bg-gray-100 transition">
                    <Filter size={18} /> Filtros
                </button>
            </div>

            {/* TABLA */}
            <div className="bg-white shadow rounded-lg overflow-hidden">
                <table className="w-full text-left">
                    <thead className="bg-gray-100 text-gray-600 text-sm">
                        <tr>
                            <th className="p-3">Profesional</th>
                            <th className="p-3">DNI</th>
                            <th className="p-3">Especialidad</th>
                            <th className="p-3">Contacto</th>
                            <th className="p-3">Rol</th>
                            <th className="p-3">Estado</th>
                            <th className="p-3">Acciones</th>
                        </tr>
                    </thead>

                    <tbody>
                        {profesionales.map((p) => (
                            <tr key={p.id} className="border-b hover:bg-gray-50 transition">
                                <td className="p-3 flex items-center gap-3">
                                    <div className="w-8 h-8 rounded-full bg-purple-600 text-white flex items-center justify-center font-bold">
                                        {p.nombre.charAt(0)}
                                    </div>
                                    {p.nombre}
                                </td>

                                <td className="p-3">{p.dni}</td>

                                <td className="p-3">
                                    <span className="px-3 py-1 text-sm rounded-full bg-blue-100 text-blue-700">
                                        {p.especialidad}
                                    </span>
                                </td>

                                <td className="p-3">
                                    <div className="flex flex-col">
                                        <div className="flex items-center gap-1 text-sm">
                                            <Phone size={14} /> {p.contactoTel}
                                        </div>
                                        <div className="flex items-center gap-1 text-sm">
                                            <Mail size={14} /> {p.contactoMail}
                                        </div>
                                    </div>
                                </td>

                                <td className="p-3">{p.rol}</td>

                                <td className="p-3">
                                    <StatusBadge estado={p.estado} />
                                </td>

                                <td className="p-3 text-blue-600 cursor-pointer hover:underline">
                                    Editar
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>

            
            </div>
        </div>
    );
}
