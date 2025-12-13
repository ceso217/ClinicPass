'use client'
import React, { useState, useEffect } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { useRouter } from 'next/navigation';
import { 
  Calendar, 
  Users, 
  Clock, 
  FileText,
  ChevronRight,
  CheckCircle,
  AlertCircle
} from 'lucide-react';

interface Turno {
  id: number;
  paciente: string;
  hora: string;
  estado: 'Programado' | 'Confirmado' | 'Completado';
  fichaCreada: boolean;
}

export const DashboardProfesional: React.FC = () => {
  const { user } = useAuth();
  const router = useRouter();
  const [turnosHoy, setTurnosHoy] = useState<Turno[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [showTurnoModal, setShowTurnoModal] = useState(false);
    const [selectedTurno, setSelectedTurno] = useState<Turno | null>(null);
  

  useEffect(() => {
    // TODO: Reemplazar con llamada real a tu API
    const fetchTurnos = async () => {
      try {
        // Simular llamada a API
        setTimeout(() => {
          setTurnosHoy([
            { id: 1, paciente: 'María González', hora: '09:00', estado: 'Completado', fichaCreada: true },
            { id: 2, paciente: 'Juan Pérez', hora: '10:00', estado: 'Confirmado', fichaCreada: false },
            { id: 3, paciente: 'Ana Martínez', hora: '11:30', estado: 'Confirmado', fichaCreada: false },
            { id: 4, paciente: 'Carlos Rodríguez', hora: '14:00', estado: 'Programado', fichaCreada: false },
            { id: 5, paciente: 'Lucía Fernández', hora: '15:30', estado: 'Programado', fichaCreada: false },
          ]);
          setLoading(false);
        }, 1000);
      } catch (error) {
        console.error('Error al cargar turnos:', error);
        setLoading(false);
      }
    };

    fetchTurnos();
  }, [selectedDate]);

  const getEstadoColor = (estado: string) => {
    switch (estado) {
      case 'Completado':
        return 'bg-green-100 text-green-700';
      case 'Confirmado':
        return 'bg-blue-100 text-blue-700';
      case 'Programado':
        return 'bg-yellow-100 text-yellow-700';
      default:
        return 'bg-gray-100 text-gray-700';
    }
  };

  const turnosCompletados = turnosHoy.filter(t => t.estado === 'Completado').length;
  const turnosPendientes = turnosHoy.filter(t => t.estado !== 'Completado').length;
  const fichasPendientes = turnosHoy.filter(t => t.estado === 'Completado' && !t.fichaCreada).length;

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <div className="bg-white border-b border-gray-200 px-8 py-6">
        <h1 className="text-gray-900">Mi Panel Profesional</h1>
        <p className="text-gray-600 mt-1">
          Bienvenido/a, Dr/a. {user?.nombreCompleto}
        </p>
        {user?.especialidad && (
          <p className="text-indigo-600 mt-1">{user.especialidad}</p>
        )}
      </div>

      <div className="p-8">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Columna principal - Turnos del día */}
          <div className="lg:col-span-2 space-y-6">
            {/* Resumen rápido */}
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
              <div className="bg-white rounded-xl shadow-md p-6">
                <div className="flex items-center gap-3 mb-2">
                  <Calendar className="w-5 h-5 text-blue-600" />
                  <span className="text-gray-600">Total Hoy</span>
                </div>
                <p className="text-gray-900">{loading ? '...' : turnosHoy.length}</p>
              </div>
              
              <div className="bg-white rounded-xl shadow-md p-6">
                <div className="flex items-center gap-3 mb-2">
                  <CheckCircle className="w-5 h-5 text-green-600" />
                  <span className="text-gray-600">Completados</span>
                </div>
                <p className="text-gray-900">{loading ? '...' : turnosCompletados}</p>
              </div>
              
              <div className="bg-white rounded-xl shadow-md p-6">
                <div className="flex items-center gap-3 mb-2">
                  <Clock className="w-5 h-5 text-orange-600" />
                  <span className="text-gray-600">Pendientes</span>
                </div>
                <p className="text-gray-900">{loading ? '...' : turnosPendientes}</p>
              </div>
            </div>

            {/* Lista de turnos */}
            <div className="bg-white rounded-xl shadow-md">
              <div className="p-6 border-b border-gray-200">
                <h2 className="text-gray-900">Turnos de Hoy</h2>
                <p className="text-gray-600 mt-1">
                  {new Date().toLocaleDateString('es-AR', { 
                    weekday: 'long', 
                    year: 'numeric', 
                    month: 'long', 
                    day: 'numeric' 
                  })}
                </p>
              </div>

              <div className="divide-y divide-gray-200">
                {loading ? (
                  <div className="p-8 text-center text-gray-500">
                    Cargando turnos...
                  </div>
                ) : turnosHoy.length === 0 ? (
                  <div className="p-8 text-center text-gray-500">
                    No hay turnos programados para hoy
                  </div>
                ) : (
                  turnosHoy.map((turno) => (
                    <div
                      key={turno.id}
                      className="p-6 hover:bg-gray-50 transition cursor-pointer"
                      onClick={() => console.log('Ver turno:', turno.id)}
                    >
                      <div className="flex items-center justify-between">
                        <div className="flex items-center gap-4">
                          <div className="bg-indigo-100 text-indigo-700 p-3 rounded-lg">
                            <Clock className="w-5 h-5" />
                          </div>
                          <div>
                            <h3 className="text-gray-900">{turno.paciente}</h3>
                            <p className="text-gray-600">{turno.hora} hs</p>
                          </div>
                        </div>
                        
                        <div className="flex items-center gap-3">
                          <span className={`px-3 py-1 rounded-full ${getEstadoColor(turno.estado)}`}>
                            {turno.estado}
                          </span>
                          
                          {turno.estado === 'Completado' && !turno.fichaCreada && (
                            <span className="flex items-center gap-1 text-orange-600 bg-orange-50 px-3 py-1 rounded-full">
                              <AlertCircle className="w-4 h-4" />
                              Crear ficha
                            </span>
                          )}
                          
                          <ChevronRight className="w-5 h-5 text-gray-400" />
                        </div>
                      </div>
                    </div>
                  ))
                )}
              </div>
            </div>
          </div>

          {/* Sidebar derecha */}
          <div className="space-y-6">
            {/* Fichas pendientes */}
            {fichasPendientes > 0 && (
              <div className="bg-orange-50 border border-orange-200 rounded-xl p-6">
                <div className="flex items-start gap-3">
                  <AlertCircle className="w-6 h-6 text-orange-600 flex-shrink-0" />
                  <div>
                    <h3 className="text-orange-900 mb-2">Fichas Pendientes</h3>
                    <p className="text-orange-800 mb-4">
                      Tienes {fichasPendientes} ficha{fichasPendientes !== 1 ? 's' : ''} de seguimiento pendiente{fichasPendientes !== 1 ? 's' : ''}
                    </p>
                    <button className="bg-orange-600 text-white px-4 py-2 rounded-lg hover:bg-orange-700 transition w-full">
                      Ver fichas
                    </button>
                  </div>
                </div>
              </div>
            )}

            {/* Calendario mini */}
            <div className="bg-white rounded-xl shadow-md p-6">
              <h3 className="text-gray-900 mb-4">Calendario</h3>
              <div className="bg-indigo-50 rounded-lg p-4 text-center mb-4">
                <p className="text-indigo-900">{selectedDate.getDate()}</p>
                <p className="text-indigo-700">
                  {selectedDate.toLocaleDateString('es-AR', { month: 'long' })}
                </p>
              </div>
              <button 
                className="w-full border border-indigo-600 text-indigo-600 px-4 py-2 rounded-lg hover:bg-indigo-50 transition"
                onClick={() => router.push('/calendario')}
              >
                Ver calendario completo
              </button>
            </div>

            {/* Acciones rápidas */}
            <div className="bg-white rounded-xl shadow-md p-6">
              <h3 className="text-gray-900 mb-4">Acciones Rápidas</h3>
              <div className="space-y-3">
                <button 
                  className="w-full flex items-center gap-3 p-3 rounded-lg hover:bg-gray-50 transition text-left"
                 onClick={() => router.push('/pacientes')}
                >
                  <Users className="w-5 h-5 text-indigo-600" />
                  <span className="text-gray-700">Mis Pacientes</span>
                </button>
                
                <button 
                  className="w-full flex items-center gap-3 p-3 rounded-lg hover:bg-gray-50 transition text-left"
                  onClick={() => router.push('/historiales')}
                >
                  <FileText className="w-5 h-5 text-indigo-600" />
                  <span className="text-gray-700">Historiales Clínicos</span>
                </button>
                
                <button 
                  className="w-full flex items-center gap-3 p-3 rounded-lg hover:bg-gray-50 transition text-left"
                  onClick={() => router.push('/calendario')}
                >
                  <Calendar className="w-5 h-5 text-indigo-600" />
                  <span className="text-gray-700">Agendar Turno</span>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
