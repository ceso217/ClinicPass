import React, { useState } from 'react';
import { useAuth } from '@/contexts/AuthContext';
import { Mail, Lock, AlertCircle } from 'lucide-react';
import { DevBanner } from './DevBanner';
import { useRouter } from 'next/navigation';


export const Login: React.FC = () => {
  const [correo, setCorreo] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const { login } = useAuth();
  const router = useRouter();
  
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      await login(correo, password);
      router.push('/dashboard');
    } catch (err) {
      setError('Credenciales incorrectas. Por favor, intente nuevamente.');
    } finally {
      setLoading(false);
    }
  };

  // Función para login rápido (solo para testing)
  const quickLogin = (email: string, pass: string) => {
    setCorreo(email);
    setPassword(pass);
  };

  return (
    <>
      <DevBanner />
      <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-blue-50 to-indigo-100 px-4">
        <div className="max-w-md w-full">
          {/* Logo y título */}
          <div className="text-center mb-8">
            <div className="inline-flex items-center justify-center w-16 h-16 bg-indigo-600 rounded-full mb-4">
              <svg
                className="w-8 h-8 text-white"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"
                />
              </svg>
            </div>
            <h1 className="text-indigo-900">ClinicPass</h1>
            <p className="text-gray-600 mt-2">Sistema de Gestión Clínica</p>
          </div>

          {/* Usuarios de prueba */}
          <div className="bg-yellow-50 border border-yellow-200 rounded-xl p-4 mb-6">
            <p className="text-yellow-900 mb-3">👨‍💻 Usuarios de Prueba:</p>
            <div className="space-y-2">
              <button
                type="button"
                onClick={() => quickLogin('admin@clinicpass.com', 'admin123')}
                className="w-full text-left bg-white px-3 py-2 rounded-lg hover:bg-yellow-100 transition border border-yellow-200"
              >
                <p className="text-gray-900">Admin</p>
                <p className="text-gray-600">admin@clinicpass.com / admin123</p>
              </button>
              <button
                type="button"
                onClick={() => quickLogin('profesional@clinicpass.com', 'prof123')}
                className="w-full text-left bg-white px-3 py-2 rounded-lg hover:bg-yellow-100 transition border border-yellow-200"
              >
                <p className="text-gray-900">Profesional</p>
                <p className="text-gray-600">profesional@clinicpass.com / prof123</p>
              </button>
            </div>
          </div>

          {/* Formulario */}
          <div className="bg-white rounded-2xl shadow-xl p-8">
            <h2 className="text-gray-900 mb-6">Iniciar Sesión</h2>

            {error && (
              <div className="mb-4 p-4 bg-red-50 border border-red-200 rounded-lg flex items-start gap-3">
                <AlertCircle className="w-5 h-5 text-red-600 flex-shrink-0 mt-0.5" />
                <p className="text-red-800">{error}</p>
              </div>
            )}

            <form onSubmit={handleSubmit} className="space-y-5">
              <div>
                <label htmlFor="correo" className="block text-gray-700 mb-2">
                  Correo Electrónico
                </label>
                <div className="relative">
                  <Mail className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                  <input
                    id="correo"
                    type="email"
                    value={correo}
                    onChange={(e) => setCorreo(e.target.value)}
                    className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition"
                    placeholder="correo@ejemplo.com"
                    required
                  />
                </div>
              </div>

              <div>
                <label htmlFor="password" className="block text-gray-700 mb-2">
                  Contraseña
                </label>
                <div className="relative">
                  <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                  <input
                    id="password"
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition"
                    placeholder="••••••••"
                    required
                  />
                </div>
              </div>

              <button
                type="submit"
                disabled={loading}
                className="w-full bg-indigo-600 text-white py-3 rounded-lg hover:bg-indigo-700 transition disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {loading ? 'Iniciando sesión...' : 'Iniciar Sesión'}
              </button>
            </form>

            <div className="mt-6 pt-6 border-t border-gray-200">
              <p className="text-gray-600 text-center">
                ¿Olvidaste tu contraseña?{' '}
                <button className="text-indigo-600 hover:text-indigo-700">
                  Recuperar acceso
                </button>
              </p>
            </div>
          </div>

          <p className="text-center text-gray-600 mt-8">
            &copy; 2025 ClinicPass - Devlights Bootcamp
          </p>
        </div>
      </div>
    </>
  );
};