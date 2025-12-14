'use client'
import React, { useState } from 'react';
import Image from 'next/image';
import { useAuth } from '../contexts/AuthContext';
// import { useNavigate, useLocation } from 'react-router-dom';
import { useRouter, usePathname } from 'next/navigation';
import { 
  LayoutDashboard,
  Users, 
  Calendar, 
  FileText, 
  UserCog,
  Settings,
  LogOut,
  ChevronLeft,
  ChevronRight,
  TrendingUp
} from 'lucide-react';
import { ThemeToggle } from './ThemeToggle';

interface MenuItem {
  icon: React.ReactNode;
  label: string;
  path: string;
  roles: number[]; // 1 = Admin, 2 = Profesional
}

export const Sidebar: React.FC = () => {
  const { user, logout, isAdmin, isProfesional } = useAuth();
  // const navigate = useNavigate();
  // const location = useLocation();
  const router = useRouter(); 
  const pathname = usePathname();

  const [collapsed, setCollapsed] = useState(false);

  const menuItems: MenuItem[] = [
    {
      icon: <LayoutDashboard className="w-5 h-5" />,
      label: 'Dashboard',
      path: '/dashboard',
      roles: [1, 2],
    },
    {
      icon: <Users className="w-5 h-5" />,
      label: 'Pacientes',
      path: '/pacientes',
      roles: [1, 2],
    },
    {
      icon: <Calendar className="w-5 h-5" />,
      label: 'Calendario',
      path: '/calendario',
      roles: [1, 2],
    },
    {
      icon: <FileText className="w-5 h-5" />,
      label: 'Historiales',
      path: '/historiales',
      roles: [1, 2],
    },
    {
      icon: <UserCog className="w-5 h-5" />,
      label: 'Profesionales',
      path: '/profesionales',
      roles: [1], // Solo Admin
    },
    {
      icon: <TrendingUp className="w-5 h-5" />,
      label: 'Reportes',
      path: '/reportes',
      roles: [1], // Solo Admin
    },
  ];

  const filteredMenuItems = menuItems.filter(item => 
    item.roles.includes(user?.rol || 0)
  );

  const handleLogout = () => {
    logout();
    router.push('/login');
  };

const isActive = (path: string) => pathname === path;

  return (
    <div 
      className={`bg-white border-r border-gray-200 h-screen flex flex-col transition-all duration-300 ${
        collapsed ? 'w-20' : 'w-64'
      }`}
    >
      {/* Header */}
      <div className="p-6 border-b border-gray-200 flex items-center justify-between">
        {!collapsed && (
          <div>
            <Image
              src="/logo_reduced_1024x1024.jpg" 
              alt="ClinicPass"
              width={150} 
              height={40}
              className=""
            />
            <p className="text-gray-600 mt-1">
              {isAdmin ? 'Administrador' : 'Profesional'}
            </p>
          </div>
        )}
        <button
          onClick={() => setCollapsed(!collapsed)}
          className="p-2 hover:bg-gray-100 rounded-lg transition"
        >
          {collapsed ? (
            <ChevronRight className="w-5 h-5 text-gray-600" />
          ) : (
            <ChevronLeft className="w-5 h-5 text-gray-600" />
          )}
        </button>
      </div>

      {/* User info */}
      {!collapsed && (
        <div className="p-6 border-b border-gray-200">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 bg-indigo-100 rounded-full flex items-center justify-center">
              <span className="text-indigo-700">
                {user?.nombreCompleto.charAt(0).toUpperCase()}
              </span>
            </div>
            <div className="flex-1 min-w-0">
              <p className="text-gray-900 truncate">{user?.nombreCompleto}</p>
              <p className="text-gray-600 truncate">{user?.correo}</p>
            </div>
          </div>
        </div>
      )}

      {/* Menu items */}
      <nav className="flex-1 p-4 overflow-y-auto">
        <div className="space-y-1">
          {filteredMenuItems.map((item) => (
            <button
              key={item.path}
              onClick={() => router.push(item.path)}
              className={`w-full flex items-center gap-3 px-4 py-3 rounded-lg transition ${
                isActive(item.path)
                  ? 'bg-indigo-50 text-indigo-700'
                  : 'text-gray-700 hover:bg-gray-100'
              } ${collapsed ? 'justify-center' : ''}`}
              title={collapsed ? item.label : ''}
            >
              {item.icon}
              {!collapsed && <span>{item.label}</span>}
            </button>
          ))}
        </div>
      </nav>

      {/* Footer actions */}
      <div className="p-4 border-t border-gray-200 space-y-1">
        <button
          onClick={() => router.push('/configuracion')}
          className={`w-full flex items-center gap-3 px-4 py-3 rounded-lg text-gray-700 hover:bg-gray-100 transition ${
            collapsed ? 'justify-center' : ''
          }`}
          title={collapsed ? 'Configuración' : ''}
        >
          <Settings className="w-5 h-5" />
          {!collapsed && <span>Configuración</span>}
        </button>
        <ThemeToggle></ThemeToggle>
        
        <button
          onClick={handleLogout}
          className={`w-full flex items-center gap-3 px-4 py-3 rounded-lg text-red-700 hover:bg-red-50 transition ${
            collapsed ? 'justify-center' : ''
          }`}
          title={collapsed ? 'Cerrar Sesión' : ''}
        >
          <LogOut className="w-5 h-5" />
          {!collapsed && <span>Cerrar Sesión</span>}
        </button>
      </div>
    </div>
  );
};

