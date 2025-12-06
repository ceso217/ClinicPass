import React from "react";
import { Link } from "react-router-dom";

export default function Dashboard() {
    return (
        <div className="p-10 flex flex-col gap-6 max-w-xl mx-auto">
            <h1 className="text-3xl font-bold text-center mb-6">Dashboard</h1>

            <div className="p-6 border rounded-xl shadow flex justify-between items-center">
                <h2 className="text-xl">Agregar Paciente</h2>
                <Link
                    to="/agregar-paciente"
                    className="bg-blue-600 text-white px-4 py-2 rounded"
                >
                    Ir
                </Link>
            </div>

            <div className="p-6 border rounded-xl shadow flex justify-between items-center">
                <h2 className="text-xl">Agregar Turno</h2>
                <Link
                    to="/agregar-turno"
                    className="bg-green-600 text-white px-4 py-2 rounded"
                >
                    Ir
                </Link>
            </div>

            <div className="p-6 border rounded-xl shadow flex justify-between items-center">
                <h2 className="text-xl">Pacientes Registrados</h2>
                <Link
                    to="/pacientes"
                    className="bg-gray-700 text-white px-4 py-2 rounded"
                >
                    Ver Tabla
                </Link>
            </div>
        </div>
    );
}
