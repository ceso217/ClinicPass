'use client'
import React, { useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { 
  User, 
  Lock, 
  Bell, 
  Palette, 
  Save,
  Mail,
  Phone,
  Eye,
  EyeOff,
  CheckCircle,
  Building
} from 'lucide-react';

export const Configuracion: React.FC = () => {
  const { user } = useAuth();
  const [activeTab, setActiveTab] = useState<'perfil' | 'seguridad' | 'notificaciones' | 'apariencia'>('perfil');
  const [successMessage, setSuccessMessage] = useState('');

  // Estado del perfil
  const [perfilData, setPerfilData] = useState({
    nombreCompleto: user?.nombreCompleto || '',
    correo: user?.correo || '',
    telefono: user?.telefono || '',
    especialidad: user?.especialidad || '',
    dni: user?.dni || '',
  });

  // Estado de contraseña
  const [passwordData, setPasswordData] = useState({
    actual: '',
    nueva: '',
    confirmar: '',
  });
  const [showPasswords, setShowPasswords] = useState({
    actual: false,
    nueva: false,
    confirmar: false,
  });

  // Estado de notificaciones
  const [notificaciones, setNotificaciones] = useState({
    emailTurnos: true,
    emailFichas: true,
    emailSistema: false,
    pushTurnos: true,
    pushRecordatorios: true,
  });

  // Estado de apariencia
  const [apariencia, setApariencia] = useState({
    tema: 'claro',
    idioma: 'es',
    compacto: false,
  });

  const handleSavePerfil = () => {
    // Aquí iría la llamada a la API
    setSuccessMessage('Perfil actualizado correctamente');
    setTimeout(() => setSuccessMessage(''), 3000);
  };

  const handleChangePassword = () => {
    if (passwordData.nueva !== passwordData.confirmar) {
      alert('Las contraseñas no coinciden');
      return;
    }
    if (passwordData.nueva.length < 6) {
      alert('La contraseña debe tener al menos 6 caracteres');
      return;
    }
    // Aquí iría la llamada a la API
    setSuccessMessage('Contraseña actualizada correctamente');
    setPasswordData({ actual: '', nueva: '', confirmar: '' });
    setTimeout(() => setSuccessMessage(''), 3000);
  };

  const handleSaveNotificaciones = () => {
    // Aquí iría la llamada a la API
    setSuccessMessage('Preferencias de notificaciones guardadas');
    setTimeout(() => setSuccessMessage(''), 3000);
  };

  const handleSaveApariencia = () => {
    // Aquí iría la llamada a la API
    setSuccessMessage('Preferencias de apariencia guardadas');
    setTimeout(() => setSuccessMessage(''), 3000);
  };

  const tabs = [
    { id: 'perfil', label: 'Mi Perfil', icon: User },
    { id: 'seguridad', label: 'Seguridad', icon: Lock },
    { id: 'notificaciones', label: 'Notificaciones', icon: Bell },
    { id: 'apariencia', label: 'Apariencia', icon: Palette },
  ] as const;

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <div className="bg-white border-b border-gray-200 px-8 py-6">
        <h1 className="text-gray-900">Configuración</h1>
        <p className="text-gray-600 mt-1">Administra tu cuenta y preferencias</p>
      </div>

      <div className="p-8">
        {/* Mensaje de éxito */}
        {successMessage && (
          <div className="mb-6 bg-green-50 border border-green-200 rounded-lg p-4 flex items-center gap-3">
            <CheckCircle className="w-5 h-5 text-green-600" />
            <p className="text-green-800">{successMessage}</p>
          </div>
        )}

        <div className="grid grid-cols-1 lg:grid-cols-4 gap-8">
          {/* Sidebar de navegación */}
          <div className="lg:col-span-1">
            <div className="bg-white rounded-xl shadow-md p-4">
              <nav className="space-y-1">
                {tabs.map((tab) => {
                  const Icon = tab.icon;
                  return (
                    <button
                      key={tab.id}
                      onClick={() => setActiveTab(tab.id)}
                      className={`w-full flex items-center gap-3 px-4 py-3 rounded-lg transition ${
                        activeTab === tab.id
                          ? 'bg-indigo-50 text-indigo-700'
                          : 'text-gray-700 hover:bg-gray-50'
                      }`}
                    >
                      <Icon className="w-5 h-5" />
                      <span>{tab.label}</span>
                    </button>
                  );
                })}
              </nav>
            </div>
          </div>

          {/* Contenido principal */}
          <div className="lg:col-span-3">
            <div className="bg-white rounded-xl shadow-md">
              {/* Tab: Mi Perfil */}
              {activeTab === 'perfil' && (
                <div className="p-6">
                  <div className="flex items-center gap-4 mb-6 pb-6 border-b border-gray-200">
                    <div className="w-20 h-20 bg-indigo-100 rounded-full flex items-center justify-center">
                      <User className="w-10 h-10 text-indigo-600" />
                    </div>
                    <div>
                      <h2 className="text-gray-900">{user?.nombreCompleto}</h2>
                      <p className="text-gray-600">{user?.rol === 1 ? 'Administrador' : 'Profesional'}</p>
                    </div>
                  </div>

                  <div className="space-y-6">
                    <h3 className="text-gray-900">Información Personal</h3>

                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                      <div>
                        <label className="block text-gray-700 mb-2">Nombre Completo</label>
                        <div className="relative">
                          <User className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                          <input
                            type="text"
                            value={perfilData.nombreCompleto}
                            onChange={(e) => setPerfilData({ ...perfilData, nombreCompleto: e.target.value })}
                            className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                          />
                        </div>
                      </div>

                      <div>
                        <label className="block text-gray-700 mb-2">DNI</label>
                        <div className="relative">
                          <Building className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                          <input
                            type="text"
                            value={perfilData.dni}
                            disabled
                            className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg bg-gray-50 cursor-not-allowed"
                          />
                        </div>
                      </div>

                      <div>
                        <label className="block text-gray-700 mb-2">Correo Electrónico</label>
                        <div className="relative">
                          <Mail className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                          <input
                            type="email"
                            value={perfilData.correo}
                            onChange={(e) => setPerfilData({ ...perfilData, correo: e.target.value })}
                            className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                          />
                        </div>
                      </div>

                      <div>
                        <label className="block text-gray-700 mb-2">Teléfono</label>
                        <div className="relative">
                          <Phone className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                          <input
                            type="tel"
                            value={perfilData.telefono}
                            onChange={(e) => setPerfilData({ ...perfilData, telefono: e.target.value })}
                            className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                          />
                        </div>
                      </div>

                      {user?.rol === 2 && (
                        <div className="md:col-span-2">
                          <label className="block text-gray-700 mb-2">Especialidad</label>
                          <input
                            type="text"
                            value={perfilData.especialidad}
                            onChange={(e) => setPerfilData({ ...perfilData, especialidad: e.target.value })}
                            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                          />
                        </div>
                      )}
                    </div>

                    <div className="flex justify-end pt-4">
                      <button
                        onClick={handleSavePerfil}
                        className="bg-indigo-600 text-white px-6 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
                      >
                        <Save className="w-5 h-5" />
                        Guardar Cambios
                      </button>
                    </div>
                  </div>
                </div>
              )}

              {/* Tab: Seguridad */}
              {activeTab === 'seguridad' && (
                <div className="p-6">
                  <h2 className="text-gray-900 mb-6">Cambiar Contraseña</h2>
                  
                  <div className="space-y-4 max-w-md">
                    <div>
                      <label className="block text-gray-700 mb-2">Contraseña Actual</label>
                      <div className="relative">
                        <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                        <input
                          type={showPasswords.actual ? 'text' : 'password'}
                          value={passwordData.actual}
                          onChange={(e) => setPasswordData({ ...passwordData, actual: e.target.value })}
                          className="w-full pl-10 pr-12 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                        />
                        <button
                          type="button"
                          onClick={() => setShowPasswords({ ...showPasswords, actual: !showPasswords.actual })}
                          className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600"
                        >
                          {showPasswords.actual ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
                        </button>
                      </div>
                    </div>

                    <div>
                      <label className="block text-gray-700 mb-2">Nueva Contraseña</label>
                      <div className="relative">
                        <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                        <input
                          type={showPasswords.nueva ? 'text' : 'password'}
                          value={passwordData.nueva}
                          onChange={(e) => setPasswordData({ ...passwordData, nueva: e.target.value })}
                          className="w-full pl-10 pr-12 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                        />
                        <button
                          type="button"
                          onClick={() => setShowPasswords({ ...showPasswords, nueva: !showPasswords.nueva })}
                          className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600"
                        >
                          {showPasswords.nueva ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
                        </button>
                      </div>
                    </div>

                    <div>
                      <label className="block text-gray-700 mb-2">Confirmar Nueva Contraseña</label>
                      <div className="relative">
                        <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                        <input
                          type={showPasswords.confirmar ? 'text' : 'password'}
                          value={passwordData.confirmar}
                          onChange={(e) => setPasswordData({ ...passwordData, confirmar: e.target.value })}
                          className="w-full pl-10 pr-12 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                        />
                        <button
                          type="button"
                          onClick={() => setShowPasswords({ ...showPasswords, confirmar: !showPasswords.confirmar })}
                          className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600"
                        >
                          {showPasswords.confirmar ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
                        </button>
                      </div>
                    </div>

                    <div className="pt-4">
                      <button
                        onClick={handleChangePassword}
                        className="bg-indigo-600 text-white px-6 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
                      >
                        <Save className="w-5 h-5" />
                        Actualizar Contraseña
                      </button>
                    </div>

                    <div className="mt-8 p-4 bg-blue-50 border border-blue-200 rounded-lg">
                      <h3 className="text-blue-900 mb-2">Recomendaciones de Seguridad</h3>
                      <ul className="text-blue-800 space-y-1 list-disc list-inside">
                        <li>Usa al menos 8 caracteres</li>
                        <li>Combina letras mayúsculas y minúsculas</li>
                        <li>Incluye números y símbolos</li>
                        <li>No uses información personal</li>
                      </ul>
                    </div>
                  </div>
                </div>
              )}

              {/* Tab: Notificaciones */}
              {activeTab === 'notificaciones' && (
                <div className="p-6">
                  <h2 className="text-gray-900 mb-6">Preferencias de Notificaciones</h2>

                  <div className="space-y-6">
                    <div>
                      <h3 className="text-gray-900 mb-4">Notificaciones por Email</h3>
                      <div className="space-y-3">
                        <label className="flex items-center justify-between p-4 border border-gray-200 rounded-lg hover:bg-gray-50 cursor-pointer">
                          <div>
                            <p className="text-gray-900">Recordatorios de Turnos</p>
                            <p className="text-gray-600">Recibe emails sobre próximos turnos</p>
                          </div>
                          <input
                            type="checkbox"
                            checked={notificaciones.emailTurnos}
                            onChange={(e) => setNotificaciones({ ...notificaciones, emailTurnos: e.target.checked })}
                            className="w-5 h-5 text-indigo-600 rounded focus:ring-2 focus:ring-indigo-500"
                          />
                        </label>

                        <label className="flex items-center justify-between p-4 border border-gray-200 rounded-lg hover:bg-gray-50 cursor-pointer">
                          <div>
                            <p className="text-gray-900">Fichas Pendientes</p>
                            <p className="text-gray-600">Alertas de fichas de seguimiento sin completar</p>
                          </div>
                          <input
                            type="checkbox"
                            checked={notificaciones.emailFichas}
                            onChange={(e) => setNotificaciones({ ...notificaciones, emailFichas: e.target.checked })}
                            className="w-5 h-5 text-indigo-600 rounded focus:ring-2 focus:ring-indigo-500"
                          />
                        </label>

                        <label className="flex items-center justify-between p-4 border border-gray-200 rounded-lg hover:bg-gray-50 cursor-pointer">
                          <div>
                            <p className="text-gray-900">Actualizaciones del Sistema</p>
                            <p className="text-gray-600">Novedades y mejoras de ClinicPass</p>
                          </div>
                          <input
                            type="checkbox"
                            checked={notificaciones.emailSistema}
                            onChange={(e) => setNotificaciones({ ...notificaciones, emailSistema: e.target.checked })}
                            className="w-5 h-5 text-indigo-600 rounded focus:ring-2 focus:ring-indigo-500"
                          />
                        </label>
                      </div>
                    </div>

                    <div>
                      <h3 className="text-gray-900 mb-4">Notificaciones Push</h3>
                      <div className="space-y-3">
                        <label className="flex items-center justify-between p-4 border border-gray-200 rounded-lg hover:bg-gray-50 cursor-pointer">
                          <div>
                            <p className="text-gray-900">Turnos Confirmados</p>
                            <p className="text-gray-600">Notificación cuando se confirma un turno</p>
                          </div>
                          <input
                            type="checkbox"
                            checked={notificaciones.pushTurnos}
                            onChange={(e) => setNotificaciones({ ...notificaciones, pushTurnos: e.target.checked })}
                            className="w-5 h-5 text-indigo-600 rounded focus:ring-2 focus:ring-indigo-500"
                          />
                        </label>

                        <label className="flex items-center justify-between p-4 border border-gray-200 rounded-lg hover:bg-gray-50 cursor-pointer">
                          <div>
                            <p className="text-gray-900">Recordatorios</p>
                            <p className="text-gray-600">Recordatorios 30 minutos antes del turno</p>
                          </div>
                          <input
                            type="checkbox"
                            checked={notificaciones.pushRecordatorios}
                            onChange={(e) => setNotificaciones({ ...notificaciones, pushRecordatorios: e.target.checked })}
                            className="w-5 h-5 text-indigo-600 rounded focus:ring-2 focus:ring-indigo-500"
                          />
                        </label>
                      </div>
                    </div>

                    <div className="flex justify-end pt-4">
                      <button
                        onClick={handleSaveNotificaciones}
                        className="bg-indigo-600 text-white px-6 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
                      >
                        <Save className="w-5 h-5" />
                        Guardar Preferencias
                      </button>
                    </div>
                  </div>
                </div>
              )}

              {/* Tab: Apariencia */}
              {activeTab === 'apariencia' && (
                <div className="p-6">
                  <h2 className="text-gray-900 mb-6">Preferencias de Apariencia</h2>

                  <div className="space-y-6">
                    <div>
                      <label className="block text-gray-700 mb-3">Tema de Color</label>
                      <div className="grid grid-cols-2 gap-4">
                        <button
                          onClick={() => setApariencia({ ...apariencia, tema: 'claro' })}
                          className={`p-4 border-2 rounded-lg transition ${
                            apariencia.tema === 'claro'
                              ? 'border-indigo-600 bg-indigo-50'
                              : 'border-gray-200 hover:border-gray-300'
                          }`}
                        >
                          <div className="w-full h-20 bg-white rounded-lg border border-gray-200 mb-3"></div>
                          <p className="text-gray-900">Claro</p>
                        </button>

                        <button
                          onClick={() => setApariencia({ ...apariencia, tema: 'oscuro' })}
                          className={`p-4 border-2 rounded-lg transition ${
                            apariencia.tema === 'oscuro'
                              ? 'border-indigo-600 bg-indigo-50'
                              : 'border-gray-200 hover:border-gray-300'
                          }`}
                        >
                          <div className="w-full h-20 bg-gray-800 rounded-lg border border-gray-700 mb-3"></div>
                          <p className="text-gray-900">Oscuro</p>
                        </button>
                      </div>
                    </div>

                    <div>
                      <label className="block text-gray-700 mb-3">Idioma</label>
                      <select
                        value={apariencia.idioma}
                        onChange={(e) => setApariencia({ ...apariencia, idioma: e.target.value })}
                        className="w-full max-w-xs px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                      >
                        <option value="es">Español</option>
                        <option value="en">English</option>
                        <option value="pt">Português</option>
                      </select>
                    </div>

                    <div>
                      <label className="flex items-center justify-between p-4 border border-gray-200 rounded-lg hover:bg-gray-50 cursor-pointer max-w-xl">
                        <div>
                          <p className="text-gray-900">Modo Compacto</p>
                          <p className="text-gray-600">Reduce el espaciado entre elementos</p>
                        </div>
                        <input
                          type="checkbox"
                          checked={apariencia.compacto}
                          onChange={(e) => setApariencia({ ...apariencia, compacto: e.target.checked })}
                          className="w-5 h-5 text-indigo-600 rounded focus:ring-2 focus:ring-indigo-500"
                        />
                      </label>
                    </div>

                    <div className="flex justify-end pt-4">
                      <button
                        onClick={handleSaveApariencia}
                        className="bg-indigo-600 text-white px-6 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
                      >
                        <Save className="w-5 h-5" />
                        Guardar Preferencias
                      </button>
                    </div>
                  </div>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};