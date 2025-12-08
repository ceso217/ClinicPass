import { Calendar, Users, ClipboardList, TrendingUp, Clock } from "lucide-react";

export default function Dashboard() {
    return (
        <div className="p-6">
            {/* Título */}
            <h1 className="text-2xl font-bold mb-1">Panel de Control</h1>
            <p className="text-gray-500 mb-6">Resumen de actividad y próximos turnos</p>

            {/* Métricas */}
            <div className="grid grid-cols-1 md:grid-cols-4 gap-4 mb-8">
                <div className="bg-white p-5 rounded-xl border flex items-center gap-4">
                    <div className="bg-blue-100 text-blue-600 p-3 rounded-lg">
                        <Users size={24} />
                    </div>
                    <div>
                        <p className="text-sm text-gray-500">Pacientes Activos</p>
                        <p className="text-xl font-semibold">234</p>
                        <p className="text-xs text-green-500 mt-1">+12%</p>
                    </div>
                </div>

                <div className="bg-white p-5 rounded-xl border flex items-center gap-4">
                    <div className="bg-green-100 text-green-600 p-3 rounded-lg">
                        <Calendar size={24} />
                    </div>
                    <div>
                        <p className="text-sm text-gray-500">Turnos Hoy</p>
                        <p className="text-xl font-semibold">18</p>
                        <p className="text-xs text-gray-400 mt-1">3 pendientes</p>
                    </div>
                </div>

                <div className="bg-white p-5 rounded-xl border flex items-center gap-4">
                    <div className="bg-orange-100 text-orange-600 p-3 rounded-lg">
                        <ClipboardList size={24} />
                    </div>
                    <div>
                        <p className="text-sm text-gray-500">Fichas Pendientes</p>
                        <p className="text-xl font-semibold">7</p>
                        <p className="text-xs text-red-500 mt-1">Urgente</p>
                    </div>
                </div>

                <div className="bg-white p-5 rounded-xl border flex items-center gap-4">
                    <div className="bg-purple-100 text-purple-600 p-3 rounded-lg">
                        <TrendingUp size={24} />
                    </div>
                    <div>
                        <p className="text-sm text-gray-500">Profesionales</p>
                        <p className="text-xl font-semibold">12</p>
                        <p className="text-xs text-gray-400 mt-1">11 activos</p>
                    </div>
                </div>
            </div>

            {/* Contenedores inferiores */}
            <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">

                {/* Próximos turnos */}
                <div className="bg-white p-5 rounded-xl border col-span-2">
                    <div className="flex justify-between items-center mb-4">
                        <h2 className="text-lg font-semibold">Próximos Turnos</h2>
                        <button className="text-blue-600 text-sm">Ver calendario</button>
                    </div>

                    <div className="space-y-4">
                        {[
                            { hora: "09:00", nombre: "Juan Pérez", tipo: "Control", estado: "Confirmado", color: "green" },
                            { hora: "10:30", nombre: "María González", tipo: "Primera consulta", estado: "Confirmado", color: "green" },
                            { hora: "11:00", nombre: "Carlos Rodríguez", tipo: "Seguimiento", estado: "Pendiente", color: "yellow" },
                            { hora: "14:00", nombre: "Ana Martínez", tipo: "Control", estado: "Confirmado", color: "green" },
                        ].map((t, i) => (
                            <div key={i} className="p-4 bg-gray-50 rounded-xl flex items-center justify-between">
                                <div className="flex items-center gap-4">
                                    <div className="bg-white border p-3 rounded-xl text-sm font-semibold">
                                        {t.hora}
                                    </div>
                                    <div>
                                        <p className="font-medium">{t.nombre}</p>
                                        <p className="text-sm text-gray-500">{t.tipo}</p>
                                    </div>
                                </div>

                                <span
                                    className={`px-3 py-1 text-sm rounded-full ${t.color === "green"
                                            ? "bg-green-100 text-green-600"
                                            : "bg-yellow-100 text-yellow-600"
                                        }`}
                                >
                                    {t.estado}
                                </span>
                            </div>
                        ))}
                    </div>
                </div>

                {/* Actividad reciente */}
                <div className="bg-white p-5 rounded-xl border">
                    <h2 className="text-lg font-semibold mb-4">Actividad Reciente</h2>

                    <div className="space-y-4">
                        {[
                            { msg: "Nueva ficha de seguimiento", user: "Laura Sánchez", tiempo: "Hace 15 min" },
                            { msg: "Turno confirmado", user: "Roberto Díaz", tiempo: "Hace 1 hora" },
                            { msg: "Nuevo paciente registrado", user: "Sofía Torres", tiempo: "Hace 2 horas" },
                        ].map((a, i) => (
                            <div key={i} className="flex flex-col border-b pb-3">
                                <p className="font-medium">{a.msg}</p>
                                <p className="text-sm text-gray-600">{a.user}</p>
                                <p className="text-xs text-gray-400">{a.tiempo}</p>
                            </div>
                        ))}
                    </div>

                    {/* Alerta */}
                    <div className="mt-6 bg-orange-50 border border-orange-200 text-orange-700 p-4 rounded-xl text-sm flex gap-2">
                        <Clock size={18} />
                        <div>
                            <p className="font-semibold">Recordatorio</p>
                            <p>7 fichas de seguimiento pendientes de completar</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

