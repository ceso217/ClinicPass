import React, { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { mockLogin } from '../data/mockUsers';

// Variable para activar/desactivar modo mock
const USE_MOCK_AUTH = true; // Cambiar a false cuando tengas el backend listo

interface User {
  id: number;
  nombreCompleto: string;
  dni: string;
  correo: string;
  especialidad?: string;
  rol: number; // 1 = Admin, 2 = Profesional
  telefono?: string;
}

interface AuthContextType {
  user: User | null;
  token: string | null;
  login: (correo: string, password: string) => Promise<void>;
  logout: () => void;
  isAuthenticated: boolean;
  isAdmin: boolean;
  isProfesional: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth debe ser usado dentro de AuthProvider');
  }
  return context;
};

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {
    // Recuperar token y usuario del localStorage al iniciar
    const storedToken = localStorage.getItem('clinicpass_token');
    const storedUser = localStorage.getItem('clinicpass_user');
    
    if (storedToken && storedUser) {
      setToken(storedToken);
      setUser(JSON.parse(storedUser));
    }
  }, []);

  const login = async (correo: string, password: string) => {
    try {
      // Modo MOCK para testing
      if (USE_MOCK_AUTH) {
        // Simular delay de red
        await new Promise(resolve => setTimeout(resolve, 800));
        
        const result = mockLogin(correo, password);
        
        if (!result) {
          throw new Error('Credenciales incorrectas');
        }

        // Guardar token y usuario
        setToken(result.token);
        setUser(result.user);
        localStorage.setItem('clinicpass_token', result.token);
        localStorage.setItem('clinicpass_user', JSON.stringify(result.user));
        
        return;
      }

      // Modo PRODUCCIÓN - API real
      // TODO: Reemplazar con la URL de tu API .NET
      const response = await fetch('http://localhost:5000/api/auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ correo, password }),
      });

      if (!response.ok) {
        throw new Error('Credenciales incorrectas');
      }

      const data = await response.json();
      
      // Guardar token y usuario
      setToken(data.token);
      setUser(data.user);
      localStorage.setItem('clinicpass_token', data.token);
      localStorage.setItem('clinicpass_user', JSON.stringify(data.user));
    } catch (error) {
      throw error;
    }
  };

  const logout = () => {
    setUser(null);
    setToken(null);
    localStorage.removeItem('clinicpass_token');
    localStorage.removeItem('clinicpass_user');
  };

  const value: AuthContextType = {
    user,
    token,
    login,
    logout,
    isAuthenticated: !!token && !!user,
    isAdmin: user?.rol === 1,
    isProfesional: user?.rol === 2,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};