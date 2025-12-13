'use client';
import React, { useState, useEffect } from 'react';
import { X, Calendar, Clock, User, UserPlus, Stethoscope, AlertCircle } from 'lucide-react';
import { mockPacientes, mockProfesionales, Turno, Paciente } from '../../data/mockData';
import { PacienteModal } from './PacienteModal';

interface TurnoModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (turno: Partial<Turno>) => void;
  turno?: Turno | null;
  fechaPreseleccionada?: string;
}

export const TurnoModal: React.FC<TurnoModalProps> = ({
  isOpen,
  onClose,
  onSave,
  turno,
  fechaPreseleccionada,
}) => {
  const [formData, setFormData] = useState({
    fecha: fechaPreseleccionada || '',
    hora: '',
    pacienteId: 0,
    profesionalId: 0,
    estado: 'Programado' as 'Programado' | 'Confirmado' | 'Completado' | 'Cancelado',
    observaciones: '',
  });

  const [errors, setErrors] = useState<Record<string, string>>({});
  const [searchPaciente, setSearchPaciente] = useState('');
  const [showPacienteModal, setShowPacienteModal] = useState(false);
  const [pacientes, setPacientes] = useState(mockPacientes);
  const [profesionales] = useState(mockProfesionales.filter(p => p.activo));

  // Horarios disponibles (de 8:00 a 20:00, cada 30 min)
  const generarHorarios = () => {
    const horarios: string[] = [];
    for (let h = 8; h <= 19; h++) {
      horarios.push(`${h.toString().padStart(2, '0')}:00`);
      if (h < 19) {
        horarios.push(`${h.toString().padStart(2, '0')}:30`);
      }
    }
    return horarios;
  };

  const horarios = generarHorarios();

  useEffect(() => {
    if (turno) {
      setFormData({
        fecha: turno.fecha,
        hora: turno.hora,
        pacienteId: turno.pacienteId,
        profesionalId: turno.profesionalId,
        estado: turno.estado,
        observaciones: '',
      });
    } else if (fechaPreseleccionada) {
      setFormData({
        ...formData,
        fecha: fechaPreseleccionada,
      });
    }
    setErrors({});
  }, [turno, fechaPreseleccionada, isOpen]);

  const pacientesFiltrados = searchPaciente
    ? pacientes.filter(
        (p) =>
          p.nombreCompleto.toLowerCase().includes(searchPaciente.toLowerCase()) ||
          p.dni.includes(searchPaciente)
      )
    : pacientes;

  const pacienteSeleccionado = pacientes.find((p) => p.id === formData.pacienteId);
  const profesionalSeleccionado = profesionales.find((p) => p.id === formData.profesionalId);

  const validateForm = (): boolean => {
    const newErrors: Record<string, string> = {};

    if (!formData.fecha) {
      newErrors.fecha = 'Seleccione una fecha';
    } else {
      const fechaSeleccionada = new Date(formData.fecha);
      const hoy = new Date();
      hoy.setHours(0, 0, 0, 0);
      if (fechaSeleccionada < hoy) {
        newErrors.fecha = 'No puede agendar turnos en fechas pasadas';
      }
    }

    if (!formData.hora) {
      newErrors.hora = 'Seleccione un horario';
    }

    if (!formData.pacienteId) {
      newErrors.paciente = 'Seleccione un paciente';
    }

    if (!formData.profesionalId) {
      newErrors.profesional = 'Seleccione un profesional';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = () => {
    if (!validateForm()) {
      return;
    }

    const paciente = pacientes.find((p) => p.id === formData.pacienteId);
    const profesional = profesionales.find((p) => p.id === formData.profesionalId);

    const turnoAEnviar: Partial<Turno> = {
      id: turno?.id,
      fecha: formData.fecha,
      hora: formData.hora,
      pacienteId: formData.pacienteId,
      pacienteNombre: paciente?.nombreCompleto || '',
      profesionalId: formData.profesionalId,
      profesionalNombre: profesional?.nombreCompleto || '',
      estado: formData.estado,
      fichaCreada: false,
    };

    onSave(turnoAEnviar);
  };

  const handleAddPaciente = (nuevoPaciente: Partial<Paciente>) => {
    const newPaciente: Paciente = {
      id: pacientes.length + 1,
      nombreCompleto: nuevoPaciente.nombreCompleto || '',
      dni: nuevoPaciente.dni || '',
      fechaNacimiento: nuevoPaciente.fechaNacimiento || '',
      edad: nuevoPaciente.edad || 0,
      localidad: nuevoPaciente.localidad || '',
      provincia: nuevoPaciente.provincia || '',
      calle: nuevoPaciente.calle || '',
      telefono: nuevoPaciente.telefono || '',
    };
    setPacientes([...pacientes, newPaciente]);
    setFormData({ ...formData, pacienteId: newPaciente.id });
    setShowPacienteModal(false);
  };

  if (!isOpen) return null;

  return (
    <>
      <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
        <div className="bg-white rounded-xl shadow-xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
          {/* Header */}
          <div className="sticky top-0 bg-white border-b border-gray-200 p-6 flex items-center justify-between">
            <div className="flex items-center gap-3">
              <div className="w-10 h-10 bg-green-100 rounded-full flex items-center justify-center">
                <Calendar className="w-6 h-6 text-green-600" />
              </div>
              <div>
                <h2 className="text-gray-900">
                  {turno ? 'Editar Turno' : 'Agendar Nuevo Turno'}
                </h2>
                <p className="text-gray-600">Complete la información del turno</p>
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
            {/* Fecha y Hora */}
            <div>
              <h3 className="text-gray-900 mb-4 flex items-center gap-2">
                <Clock className="w-5 h-5 text-green-600" />
                Fecha y Hora
              </h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="block text-gray-700 mb-2">
                    Fecha <span className="text-red-500">*</span>
                  </label>
                  <input
                    type="date"
                    value={formData.fecha}
                    onChange={(e) => setFormData({ ...formData, fecha: e.target.value })}
                    className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-green-500 ${
                      errors.fecha ? 'border-red-500' : 'border-gray-300'
                    }`}
                    min={new Date().toISOString().split('T')[0]}
                  />
                  {errors.fecha && <p className="text-red-500 mt-1">{errors.fecha}</p>}
                </div>

                <div>
                  <label className="block text-gray-700 mb-2">
                    Horario <span className="text-red-500">*</span>
                  </label>
                  <select
                    value={formData.hora}
                    onChange={(e) => setFormData({ ...formData, hora: e.target.value })}
                    className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-green-500 ${
                      errors.hora ? 'border-red-500' : 'border-gray-300'
                    }`}
                  >
                    <option value="">Seleccione un horario</option>
                    {horarios.map((h) => (
                      <option key={h} value={h}>
                        {h} hs
                      </option>
                    ))}
                  </select>
                  {errors.hora && <p className="text-red-500 mt-1">{errors.hora}</p>}
                </div>
              </div>
            </div>

            {/* Selección de Paciente */}
            <div>
              <div className="flex items-center justify-between mb-4">
                <h3 className="text-gray-900 flex items-center gap-2">
                  <User className="w-5 h-5 text-green-600" />
                  Paciente
                </h3>
                <button
                  onClick={() => setShowPacienteModal(true)}
                  className="flex items-center gap-2 px-3 py-1 text-green-600 border border-green-600 rounded-lg hover:bg-green-50 transition"
                >
                  <UserPlus className="w-4 h-4" />
                  Nuevo Paciente
                </button>
              </div>

              <div className="space-y-3">
                <input
                  type="text"
                  placeholder="Buscar por nombre o DNI..."
                  value={searchPaciente}
                  onChange={(e) => setSearchPaciente(e.target.value)}
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500"
                />

                <div className="border border-gray-300 rounded-lg max-h-48 overflow-y-auto">
                  {pacientesFiltrados.length === 0 ? (
                    <div className="p-4 text-center text-gray-500">
                      No se encontraron pacientes
                    </div>
                  ) : (
                    pacientesFiltrados.map((paciente) => (
                      <button
                        key={paciente.id}
                        onClick={() => setFormData({ ...formData, pacienteId: paciente.id })}
                        className={`w-full p-3 text-left hover:bg-gray-50 transition border-b border-gray-200 last:border-0 ${
                          formData.pacienteId === paciente.id ? 'bg-green-50' : ''
                        }`}
                      >
                        <div className="flex items-center justify-between">
                          <div>
                            <p className="text-gray-900">{paciente.nombreCompleto}</p>
                            <p className="text-gray-600">
                              DNI: {paciente.dni} • {paciente.edad} años
                            </p>
                          </div>
                          {formData.pacienteId === paciente.id && (
                            <div className="w-5 h-5 bg-green-600 rounded-full flex items-center justify-center">
                              <svg className="w-3 h-3 text-white" fill="currentColor" viewBox="0 0 20 20">
                                <path
                                  fillRule="evenodd"
                                  d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z"
                                  clipRule="evenodd"
                                />
                              </svg>
                            </div>
                          )}
                        </div>
                      </button>
                    ))
                  )}
                </div>
                {errors.paciente && <p className="text-red-500 mt-1">{errors.paciente}</p>}

                {pacienteSeleccionado && (
                  <div className="bg-green-50 border border-green-200 rounded-lg p-3">
                    <p className="text-green-800">
                      <strong>Paciente seleccionado:</strong> {pacienteSeleccionado.nombreCompleto}
                    </p>
                    <p className="text-green-700">
                      Tel: {pacienteSeleccionado.telefono} • {pacienteSeleccionado.localidad}
                    </p>
                  </div>
                )}
              </div>
            </div>

            {/* Selección de Profesional */}
            <div>
              <h3 className="text-gray-900 mb-4 flex items-center gap-2">
                <Stethoscope className="w-5 h-5 text-green-600" />
                Profesional
              </h3>
              <div className="space-y-3">
                <select
                  value={formData.profesionalId}
                  onChange={(e) => setFormData({ ...formData, profesionalId: parseInt(e.target.value) })}
                  className={`w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-green-500 ${
                    errors.profesional ? 'border-red-500' : 'border-gray-300'
                  }`}
                >
                  <option value="">Seleccione un profesional</option>
                  {profesionales.map((prof) => (
                    <option key={prof.id} value={prof.id}>
                      {prof.nombreCompleto} - {prof.especialidad}
                    </option>
                  ))}
                </select>
                {errors.profesional && <p className="text-red-500 mt-1">{errors.profesional}</p>}

                {profesionalSeleccionado && (
                  <div className="bg-blue-50 border border-blue-200 rounded-lg p-3">
                    <p className="text-blue-800">
                      <strong>{profesionalSeleccionado.nombreCompleto}</strong>
                    </p>
                    <p className="text-blue-700">
                      {profesionalSeleccionado.especialidad} • Tel: {profesionalSeleccionado.telefono}
                    </p>
                  </div>
                )}
              </div>
            </div>

            {/* Estado */}
            <div>
              <label className="block text-gray-700 mb-2">Estado del Turno</label>
              <select
                value={formData.estado}
                onChange={(e) => setFormData({ ...formData, estado: e.target.value as any })}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500"
              >
                <option value="Programado">Programado</option>
                <option value="Confirmado">Confirmado</option>
                <option value="Completado">Completado</option>
                <option value="Cancelado">Cancelado</option>
              </select>
            </div>

            {/* Observaciones */}
            <div>
              <label className="block text-gray-700 mb-2">Observaciones (opcional)</label>
              <textarea
                value={formData.observaciones}
                onChange={(e) => setFormData({ ...formData, observaciones: e.target.value })}
                rows={3}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500"
                placeholder="Motivo de consulta, información adicional..."
              />
            </div>

            {/* Resumen */}
            {formData.fecha && formData.hora && pacienteSeleccionado && profesionalSeleccionado && (
              <div className="bg-indigo-50 border border-indigo-200 rounded-lg p-4">
                <div className="flex items-start gap-2">
                  <AlertCircle className="w-5 h-5 text-indigo-600 mt-0.5" />
                  <div>
                    <p className="text-indigo-900 mb-2">
                      <strong>Resumen del Turno:</strong>
                    </p>
                    <ul className="text-indigo-800 space-y-1">
                      <li>
                        📅 Fecha: {new Date(formData.fecha).toLocaleDateString('es-AR', {
                          weekday: 'long',
                          year: 'numeric',
                          month: 'long',
                          day: 'numeric',
                        })}
                      </li>
                      <li>🕐 Hora: {formData.hora} hs</li>
                      <li>👤 Paciente: {pacienteSeleccionado.nombreCompleto}</li>
                      <li>🩺 Profesional: {profesionalSeleccionado.nombreCompleto}</li>
                      <li>📋 Estado: {formData.estado}</li>
                    </ul>
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
              className="px-6 py-2 bg-green-600 text-white rounded-lg hover:bg-green-700 transition"
            >
              {turno ? 'Guardar Cambios' : 'Agendar Turno'}
            </button>
          </div>
        </div>
      </div>

      {/* Modal anidado para agregar paciente */}
      <PacienteModal
        isOpen={showPacienteModal}
        onClose={() => setShowPacienteModal(false)}
        onSave={handleAddPaciente}
        mode="create"
      />
    </>
  );
};
