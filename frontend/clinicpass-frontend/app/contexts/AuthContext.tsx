"use client";
import React, {
  createContext,
  useContext,
  useState,
  useEffect,
  ReactNode,
} from "react";
import { mockLogin } from "../data/mockUsers";
import { jwtDecode } from "jwt-decode";

// Variable para activar/desactivar modo mock
const USE_MOCK_AUTH = false; // Cambiar a false cuando tengas el backend listo

interface TokenPayload {
  role: string; // Por ejemplo, el ID del rol
  sub: string; // Subject (generalmente el ID del usuario)
  // ... cualquier otro campo que envíe el backend
}
const decodeToken = (token: string): TokenPayload | null => {//Decodificador del token
  try {
    return jwtDecode<TokenPayload>(token);
  } catch (error) {
    console.error("Error decodificando el token:", error);
    return null;
  }
}; 

interface User {
  id: number;
  nombreCompleto: string;
  dni: string;
  username: string;
  correo: string;
  especialidad?: string;
  rol: number; // 1 = Admin, 2 = Profesional
  telefono?: string;
}

interface AuthContextType {
  user: User | null;
  token: string | null;
  login: (username: string, password: string) => Promise<void>;
  logout: () => void;
  isAuthenticated: boolean;
  isAdmin: boolean;
  isProfesional: boolean;
  loading: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth debe ser usado dentro de AuthProvider");
  }
  return context;
};

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [decodedPayload, setDecodedPayload] = useState<TokenPayload | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Recuperar token y usuario del localStorage al iniciar
    const storedToken = localStorage.getItem("clinicpass_token");
    const storedUser = localStorage.getItem("clinicpass_user");

    if (storedToken && storedUser) {
      setToken(storedToken);
      setUser(JSON.parse(storedUser));

      // Decodificar el token al inicio
      setDecodedPayload(decodeToken(storedToken));
    }
    setLoading(false);
  }, []);

  const login = async (username: string, password: string) => {
    try {
      // Modo MOCK para testing
      if (USE_MOCK_AUTH) {
        // Simular delay de red
        await new Promise((resolve) => setTimeout(resolve, 800));

        const result = mockLogin(username, password);

        if (!result) {
          throw new Error("Credenciales incorrectas");
        }

        // Guardar token y usuario
        setToken(result.token);
        setUser(result.user);
        localStorage.setItem("clinicpass_token", result.token);
        localStorage.setItem("clinicpass_user", JSON.stringify(result.user));

        return;
      }

      // Modo PRODUCCIÓN - API real
      // TODO: Reemplazar con la URL de tu API .NET
      const response = await fetch(
        `${process.env.NEXT_PUBLIC_API_URL}/api/Auth/login`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ username, password }),
        }
      );

      if (!response.ok) {
        throw new Error("Credenciales incorrectas");
      }

      const data = await response.json();

      // Guardar token y usuario
      setToken(data.token);
      setUser(data.user);
      // Decodificar y guardar el payload aquí
      setDecodedPayload(decodeToken(data.token));

      localStorage.setItem("clinicpass_token", data.token);
      localStorage.setItem("clinicpass_user", JSON.stringify(data.user));
      console.log("rol: "+ decodedPayload?.role)
    } catch (error) {
      throw error;
    }
  };

  const logout = () => {
    setUser(null);
    setToken(null);
    setDecodedPayload(null); // Limpiar el payload al cerrar sesión
    localStorage.removeItem("clinicpass_token");
    localStorage.removeItem("clinicpass_user");

    window.location.href = "/login";
  };

  const value: AuthContextType = {
    user,
    loading,
    token,
    login,
    logout,
    isAuthenticated: !!token && !!user,
    isAdmin: decodedPayload?.role === "Admin",
    isProfesional: decodedPayload?.role === "Profesional",
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};
