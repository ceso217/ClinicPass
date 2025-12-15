'use client';
import React, { useState, useEffect } from 'react';
import { X, User, Mail, Phone, Stethoscope, Building, Key, Shield } from 'lucide-react';
import { Profesional } from '../../data/mockData';

interface ProfesionalModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (profesional: Partial<Profesional>) => void;
  profesional?: Profesional | null;
  mode: 'create' | 'edit';
}

export const ProfesionalModal: React.FC<ProfesionalModalProps> = ({
  isOpen,
  onClose,
  onSave,
  profesional,
  mode,
}) => {
  const [formData, setFormData] = useState<Partial<Profesional>>({
    nombreCompleto: '',
    dni: '',
    correo: '',
    telefono: '',
    especialidad: '',
    rol: 2,
    activo: true,
  });

  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [errors, setErrors] = useState<Record<string, string>>({});

  const especialidades = [
    'Cardiología',
    'Pediatría',
    'Traumatología',
    'Dermatología',
    'Clínica Médica',
    'Neurología',
    'Oftalmología',
    'Otorrinolaringología',
    'Ginecología',
    'Urología',
    'Psiquiatría',
    'Nutrición',
    'Kinesiología',
    'Odontología',
    'Otra',
  ];

  useEffect(() => {
    if (profesional && mode === 'edit') {
      setFormData(profesional);
      setPassword('');
      setConfirmPassword('');
    } else {
      setFormData({
        nombreCompleto: '',
        dni: '',
        correo: '',
        telefono: '',
        especialidad: '',
        rol: 2,
        activo: true,
      });
      setPassword('');
      setConfirmPassword('');
    }
    setErrors({});
  }, [profesional, mode, isOpen]);

  const validateForm = (): boolean => {
    const newErrors: Record<string, string> = {};

    if (!formData.nombreCompleto?.trim()) {
      newErrors.nombreCompleto = 'El nombre es requerido';
    }

    if (!formData.dni?.trim()) {
      newErrors.dni = 'El DNI es requerido';
    } else if (!/^\d{7,8}$/.test(formData.dni)) {
      newErrors.dni = 'El DNI debe tener 7 u 8 dígitos';
    }

    if (!formData.correo?.trim()) {
      newErrors.correo = 'El correo es requerido';
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.correo)) {
      newErrors.correo = 'Ingrese un correo válido';
    }

    if (!formData.telefono?.trim()) {
      newErrors.telefono = 'El teléfono es requerido';
    }

    if (!formData.especialidad?.trim()) {
      newErrors.especialidad = 'La especialidad es requerida';
    }

    // Validar contraseña solo en modo create
    if (mode === 'create') {
      if (!password) {
        newErrors.password = 'La contraseña es requerida';
      } else if (password.length < 6) {
        newErrors.password = 'La contraseña debe tener al menos 6 caracteres';
      }

      if (password !== confirmPassword) {
        newErrors.confirmPassword = 'Las contraseñas no coinciden';
      }
    } else if (password) {
      // En modo edit, solo validar si se ingresó una nueva contraseña
      if (password.length < 6) {
        newErrors.password = 'La contraseña debe tener al menos 6 caracteres';
      }
      if (password !== confirmPassword) {
        newErrors.confirmPassword = 'Las contraseñas no coinciden';
      }
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = () => {
    if (!validateForm()) {
      return;
    }

    const profesionalData = {
      ...formData,
      ...(password && { password }), // Solo incluir password si se ingresó
    };

    onSave(profesionalData);
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
      <div className="bg-white rounded-xl shadow-xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
        {/* Header */}
        <div className="sticky top-0 bg-white border-b border-gray-200 p-6 flex items-center justify-between">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 bg-blue-100 rounded-full flex items-center justify-center">
              <Stethoscope className="w-6 h-6 text-blue-600" />
            </div>
            <div>
              <h2 className="text-gray-900">
                {mode === 'create' ? 'Agregar Nuevo Profesional' : 'Editar Profesional'}
              </h2>
              <p className="text-gray-600">
                {mode === 'create' ? 'Complete los datos del profesional' : 'Modifique los datos necesarios'}
              </p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="p-2 hover:bg-gray-100 rounded-lg transition"
          >
            <X className="w-5 h-5 text-gray-600" />
          </button>
        </div>

        {/* Body */}
        <div className="p-6 space-y-6">
          {/* Información Personal */}
          <div>
            <h3 className="text-gray-900 mb-4 flex items-center gap-2">
              <User className="w-5 h-5 text-blue-600" />
              Información Personal
            </h3>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div className="md:col-span-2">
                <label className="block text-gray-700 mb-2">
                  Nombre Completo <span className="text-red-500">*</span>
                </label>
                <input
                  type="text"
                  value={formData.nombreCompleto}
                  onChange={(e) => setFormData({ ...formData, nombreCompleto: e.target.value })}
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 ${
                    errors.nombreCompleto ? 'border-red-500' : 'border-gray-300'
                  }`}
                  placeholder="Ej: Dr. Juan Carlos Pérez"
                />
                {errors.nombreCompleto && (
                  <p className="text-red-500 mt-1">{errors.nombreCompleto}</p>
                )}
              </div>

              <div>
                <label className="block text-gray-700 mb-2">
                  DNI <span className="text-red-500">*</span>
                </label>
                <div className="relative">
                  <Building className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                  <input
                    type="text"
                    value={formData.dni}
                    onChange={(e) => setFormData({ ...formData, dni: e.target.value.replace(/\D/g, '') })}
                    className={`w-full pl-10 pr-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 ${
                      errors.dni ? 'border-red-500' : 'border-gray-300'
                    }`}
                    placeholder="12345678"
                    maxLength={8}
                    disabled={mode === 'edit'}
                  />
                </div>
                {errors.dni && <p className="text-red-500 mt-1">{errors.dni}</p>}
              </div>

              <div>
                <label className="block text-gray-700 mb-2">
                  Teléfono <span className="text-red-500">*</span>
                </label>
                <div className="relative">
                  <Phone className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                  <input
                    type="tel"
                    value={formData.telefono}
                    onChange={(e) => setFormData({ ...formData, telefono: e.target.value })}
                    className={`w-full pl-10 pr-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 ${
                      errors.telefono ? 'border-red-500' : 'border-gray-300'
                    }`}
                    placeholder="3794-123456"
                  />
                </div>
                {errors.telefono && <p className="text-red-500 mt-1">{errors.telefono}</p>}
              </div>

              <div className="md:col-span-2">
                <label className="block text-gray-700 mb-2">
                  Correo Electrónico <span className="text-red-500">*</span>
                </label>
                <div className="relative">
                  <Mail className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                  <input
                    type="email"
                    value={formData.correo}
                    onChange={(e) => setFormData({ ...formData, correo: e.target.value })}
                    className={`w-full pl-10 pr-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 ${
                      errors.correo ? 'border-red-500' : 'border-gray-300'
                    }`}
                    placeholder="doctor@clinicpass.com"
                  />
                </div>
                {errors.correo && <p className="text-red-500 mt-1">{errors.correo}</p>}
              </div>
            </div>
          </div>

          {/* Información Profesional */}
          <div className="border-t border-gray-200 pt-6">
            <h3 className="text-gray-900 mb-4 flex items-center gap-2">
              <Stethoscope className="w-5 h-5 text-blue-600" />
              Información Profesional
            </h3>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label className="block text-gray-700 mb-2">
                  Especialidad <span className="text-red-500">*</span>
                </label>
                <select
                  value={formData.especialidad}
                  onChange={(e) => setFormData({ ...formData, especialidad: e.target.value })}
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 ${
                    errors.especialidad ? 'border-red-500' : 'border-gray-300'
                  }`}
                >
                  <option value="">Seleccione una especialidad</option>
                  {especialidades.map((esp) => (
                    <option key={esp} value={esp}>
                      {esp}
                    </option>
                  ))}
                </select>
                {errors.especialidad && <p className="text-red-500 mt-1">{errors.especialidad}</p>}
              </div>

              <div>
                <label className="block text-gray-700 mb-2">
                  Rol <span className="text-red-500">*</span>
                </label>
                <select
                  value={formData.rol}
                  onChange={(e) => setFormData({ ...formData, rol: parseInt(e.target.value) })}
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500"
                >
                  <option value={2}>Profesional</option>
                  <option value={1}>Administrador</option>
                </select>
                <p className="text-gray-600 mt-1">
                  {formData.rol === 1 ? 'Acceso completo al sistema' : 'Acceso a pacientes y turnos'}
                </p>
              </div>

              <div>
                <label className="block text-gray-700 mb-2">Estado</label>
                <div className="flex items-center gap-4 pt-2">
                  <label className="flex items-center gap-2 cursor-pointer">
                    <input
                      type="radio"
                      checked={formData.activo === true}
                      onChange={() => setFormData({ ...formData, activo: true })}
                      className="w-4 h-4 text-blue-600 focus:ring-2 focus:ring-blue-500"
                    />
                    <span className="text-gray-700">Activo</span>
                  </label>
                  <label className="flex items-center gap-2 cursor-pointer">
                    <input
                      type="radio"
                      checked={formData.activo === false}
                      onChange={() => setFormData({ ...formData, activo: false })}
                      className="w-4 h-4 text-blue-600 focus:ring-2 focus:ring-blue-500"
                    />
                    <span className="text-gray-700">Inactivo</span>
                  </label>
                </div>
              </div>
            </div>
          </div>

          {/* Seguridad */}
          <div className="border-t border-gray-200 pt-6">
            <h3 className="text-gray-900 mb-4 flex items-center gap-2">
              <Shield className="w-5 h-5 text-blue-600" />
              {mode === 'create' ? 'Contraseña' : 'Cambiar Contraseña (opcional)'}
            </h3>
            {mode === 'edit' && (
              <p className="text-gray-600 mb-4">
                Deja los campos vacíos si no deseas cambiar la contraseña
              </p>
            )}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label className="block text-gray-700 mb-2">
                  Contraseña {mode === 'create' && <span className="text-red-500">*</span>}
                </label>
                <div className="relative">
                  <Key className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                  <input
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className={`w-full pl-10 pr-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 ${
                      errors.password ? 'border-red-500' : 'border-gray-300'
                    }`}
                    placeholder="Mínimo 6 caracteres"
                  />
                </div>
                {errors.password && <p className="text-red-500 mt-1">{errors.password}</p>}
              </div>

              <div>
                <label className="block text-gray-700 mb-2">
                  Confirmar Contraseña {mode === 'create' && <span className="text-red-500">*</span>}
                </label>
                <div className="relative">
                  <Key className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                  <input
                    type="password"
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                    className={`w-full pl-10 pr-4 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 ${
                      errors.confirmPassword ? 'border-red-500' : 'border-gray-300'
                    }`}
                    placeholder="Repite la contraseña"
                  />
                </div>
                {errors.confirmPassword && <p className="text-red-500 mt-1">{errors.confirmPassword}</p>}
              </div>
            </div>
          </div>

          {/* Información adicional */}
          {mode === 'create' && (
            <div className="bg-blue-50 border border-blue-200 rounded-lg p-4">
              <p className="text-blue-800">
                <strong>Nota:</strong> Se enviará un correo al profesional con sus credenciales de acceso
                una vez creada la cuenta.
              </p>
            </div>
          )}
        </div>

        {/* Footer */}
        <div className="sticky bottom-0 bg-gray-50 border-t border-gray-200 p-6 flex justify-end gap-3">
          <button
            onClick={onClose}
            className="px-6 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-100 transition"
          >
            Cancelar
          </button>
          <button
            onClick={handleSubmit}
            className="px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition"
          >
            {mode === 'create' ? 'Agregar Profesional' : 'Guardar Cambios'}
          </button>
        </div>
      </div>
    </div>
  );
};
