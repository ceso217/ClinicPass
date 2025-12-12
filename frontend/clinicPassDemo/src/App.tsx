import React from 'react';
import { AuthProvider, useAuth } from './contexts/AuthContext';
import { Login } from './components/Login';
import { DashboardAdmin } from './components/DashboardAdmin';
import { DashboardProfesional } from './components/DashboardProfesional';
import { Pacientes } from './components/Pacientes';
import { Calendario } from './components/Calendario';
import { Historiales } from './components/Historiales';
import { Profesionales } from './components/Profesionales';
import { ProtectedRoute } from './components/ProtectedRoute';
import { Layout } from './components/Layout';

// Componente Dashboard que redirige según el rol
const DashboardRouter: React.FC = () => {
  const { isAdmin, isProfesional } = useAuth();

  if (isAdmin) {
    return <DashboardAdmin />;
  }

  if (isProfesional) {
    return <DashboardProfesional />;
  }

  return <Navigate to="/login" replace />;
};

// Componente para rutas con layout
const ProtectedLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <ProtectedRoute>
      <Layout>{children}</Layout>
    </ProtectedRoute>
  );
};

export default function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          {/* Ruta pública - Login */}
          <Route path="/login" element={<Login />} />

          {/* Rutas protegidas con Layout */}
          <Route
            path="/dashboard"
            element={
              <ProtectedLayout>
                <DashboardRouter />
              </ProtectedLayout>
            }
          />

          <Route
            path="/pacientes"
            element={
              <ProtectedLayout>
                <Pacientes />
              </ProtectedLayout>
            }
          />

          <Route
            path="/calendario"
            element={
              <ProtectedLayout>
                <Calendario />
              </ProtectedLayout>
            }
          />

          <Route
            path="/historiales"
            element={
              <ProtectedLayout>
                <Historiales />
              </ProtectedLayout>
            }
          />

          <Route
            path="/profesionales"
            element={
              <ProtectedLayout>
                <ProtectedRoute allowedRoles={[1]}>
                  <Profesionales />
                </ProtectedRoute>
              </ProtectedLayout>
            }
          />

          <Route
            path="/reportes"
            element={
              <ProtectedLayout>
                <ProtectedRoute allowedRoles={[1]}>
                  <div className="p-8">
                    <h1>Reportes</h1>
                    <p className="text-gray-600 mt-2">Solo accesible para administradores</p>
                  </div>
                </ProtectedRoute>
              </ProtectedLayout>
            }
          />

          <Route
            path="/configuracion"
            element={
              <ProtectedLayout>
                <div className="p-8">
                  <h1>Configuración</h1>
                  <p className="text-gray-600 mt-2">Módulo en desarrollo</p>
                </div>
              </ProtectedLayout>
            }
          />

          {/* Ruta por defecto */}
          <Route path="/" element={<Navigate to="/dashboard" replace />} />

          {/* Ruta 404 */}
          <Route 
            path="*" 
            element={
              <div className="min-h-screen flex items-center justify-center bg-gray-50">
                <div className="text-center">
                  <h1 className="text-gray-900 mb-4">404 - Página no encontrada</h1>
                  <a href="/" className="text-indigo-600 hover:text-indigo-700">
                    Volver al inicio
                  </a>
                </div>
              </div>
            } 
          />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}