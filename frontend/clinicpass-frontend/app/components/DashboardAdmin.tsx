'use client'
import React, { useState, useEffect } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { 
  Users, 
  Calendar, 
  FileText, 
  UserCog, 
  TrendingUp,
  Clock,
  CheckCircle,
  AlertCircle
} from 'lucide-react';
import PerformanceCard from './PerformanceCard';

interface DashboardStats {
  totalPacientes: number;
  turnosHoy: number;
  profesionalesActivos: number;
  fichasPendientes: number;
  turnosProgramados: number;
  turnosCompletados: number;
}

export const DashboardAdmin: React.FC = () => {
  const { user } = useAuth();
  const [stats, setStats] = useState<DashboardStats>({
    totalPacientes: 0,
    turnosHoy: 0,
    profesionalesActivos: 0,
    fichasPendientes: 0,
    turnosProgramados: 0,
    turnosCompletados: 0,
  });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // TODO: Reemplazar con llamadas reales a tu API
    const fetchStats = async () => {
      try {
        // Simular llamada a API
        setTimeout(() => {
          setStats({
            totalPacientes: 248,
            turnosHoy: 12,
            profesionalesActivos: 8,
            fichasPendientes: 5,
            turnosProgramados: 34,
            turnosCompletados: 156,
          });
          setLoading(false);
        }, 1000);
      } catch (error) {
        console.error('Error al cargar estadísticas:', error);
        setLoading(false);
      }
    };

    fetchStats();
  }, []);

  const StatCard: React.FC<{
    icon: React.ReactNode;
    title: string;
    value: string | number;
    bgColor: string;
    iconColor: string;
  }> = ({ icon, title, value, bgColor, iconColor }) => (
    <div className="bg-white rounded-xl shadow-md p-6 hover:shadow-lg transition">
      <div className="flex items-start justify-between">
        <div>
          <p className="text-gray-600 mb-1">{title}</p>
          <p className="text-gray-900">{loading ? '...' : value}</p>
        </div>
        <div className={`${bgColor} ${iconColor} p-3 rounded-lg`}>
          {icon}
        </div>
      </div>
    </div>
  );

  const QuickActionCard: React.FC<{
    icon: React.ReactNode;
    title: string;
    description: string;
    onClick: () => void;
  }> = ({ icon, title, description, onClick }) => (
    <button
      onClick={onClick}
      className="bg-white rounded-xl shadow-md p-6 hover:shadow-lg hover:scale-105 transition text-left w-full"
    >
      <div className="flex items-start gap-4">
        <div className="bg-indigo-100 text-indigo-600 p-3 rounded-lg flex-shrink-0">
          {icon}
        </div>
        <div>
          <h3 className="text-gray-900 mb-1">{title}</h3>
          <p className="text-gray-600">{description}</p>
        </div>
      </div>
    </button>
  );

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <div className="bg-white border-b border-gray-200 px-8 py-6">
        <h1 className="text-gray-900">Panel de Administración</h1>
        <p className="text-gray-600 mt-1">
          Bienvenido/a, {user?.nombreCompleto}
        </p>
      </div>

      <div className="p-8">
        {/* Estadísticas principales */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
          <StatCard
            icon={<Users className="w-6 h-6" />}
            title="Total Pacientes"
            value={stats.totalPacientes}
            bgColor="bg-blue-100"
            iconColor="text-blue-600"
          />
          <StatCard
            icon={<Calendar className="w-6 h-6" />}
            title="Turnos Hoy"
            value={stats.turnosHoy}
            bgColor="bg-green-100"
            iconColor="text-green-600"
          />
          <StatCard
            icon={<UserCog className="w-6 h-6" />}
            title="Profesionales Activos"
            value={stats.profesionalesActivos}
            bgColor="bg-purple-100"
            iconColor="text-purple-600"
          />
          <StatCard
            icon={<FileText className="w-6 h-6" />}
            title="Fichas Pendientes"
            value={stats.fichasPendientes}
            bgColor="bg-orange-100"
            iconColor="text-orange-600"
          />
        </div>

        {/* Gráfico resumen */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6 mb-8">
          <div className="bg-white rounded-xl shadow-md p-6 lg:col-span-2">
            <div className="flex items-center justify-between mb-6">
              <h2 className="text-gray-900">Estado de Turnos</h2>
              <select className="border border-gray-300 rounded-lg px-3 py-2">
                <option>Última semana</option>
                <option>Último mes</option>
                <option>Último trimestre</option>
              </select>
            </div>
            
            <div className="space-y-4">
              <div className="flex items-center justify-between">
                <div className="flex items-center gap-3">
                  <Clock className="w-5 h-5 text-yellow-500" />
                  <span className="text-gray-700">Programados</span>
                </div>
                <span className="text-gray-900">{stats.turnosProgramados}</span>
              </div>
              
              <div className="w-full bg-gray-200 rounded-full h-2">
                <div 
                  className="bg-yellow-500 h-2 rounded-full" 
                  style={{ width: '45%' }}
                ></div>
              </div>

              <div className="flex items-center justify-between">
                <div className="flex items-center gap-3">
                  <CheckCircle className="w-5 h-5 text-green-500" />
                  <span className="text-gray-700">Completados</span>
                </div>
                <span className="text-gray-900">{stats.turnosCompletados}</span>
              </div>
              
              <div className="w-full bg-gray-200 rounded-full h-2">
                <div 
                  className="bg-green-500 h-2 rounded-full" 
                  style={{ width: '78%' }}
                ></div>
              </div>
            </div>
          </div>
          <PerformanceCard></PerformanceCard>
        </div>
          {/* <div className="bg-gradient-to-br from-indigo-500 to-purple-600 rounded-xl shadow-md p-6 text-white">
            <TrendingUp className="w-8 h-8 mb-4" />
            <h3 className="mb-2">Rendimiento</h3>
            <p className="mb-4 opacity-90">
              El sistema está funcionando de manera óptima
            </p>
            <div className="flex items-center gap-2">
              <div className="w-2 h-2 bg-green-400 rounded-full animate-pulse"></div>
              <span>Sistema operativo</span>
            </div>
          </div>
        </div> */}

        {/* Acciones rápidas */}
        

        
        <h2 className="text-gray-900 mb-4">Acciones Rápidas</h2>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          <QuickActionCard
            icon={<Users className="w-6 h-6" />}
            title="Gestionar Pacientes"
            description="Ver, agregar o editar pacientes"
            onClick={() => console.log('Navegar a pacientes')}
          />
          <QuickActionCard
            icon={<Calendar className="w-6 h-6" />}
            title="Ver Calendario"
            description="Administrar turnos y citas"
            onClick={() => console.log('Navegar a calendario')}
          />
          <QuickActionCard
            icon={<UserCog className="w-6 h-6" />}
            title="Administrar Profesionales"
            description="Gestionar personal médico"
            onClick={() => console.log('Navegar a profesionales')}
          />
          <QuickActionCard
            icon={<FileText className="w-6 h-6" />}
            title="Historiales Clínicos"
            description="Acceder a registros médicos"
            onClick={() => console.log('Navegar a historiales')}
          />
          <QuickActionCard
            icon={<AlertCircle className="w-6 h-6" />}
            title="Fichas Pendientes"
            description="Revisar fichas sin completar"
            onClick={() => console.log('Navegar a fichas pendientes')}
          />
          <QuickActionCard
            icon={<TrendingUp className="w-6 h-6" />}
            title="Reportes"
            description="Generar informes y estadísticas"
            onClick={() => console.log('Navegar a reportes')}
          />
        </div>
      </div>
    </div>

  );
};
