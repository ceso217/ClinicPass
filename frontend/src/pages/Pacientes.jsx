import React from "react";
import { Search, SlidersHorizontal, FileText, Calendar } from "lucide-react";

const pacientes = [
    {
        nombre: "Juan Carlos Pérez",
        dni: "35.456.789",
        edad: 45,
        telefono: "3624-123456",
        localidad: "Resistencia",
        ultimaFicha: "27/11/2025",
        inicial: "J",
    },
    {
        nombre: "María Laura González",
        dni: "42.789.456",
        edad: 32,
        telefono: "3624-789012",
        localidad: "Resistencia",
        ultimaFicha: "29/11/2025",
        inicial: "M",
    },
    {
        nombre: "Carlos Alberto Rodríguez",
        dni: "28.123.456",
        edad: 58,
        telefono: "3624-345678",
        localidad: "Barranqueras",
        ultimaFicha: "24/11/2025",
        inicial: "C",
    },
    {
        nombre: "Ana Sofía Martínez",
        dni: "40.234.567",
        edad: 28,
        telefono: "3624-567890",
        localidad: "Resistencia",
        ultimaFicha: "30/11/2025",
        inicial: "A",
    },
];

const DashboardPacientes = () => {
    return (
        <div className="p-6 md:p-10">

            {/* Header */}
            <div className="flex justify-between items-center mb-6">
                <div>
                    <h1 className="text-2xl font-bold">Gestión de Pacientes</h1>
                    <p className="text-gray-500 text-sm">
                        {pacientes.length} pacientes registrados
                    </p>
                </div>

                <button className="bg-blue-600 text-white px-4 py-2 rounded-lg shadow hover:bg-blue-700 transition">
                    + Nuevo Paciente
                </button>
            </div>

            {/* Buscador */}
            <div className="flex gap-4 w-full mb-6">
                <div className="flex items-center gap-2 w-full border rounded-xl px-4 py-2 shadow-sm bg-white">
                    <Search size={20} className="text-gray-400" />
                    <input
                        type="text"
                        placeholder="Buscar por nombre, DNI o localidad..."
                        className="w-full outline-none"
                    />
                </div>

                <button className="flex items-center gap-2 border px-4 py-2 rounded-xl shadow-sm bg-white hover:bg-gray-50">
                    <SlidersHorizontal size={20} />
                    Filtros
                </button>
            </div>

            {/* Tabla */}
            <div className="overflow-x-auto bg-white shadow rounded-xl">
                <table className="w-full">
                    <thead className="bg-gray-50 text-left">
                        <tr className="text-gray-600 text-sm">
                            <th className="p-4">Paciente</th>
                            <th className="p-4">DNI</th>
                            <th className="p-4">Edad</th>
                            <th className="p-4">Teléfono</th>
                            <th className="p-4">Localidad</th>
                            <th className="p-4">Última Ficha</th>
                            <th className="p-4">Acciones</th>
                        </tr>
                    </thead>

                    <tbody>
                        {pacientes.map((p, index) => (
                            <tr key={index} className="border-t hover:bg-gray-50">
                                <td className="p-4 flex items-center gap-3">
                                    <div className="w-9 h-9 rounded-full bg-blue-600 text-white flex items-center justify-center font-semibold">
                                        {p.inicial}
                                    </div>
                                    {p.nombre}
                                </td>

                                <td className="p-4">{p.dni}</td>
                                <td className="p-4">{p.edad} años</td>
                                <td className="p-4">{p.telefono}</td>
                                <td className="p-4">{p.localidad}</td>
                                <td className="p-4">{p.ultimaFicha}</td>

                                <td className="p-4 flex gap-4">
                                    <button className="text-blue-600 hover:text-blue-800">
                                        <FileText size={20} />
                                    </button>
                                    <button className="text-green-600 hover:text-green-800">
                                        <Calendar size={20} />
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>

                {/* Footer tabla */}
                <div className="p-4 flex justify-between items-center text-sm text-gray-600">
                    <span>Mostrando {pacientes.length} de {pacientes.length} pacientes</span>

                    <div className="flex gap-2">
                        <button className="px-3 py-1 border rounded-lg">Anterior</button>
                        <button className="px-3 py-1 bg-blue-600 text-white rounded-lg">1</button>
                        <button className="px-3 py-1 border rounded-lg">2</button>
                        <button className="px-3 py-1 border rounded-lg">Siguiente</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default DashboardPacientes;
