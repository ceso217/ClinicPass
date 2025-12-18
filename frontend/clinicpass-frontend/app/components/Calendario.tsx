'use client'
import React, { useState, useMemo, useCallback, useEffect } from 'react';
import { ChevronLeft, ChevronRight, Plus, Clock, User, Filter } from 'lucide-react';
import { mockTurnos, mockProfesionales, mockPacientes} from '../data/mockData';
import {Profesional} from '../types/profesional';
import {Turno} from '../types/turno';

import { TurnoModal } from './modals/TurnoModal';
import { createTurno, getTurnos } from '../hooks/turnosApi';
import { getProfesionales } from '../hooks/profesionalesApi';
import { Paciente } from '../types/paciente';
import { getPacientes } from '../hooks/pacientesApi';




export const Calendario: React.FC = () => {
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null);
  const [currentDate, setCurrentDate] = useState(new Date());
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  //**************** */
  //TURNOS
  //**************** */
  const [turnos, setTurnos] = useState<Turno[]>([]);
  const [profesionales, setProfesionales] = useState<Profesional[]>([]);
  const [pacientes, setPacientes] = useState<Paciente[]>([]);
  const [filterProfesional, setFilterProfesional] = useState<number | null>(null);
  const [filterEstado, setFilterEstado] = useState<string>('');

  //Modal
  const [showTurnoModal, setShowTurnoModal] = useState(false);
  const [selectedTurno, setSelectedTurno] = useState<Turno | null>(null);

  //fecha
  const year = currentDate.getFullYear();
  const month = currentDate.getMonth();

  // Obtener primer y último día del mes
  const firstDayOfMonth = new Date(year, month, 1);
  const lastDayOfMonth = new Date(year, month + 1, 0);
  const firstDayWeekday = firstDayOfMonth.getDay();
  const daysInMonth = lastDayOfMonth.getDate();

  // Función para cargar los datos Turnos (usada en useEffect)
  const fetchTurnos= useCallback(async () => {
      setIsLoading(true);
      try {
          const data: Turno[] = await getTurnos(); 
          setTurnos(data);
          const profesionalesList: Profesional[] = await getProfesionales();
          setProfesionales(profesionalesList);
          const pacientesList: Paciente[] = await getPacientes();
          setPacientes(pacientesList);
          //setFilterProfesional();
          //setFilterEstado();
      } catch (err) {
          console.error("Error al cargar Turnos", err);
          setError("No se pudieron cargar los turnos. Intente de nuevo.");
      } finally {
          setIsLoading(false);
      }
  }, []);

  useEffect(() => {
      fetchTurnos();
  }, [fetchTurnos]);




  // Navegar meses
  const handlePrevMonth = () => {
    setCurrentDate(new Date(year, month - 1, 1));
  };

  const handleNextMonth = () => {
    setCurrentDate(new Date(year, month + 1, 1));
  };

  // Obtener turnos por fecha
  const getTurnosPorFecha = useCallback((fecha: Date) => {
    const fechaStr = fecha.toISOString().split('T')[0];
    
    return turnos.filter((t) => {
        const coincideFecha = t.fecha === fechaStr;
        const coincideProfesional = filterProfesional 
            ? t.profesionalId === filterProfesional 
            : true;
        const coincideEstado = filterEstado 
            ? t.estado === filterEstado 
            : true;

        return coincideFecha && coincideProfesional && coincideEstado;
    });
}, [turnos, filterProfesional, filterEstado]);

  // Turnos del día seleccionado
  const turnosDelDia = useMemo(() => {
    if (!selectedDate) return [];
    return getTurnosPorFecha(selectedDate);
  }, [selectedDate, filterProfesional, filterEstado, turnos]);

  // Generar días del calendario
  const calendarDays = [];
  
  // Días vacíos del mes anterior
  for (let i = 0; i < firstDayWeekday; i++) {
    calendarDays.push(null);
  }
  
  // Días del mes actual
  for (let day = 1; day <= daysInMonth; day++) {
    calendarDays.push(day);
  }

  const getEstadoColor = (estado: string) => {
    switch (estado) {
      case 'Completado':
        return 'bg-green-100 text-green-700';
      case 'Confirmado':
        return 'bg-blue-100 text-blue-700';
      case 'Programado':
        return 'bg-yellow-100 text-yellow-700';
      case 'Cancelado':
        return 'bg-red-100 text-red-700';
      default:
        return 'bg-gray-100 text-gray-700';
    }
  };

  const isDayWithTurnos = (day: number) => {
    const fecha = new Date(year, month, day);
    return getTurnosPorFecha(fecha).length > 0;
  };

  const isSelectedDay = (day: number) => {
    if (!selectedDate) return false;
    return (
      selectedDate.getDate() === day &&
      selectedDate.getMonth() === month &&
      selectedDate.getFullYear() === year
    );
  };

  const isToday = (day: number) => {
    const today = new Date();
    return (
      today.getDate() === day &&
      today.getMonth() === month &&
      today.getFullYear() === year
    );
  };

  const handleDayClick = (day: number) => {
    setSelectedDate(new Date(year, month, day));
  };
  
  const handleAbrirModalTurno = () => {
    setSelectedTurno(null);
    setShowTurnoModal(true);
  };
  const handleSaveTurno = async (turnoData: Partial<Turno>) => {
    const maxId = turnos.length > 0 ? Math.max(...turnos.map(t => t.id)) : 0;

    // Buscar los nombres basados en los IDs enviados por el modal
    const profesionalEncontrado = profesionales.find(p => Number(p.id) === turnoData.profesionalId);
    const pacienteEncontrado = pacientes.find(p => p.idPaciente === turnoData.pacienteId);
    
    if (turnoData.id) {

     setTurnos(turnos.map(t => t.id === turnoData.id ? { 
      ...t, 
      ...turnoData,
      profesionalNombre: profesionalEncontrado?.nombreCompleto || t.profesionalNombre,
      pacienteNombre: pacienteEncontrado?.nombreCompleto || t.pacienteNombre 
      } as Turno : t));

    } else {
      const nuevoTurno : Turno = {
        id: maxId + 1, 
        pacienteId: Number(turnoData.pacienteId )|| 0,
        pacienteNombre: pacienteEncontrado?.nombreCompleto || 'Paciente no encontrado',
        profesionalId: Number(turnoData.profesionalId) || 0, 
        profesionalNombre: profesionalEncontrado?.nombreCompleto || 'Profesional no encontrado',
        fecha: turnoData.fecha || '',
        hora: turnoData.hora || '',
        estado: turnoData.estado || 'Pendiente', 
        fichaCreada: false,
      };
      try{
         await createTurno(nuevoTurno);
        alert('Turno Creado')
      }catch(error){
        throw new Error("No se creo el turno")
      }
      setTurnos([...turnos, nuevoTurno]);

    }
    handleCloseModalTurno();
  }

  const handleCloseModalTurno = async () => {
        setShowTurnoModal(false);
        setSelectedTurno(null); 

        const profesionalesList: Profesional[] = await getProfesionales();
        setProfesionales(profesionalesList);
        const pacientesList: Paciente[] = await getPacientes();
        setPacientes(pacientesList);
    };
  const handleAbrirModalTurnoEditar = (turnoParaEditar: Turno | null = null) => {
      setSelectedTurno(turnoParaEditar); // Establece el turno para edición (o null para creación)
      setShowTurnoModal(true);
  };

  const monthNames = [
    'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
    'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
  ];

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="bg-white border-b border-gray-200 px-8 py-6">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-gray-900">Calendario de Turnos</h1>
            <p className="text-gray-600 mt-1">
              {monthNames[month]} {year}
            </p>
          </div>
          <button
            onClick={(handleAbrirModalTurno)}
            className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
          >
            <Plus className="w-5 h-5" />
            Agendar Turno
          </button>
        </div>
      </div>

      <div className="p-8">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Calendario */}
          <div className="lg:col-span-2">
            <div className="bg-white rounded-xl shadow-md p-6">
              {/* Header del calendario */}
              <div className="flex items-center justify-between mb-6">
                <h2 className="text-gray-900">
                  {monthNames[month]} {year}
                </h2>
                <div className="flex items-center gap-2">
                  <button
                    onClick={handlePrevMonth}
                    className="p-2 hover:bg-gray-100 rounded-lg transition"
                  >
                    <ChevronLeft className="w-5 h-5 text-gray-600" />
                  </button>
                  <button
                    onClick={handleNextMonth}
                    className="p-2 hover:bg-gray-100 rounded-lg transition"
                  >
                    <ChevronRight className="w-5 h-5 text-gray-600" />
                  </button>
                </div>
              </div>

              {/* Días de la semana */}
              <div className="grid grid-cols-7 gap-2 mb-2">
                {['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'].map((day) => (
                  <div key={day} className="text-center text-gray-600 py-2">
                    {day}
                  </div>
                ))}
              </div>

              {/* Días del mes */}
              <div className="grid grid-cols-7 gap-2">
                {calendarDays.map((day, index) => {
                  if (day === null) {
                    return <div key={`empty-${index}`} className="aspect-square" />;
                  }

                  const hasTurnos = isDayWithTurnos(day);
                  const selected = isSelectedDay(day);
                  const today = isToday(day);
                  const turnosCount = getTurnosPorFecha(new Date(year, month, day)).length;

                  return (
                    <button
                      key={day}
                      onClick={() => handleDayClick(day)}
                      className={`aspect-square p-2 rounded-lg transition relative ${
                        selected
                          ? 'bg-indigo-600 text-white'
                          : today
                          ? 'bg-indigo-100 text-indigo-900'
                          : 'hover:bg-gray-100 text-gray-900'
                      }`}
                    >
                      <span className="text-center block">{day}</span>
                      {hasTurnos && (
                        <span
                          className={`absolute bottom-1 left-1/2 transform -translate-x-1/2 text-xs px-2 py-0.5 rounded-full ${
                            selected ? 'bg-white text-indigo-600' : 'bg-indigo-600 text-white'
                          }`}
                        >
                          {turnosCount}
                        </span>
                      )}
                    </button>
                  );
                })}
              </div>

              {/* Leyenda */}
              <div className="mt-6 pt-6 border-t border-gray-200">
                <div className="flex items-center justify-center gap-6">
                  <div className="flex items-center gap-2">
                    <div className="w-4 h-4 bg-indigo-100 rounded"></div>
                    <span className="text-gray-700">Hoy</span>
                  </div>
                  <div className="flex items-center gap-2">
                    <div className="w-4 h-4 bg-indigo-600 rounded"></div>
                    <span className="text-gray-700">Seleccionado</span>
                  </div>
                </div>
              </div>
            </div>
          </div>

          {/* Panel lateral - Turnos del día */}
          <div className="space-y-6">
            {/* Filtros */}
            <div className="bg-white rounded-xl shadow-md p-6">
              <div className="flex items-center gap-2 mb-4">
                <Filter className="w-5 h-5 text-gray-600" />
                <h3 className="text-gray-900">Filtros</h3>
              </div>
              <div className="space-y-3">
                <select
                  value={filterProfesional || ''}
                  onChange={(e) => setFilterProfesional(e.target.value ? parseInt(e.target.value) : null)}
                  className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                >
                  <option value="">Todos los profesionales</option>
                  {profesionales.map((prof) => (
                    <option key={prof.id} value={prof.id}>
                      {prof.nombreCompleto}
                      </option>
                  ))}
                </select>
                <select
                  value={filterEstado}
                  onChange={(e) => setFilterEstado(e.target.value)}
                  className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                >
                  <option value="">Todos los estados</option>
                  <option value="Pendiente">Pendiente</option>
                  <option value="Confirmado">Confirmado</option>
                  <option value="Completado">Completado</option>
                  <option value="Cancelado">Cancelado</option>
                </select>
              </div>
            </div>

            {/* Turnos */}
            <div className="bg-white rounded-xl shadow-md overflow-hidden">
              <div className="p-6 border-b border-gray-200">
                <h3 className="text-gray-900">
                  {selectedDate
                    ? `Turnos del ${selectedDate.getDate()} de ${monthNames[selectedDate.getMonth()]}`
                    : 'Selecciona un día'}
                </h3>
                {selectedDate && (
                  <p className="text-gray-600 mt-1">
                    {turnosDelDia.length} turno{turnosDelDia.length !== 1 ? 's' : ''}
                  </p>
                )}
              </div>

              <div className="max-h-[500px] overflow-y-auto">
                {!selectedDate ? (
                  <div className="p-8 text-center text-gray-500">
                    Selecciona un día en el calendario para ver los turnos
                  </div>
                ) : turnosDelDia.length === 0 ? (
                  <div className="p-8 text-center text-gray-500">
                    No hay turnos para este día
                  </div>
                ) : (
                  <div className="divide-y divide-gray-200">
                    {turnosDelDia.map((turno) => (
                      <div
                        key={turno.id}
                        className="p-4 hover:bg-gray-50 transition cursor-pointer"
                        onClick={() => handleAbrirModalTurnoEditar(turno)}
                      >
                        <div className="flex items-start justify-between mb-2">
                          <div className="flex items-center gap-2">
                            <Clock className="w-4 h-4 text-indigo-600" />
                            <span className="text-gray-900">{turno.hora} hs</span>
                          </div>
                          <span className={`px-2 py-1 rounded-full text-xs ${getEstadoColor(turno.estado)}`}>
                            {turno.estado}
                          </span>
                        </div>
                        <p className="text-gray-900 mb-1">{turno.pacienteNombre}</p>
                        <div className="flex items-center gap-1 text-gray-600">
                          <User className="w-4 h-4" />
                          <span>{turno.profesionalNombre}</span>
                        </div>
                      </div>
                    ))}
                  </div>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>

       {/* Modal de Turno */}
      <TurnoModal
        isOpen={showTurnoModal}
        onClose={handleCloseModalTurno}
        onSave={handleSaveTurno}
        turno={selectedTurno}
        fechaPreseleccionada={selectedDate?.toISOString().split('T')[0]}
        profesionales={profesionales}
        pacientes={pacientes}
      />

    </div>
  );
};
