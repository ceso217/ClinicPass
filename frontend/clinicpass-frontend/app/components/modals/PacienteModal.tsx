import React, { useState, useEffect } from 'react';
import { X, User, Calendar, MapPin, Phone, Mail, Users } from 'lucide-react';
import { Paciente } from '../../data/mockData';

interface PacienteModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (paciente: Partial<Paciente>) => void;
  paciente?: Paciente | null;
  mode: 'create' | 'edit';
}

export const PacienteModal: React.FC<PacienteModalProps> = ({
  isOpen,
  onClose,
  onSave,
  paciente,
  mode,
}) => {
  const [formData, setFormData] = useState<Partial<Paciente>>({
    nombreCompleto: '',
    dni: '',
    fechaNacimiento: '',
    edad: 0,
    localidad: '',
    provincia: 'Chaco',
    calle: '',
    telefono: '',
  });

  const [errors, setErrors] = useState<Record<string, string>>({});
  const [esMenor, setEsMenor] = useState(false);
  const [tutorData, setTutorData] = useState({
    nombreCompleto: '',
    dni: '',
    telefono: '',
    relacion: 'Padre',
  });

  useEffect(() => {
    if (paciente && mode === 'edit') {
      setFormData(paciente);
      // Verificar si es menor de edad
      const edad = paciente.edad || 0;
      setEsMenor(edad < 18);
    } else {
      // Reset para modo create
      setFormData({
        nombreCompleto: '',
        dni: '',
        fechaNacimiento: '',
        edad: 0,
        localidad: '',
        provincia: 'Chaco',
        calle: '',
        telefono: '',
      });
      setEsMenor(false);
      setTutorData({
        nombreCompleto: '',
        dni: '',
        telefono: '',
        relacion: 'Padre',
      });
    }
    setErrors({});
  }, [paciente, mode, isOpen]);

  const calcularEdad = (fechaNacimiento: string): number => {
    const hoy = new Date();
    const nacimiento = new Date(fechaNacimiento);
    let edad = hoy.getFullYear() - nacimiento.getFullYear();
    const mes = hoy.getMonth() - nacimiento.getMonth();
    if (mes < 0 || (mes === 0 && hoy.getDate() < nacimiento.getDate())) {
      edad--;
    }
    return edad;
  };

  const handleFechaNacimientoChange = (fecha: string) => {
    const edad = calcularEdad(fecha);
    setFormData({ ...formData, fechaNacimiento: fecha, edad });
    setEsMenor(edad < 18);
  };

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

    if (!formData.fechaNacimiento) {
      newErrors.fechaNacimiento = 'La fecha de nacimiento es requerida';
    }

    if (!formData.localidad?.trim()) {
      newErrors.localidad = 'La localidad es requerida';
    }

    if (!formData.telefono?.trim()) {
      newErrors.telefono = 'El teléfono es requerido';
    }

    if (esMenor) {
      if (!tutorData.nombreCompleto?.trim()) {
        newErrors.tutorNombre = 'El nombre del tutor es requerido';
      }
      if (!tutorData.dni?.trim()) {
        newErrors.tutorDni = 'El DNI del tutor es requerido';
      }
      if (!tutorData.telefono?.trim()) {
        newErrors.tutorTelefono = 'El teléfono del tutor es requerido';
      }
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = () => {
    if (!validateForm()) {
      return;
    }

    const pacienteData = {
      ...formData,
      ...(esMenor && { tutorData }),
    };

    onSave(pacienteData);
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
      <div className="bg-white rounded-xl shadow-xl max-w-3xl w-full max-h-[90vh] overflow-y-auto">
        {/* Header */}
        <div className="sticky top-0 bg-white border-b border-gray-200 p-6 flex items-center justify-between">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 bg-indigo-100 rounded-full flex items-center justify-center">
              <User className="w-6 h-6 text-indigo-600" />
            </div>
            <div>
              <h2 className="text-gray-900">
                {mode === 'create' ? 'Agregar Nuevo Paciente' : 'Editar Paciente'}
              </h2>
              <p className="text-gray-600">
                {mode === 'create' ? 'Complete los datos del paciente' : 'Modifique los datos necesarios'}
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
              <User className="w-5 h-5 text-indigo-600" />
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
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-indigo-500 ${
                    errors.nombreCompleto ? 'border-red-500' : 'border-gray-300'
                  }`}
                  placeholder="Ej: Juan Carlos Pérez"
                />
                {errors.nombreCompleto && (
                  <p className="text-red-500 mt-1">{errors.nombreCompleto}</p>
                )}
              </div>

              <div>
                <label className="block text-gray-700 mb-2">
                  DNI <span className="text-red-500">*</span>
                </label>
                <input
                  type="text"
                  value={formData.dni}
                  onChange={(e) => setFormData({ ...formData, dni: e.target.value.replace(/\D/g, '') })}
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-indigo-500 ${
                    errors.dni ? 'border-red-500' : 'border-gray-300'
                  }`}
                  placeholder="Ej: 12345678"
                  maxLength={8}
                />
                {errors.dni && <p className="text-red-500 mt-1">{errors.dni}</p>}
              </div>

              <div>
                <label className="block text-gray-700 mb-2">
                  Fecha de Nacimiento <span className="text-red-500">*</span>
                </label>
                <div className="relative">
                  <Calendar className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                  <input
                    type="date"
                    value={formData.fechaNacimiento}
                    onChange={(e) => handleFechaNacimientoChange(e.target.value)}
                    className={`w-full pl-10 pr-4 py-2 border rounded-lg focus:ring-2 focus:ring-indigo-500 ${
                      errors.fechaNacimiento ? 'border-red-500' : 'border-gray-300'
                    }`}
                  />
                </div>
                {errors.fechaNacimiento && (
                  <p className="text-red-500 mt-1">{errors.fechaNacimiento}</p>
                )}
              </div>

              <div>
                <label className="block text-gray-700 mb-2">Edad</label>
                <input
                  type="number"
                  value={formData.edad || ''}
                  disabled
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg bg-gray-50"
                  placeholder="Se calcula automáticamente"
                />
                {esMenor && (
                  <p className="text-orange-600 mt-1 flex items-center gap-1">
                    <Users className="w-4 h-4" />
                    Menor de edad - Requiere tutor
                  </p>
                )}
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
                    className={`w-full pl-10 pr-4 py-2 border rounded-lg focus:ring-2 focus:ring-indigo-500 ${
                      errors.telefono ? 'border-red-500' : 'border-gray-300'
                    }`}
                    placeholder="Ej: 3794-123456"
                  />
                </div>
                {errors.telefono && <p className="text-red-500 mt-1">{errors.telefono}</p>}
              </div>
            </div>
          </div>

          {/* Dirección */}
          <div>
            <h3 className="text-gray-900 mb-4 flex items-center gap-2">
              <MapPin className="w-5 h-5 text-indigo-600" />
              Dirección
            </h3>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label className="block text-gray-700 mb-2">
                  Calle y Número <span className="text-red-500">*</span>
                </label>
                <input
                  type="text"
                  value={formData.calle}
                  onChange={(e) => setFormData({ ...formData, calle: e.target.value })}
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                  placeholder="Ej: Av. Alberdi 1234"
                />
              </div>

              <div>
                <label className="block text-gray-700 mb-2">
                  Localidad <span className="text-red-500">*</span>
                </label>
                <input
                  type="text"
                  value={formData.localidad}
                  onChange={(e) => setFormData({ ...formData, localidad: e.target.value })}
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-indigo-500 ${
                    errors.localidad ? 'border-red-500' : 'border-gray-300'
                  }`}
                  placeholder="Ej: Resistencia"
                />
                {errors.localidad && <p className="text-red-500 mt-1">{errors.localidad}</p>}
              </div>

              <div>
                <label className="block text-gray-700 mb-2">
                  Provincia <span className="text-red-500">*</span>
                </label>
                <select
                  value={formData.provincia}
                  onChange={(e) => setFormData({ ...formData, provincia: e.target.value })}
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                >
                  <option value="Chaco">Chaco</option>
                  <option value="Corrientes">Corrientes</option>
                  <option value="Formosa">Formosa</option>
                  <option value="Misiones">Misiones</option>
                  <option value="Santa Fe">Santa Fe</option>
                </select>
              </div>
            </div>
          </div>

          {/* Datos del Tutor (solo si es menor) */}
          {esMenor && (
            <div className="border-t border-gray-200 pt-6">
              <h3 className="text-gray-900 mb-4 flex items-center gap-2">
                <Users className="w-5 h-5 text-orange-600" />
                Datos del Tutor
              </h3>
              <div className="bg-orange-50 border border-orange-200 rounded-lg p-4 mb-4">
                <p className="text-orange-800">
                  El paciente es menor de edad. Es obligatorio registrar los datos de un tutor responsable.
                </p>
              </div>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="md:col-span-2">
                  <label className="block text-gray-700 mb-2">
                    Nombre del Tutor <span className="text-red-500">*</span>
                  </label>
                  <input
                    type="text"
                    value={tutorData.nombreCompleto}
                    onChange={(e) => setTutorData({ ...tutorData, nombreCompleto: e.target.value })}
                    className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-indigo-500 ${
                      errors.tutorNombre ? 'border-red-500' : 'border-gray-300'
                    }`}
                    placeholder="Nombre completo del tutor"
                  />
                  {errors.tutorNombre && <p className="text-red-500 mt-1">{errors.tutorNombre}</p>}
                </div>

                <div>
                  <label className="block text-gray-700 mb-2">
                    DNI del Tutor <span className="text-red-500">*</span>
                  </label>
                  <input
                    type="text"
                    value={tutorData.dni}
                    onChange={(e) => setTutorData({ ...tutorData, dni: e.target.value.replace(/\D/g, '') })}
                    className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-indigo-500 ${
                      errors.tutorDni ? 'border-red-500' : 'border-gray-300'
                    }`}
                    placeholder="DNI del tutor"
                    maxLength={8}
                  />
                  {errors.tutorDni && <p className="text-red-500 mt-1">{errors.tutorDni}</p>}
                </div>

                <div>
                  <label className="block text-gray-700 mb-2">
                    Teléfono del Tutor <span className="text-red-500">*</span>
                  </label>
                  <input
                    type="tel"
                    value={tutorData.telefono}
                    onChange={(e) => setTutorData({ ...tutorData, telefono: e.target.value })}
                    className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-indigo-500 ${
                      errors.tutorTelefono ? 'border-red-500' : 'border-gray-300'
                    }`}
                    placeholder="Teléfono del tutor"
                  />
                  {errors.tutorTelefono && <p className="text-red-500 mt-1">{errors.tutorTelefono}</p>}
                </div>

                <div>
                  <label className="block text-gray-700 mb-2">Relación</label>
                  <select
                    value={tutorData.relacion}
                    onChange={(e) => setTutorData({ ...tutorData, relacion: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                  >
                    <option value="Padre">Padre</option>
                    <option value="Madre">Madre</option>
                    <option value="Tutor Legal">Tutor Legal</option>
                    <option value="Abuelo/a">Abuelo/a</option>
                    <option value="Otro">Otro</option>
                  </select>
                </div>
              </div>
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
            className="px-6 py-2 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 transition"
          >
            {mode === 'create' ? 'Agregar Paciente' : 'Guardar Cambios'}
          </button>
        </div>
      </div>
    </div>
  );
};