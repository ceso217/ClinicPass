  'use client'
  import React, { useEffect } from 'react';
  // 1. Reemplazamos 'react-router-dom' por 'next/navigation'
  import { useRouter } from 'next/navigation'; 
  import { useAuth } from '../contexts/AuthContext';

  interface ProtectedRouteProps {
    children: React.ReactNode;
    allowedRoles?: number[]; // 1 = Admin, 2 = Profesional
  }
  export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ 
    children, 
    allowedRoles 
  }) => {
    const { isAuthenticated, user, loading } = useAuth();
    const router = useRouter();
    const userRole = user?.rol;
    useEffect(() => {
      // Detenemos la ejecución si el estado de autenticación aún se está cargando
      if (loading) {
        return;
      }
      // 2. Lógica de redirección: Si NO está autenticado, redirigir a /login
      if (!isAuthenticated) {
        router.push('/login');
        return; 
      }
      // 3. Lógica de permisos: Si hay roles requeridos Y el rol del usuario NO está permitido, redirigir a /dashboard
      const isForbidden = allowedRoles && userRole && !allowedRoles.includes(userRole);
      if (isForbidden) {
        router.push('/dashboard');
        return; 
      }
    }, [isAuthenticated, loading, userRole, allowedRoles, router]);
    // 4. Manejo del estado de Carga/Redirección
    // Si está cargando O el usuario no está autenticado O el acceso está prohibido, 
    // mostramos un estado de carga o null mientras se realiza la redirección.
  const isAccessDeniedOrLoading = loading || !isAuthenticated || (allowedRoles && userRole && !allowedRoles.includes(userRole));
  
  if (isAccessDeniedOrLoading) {
    // Puedes devolver un spinner, un esqueleto, o simplemente 'null'
    return null; // O un componente de carga: <div>Cargando...</div>
    }
   // 5. Si pasa todas las comprobaciones, muestra el contenido hijo
    return <>{children}</>;
  };