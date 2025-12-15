import { useState } from "react";
import { ChevronLeft, ChevronRight, Plus, Clock, User } from "lucide-react";

export default function SchedulePage() {
    const [selectedDate, setSelectedDate] = useState(null);
    const [currentMonth, setCurrentMonth] = useState(new Date("2025-12-01"));

    // Datos fake (después conectás al backend)
    const appointments = {
        "2025-12-01": [
            { hour: "08:00", name: "Juan Pérez", type: "Control", status: "Confirmado" },
            { hour: "09:00", name: "María González", type: "Primera consulta", status: "Confirmado" },
            { hour: "10:00", name: "Carlos Rodríguez", type: "Seguimiento", status: "Pendiente" },
            { hour: "11:00", name: "Ana Martínez", type: "Control", status: "Confirmado" },
            { hour: "14:00", name: "Roberto Díaz", type: "Primera consulta", status: "Confirmado" },
        ],
    };

    const year = currentMonth.getFullYear();
    const month = currentMonth.getMonth();

    const firstDay = new Date(year, month, 1).getDay();
    const daysInMonth = new Date(year, month + 1, 0).getDate();

    const days = [];
    for (let i = 0; i < firstDay; i++) days.push(null);
    for (let d = 1; d <= daysInMonth; d++) days.push(d);

    const formatDate = (isoDate) => {
        const [year, month, day] = isoDate.split("-");
        const months = [
            "enero", "febrero", "marzo", "abril", "mayo", "junio",
            "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"
        ];
        return `${parseInt(day, 10)} de ${months[parseInt(month, 10) - 1]} de ${year}`;
    };

    // Render tarjeta de turno
    const renderAppointmentCard = (a, i) => {
        const statusColor =
            a.status === "Confirmado"
                ? "bg-green-100 text-green-600"
                : a.status === "Pendiente"
                    ? "bg-yellow-100 text-yellow-600"
                    : "bg-gray-200";

        return (
            <div key={i} className="p-4 mb-3 border rounded-xl shadow-sm hover:bg-gray-50">
                <div className="flex items-center gap-2 text-blue-600">
                    <Clock size={18} />
                    <span className="font-semibold">{a.hour}</span>
                </div>

                <div className="flex items-center gap-2 mt-1 text-gray-700">
                    <User size={16} />
                    {a.name}
                </div>

                <div className="text-gray-500 text-sm">{a.type}</div>

                <span className={`text-xs px-2 py-1 rounded-md mt-2 inline-block ${statusColor}`}>
                    {a.status}
                </span>
            </div>
        );
    };

    // Render de un día del calendario
    const renderDay = (day, i) => {
        if (!day) return <div key={i}></div>;

        const fullDate = `${year}-${String(month + 1).padStart(2, "0")}-${String(day).padStart(2, "0")}`;
        const count = appointments[fullDate]?.length || 0;

        const bg =
            count === 0
                ? "bg-white"
                : count < 10
                    ? "bg-blue-50 text-blue-700"
                    : "bg-red-50 text-red-700";

        const border =
            selectedDate === fullDate
                ? "border-2 border-blue-500"
                : "border border-gray-200";

        return (
            <button
                key={i}
                onClick={() => setSelectedDate(fullDate)}
                className={`h-20 rounded-xl flex flex-col items-center justify-center text-sm ${bg} ${border} hover:opacity-90`}
            >
                <span className="font-medium">{day}</span>
                {count > 0 && <span>{count} turnos</span>}
            </button>
        );
    };

    return (
        <div className="p-6 space-y-6">
            {/* Header */}
            <div className="flex justify-between items-center">
                <h1 className="text-2xl font-semibold">Calendario de Turnos</h1>
                <button className="flex items-center gap-2 px-4 py-2 rounded-lg bg-blue-600 text-white hover:bg-blue-700 transition">
                    <Plus size={18} />
                    Nuevo Turno
                </button>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">

                {/* CALENDARIO */}
                <div className="md:col-span-2">
                    <div className="bg-white p-6 rounded-xl shadow-sm border">
                        {/* Selector mes */}
                        <div className="flex justify-between items-center mb-4">
                            <button onClick={() => setCurrentMonth(new Date(year, month - 1, 1))}>
                                <ChevronLeft className="text-gray-500" />
                            </button>

                            <h2 className="text-lg font-medium">
                                {currentMonth.toLocaleString("es-ES", { month: "long" })} {year}
                            </h2>

                            <button onClick={() => setCurrentMonth(new Date(year, month + 1, 1))}>
                                <ChevronRight className="text-gray-500" />
                            </button>
                        </div>

                        {/* Días */}
                        <div className="grid grid-cols-7 gap-2">
                            {["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb"].map((d) => (
                                <div key={d} className="text-center text-gray-500 text-sm">{d}</div>
                            ))}

                            {days.map((day, i) => renderDay(day, i))}
                        </div>
                    </div>
                </div>

                {/* LISTA DE TURNOS */}
                <div className="border rounded-xl p-4 bg-white shadow-sm">
                    {!selectedDate ? (
                        <div className="text-gray-500 text-center mt-10">
                            Seleccione un día del calendario para ver los turnos programados
                        </div>
                    ) : (
                        <div>
                                <h3 className="font-semibold text-lg mb-4">
                                    Turnos del {formatDate(selectedDate)}
                                </h3>

                            {appointments[selectedDate]?.map(renderAppointmentCard)}
                        </div>
                    )}
                </div>

            </div>
        </div>
    );
}
