
'use client';
import React, { useEffect, useState } from 'react';
import { X, Stethoscope, Calendar } from 'lucide-react';

export type EstadoTratamiento =
  | 'Activo'
  | 'Finalizado'
  | 'Programado'
  | 'Cancelado';

export interface TratamientoForm {
  tipo: string;
  descripcion: string;
  fechaInicio: string;
  estado: EstadoTratamiento;
}

interface TratamientoModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (data: TratamientoForm) => void;
  data?: TratamientoForm | null;
  mode: 'create' | 'edit';
}

export const TratamientoModal: React.FC<TratamientoModalProps> = ({
  isOpen,
  onClose,
  onSave,
  data,
  mode,
}) => {
  const [formData, setFormData] = useState<TratamientoForm>({
    tipo: '',
    descripcion: '',
    fechaInicio: '',
    estado: 'Activo',
  });

  const [errors, setErrors] = useState<Record<string, string>>({});

  useEffect(() => {
    if (data && mode === 'edit') {
      setFormData(data);
    } else {
      setFormData({
        tipo: '',
        descripcion: '',
        fechaInicio: '',
        estado: 'Activo',
      });
    }
    setErrors({});
  }, [data, mode, isOpen]);

  const validate = () => {
    const newErrors: Record<string, string> = {};

    if (!formData.tipo.trim()) newErrors.tipo = 'El tipo de tratamiento es requerido';
    if (!formData.descripcion.trim()) newErrors.descripcion = 'La descripción es requerida';
    if (!formData.fechaInicio) newErrors.fechaInicio = 'La fecha de inicio es requerida';

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = () => {
    if (!validate()) return;
    onSave(formData);
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
      <div className="bg-white rounded-xl shadow-xl max-w-xl w-full">

        {/* Header */}
        <div className="border-b border-gray-200 p-6 flex items-center justify-between">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 bg-indigo-100 rounded-full flex items-center justify-center">
              <Stethoscope className="w-6 h-6 text-indigo-600" />
            </div>
            <div>
              <h2 className="text-gray-900">
                {mode === 'create' ? 'Agregar Tratamiento' : 'Editar Tratamiento'}
              </h2>
              <p className="text-gray-600">Datos clínicos del tratamiento</p>
            </div>
          </div>

          <button onClick={onClose} className="p-2 hover:bg-gray-100 rounded-lg">
            <X className="w-5 h-5 text-gray-600" />
          </button>
        </div>

        {/* Body */}
        <div className="p-6 space-y-4">
          <div>
            <label className="block text-gray-700 mb-2">
              Tipo de Tratamiento <span className="text-red-500">*</span>
            </label>
            <input
              type="text"
              value={formData.tipo}
              onChange={(e) => setFormData({ ...formData, tipo: e.target.value })}
              className={`w-full px-4 py-2 border rounded-lg ${
                errors.tipo ? 'border-red-500' : 'border-gray-300'
              }`}
              placeholder="Ej: Kinesiología"
            />
            {errors.tipo && <p className="text-red-500 mt-1">{errors.tipo}</p>}
          </div>

          <div>
            <label className="block text-gray-700 mb-2">
              Descripción <span className="text-red-500">*</span>
            </label>
            <textarea
              rows={4}
              value={formData.descripcion}
              onChange={(e) => setFormData({ ...formData, descripcion: e.target.value })}
              className={`w-full px-4 py-2 border rounded-lg ${
                errors.descripcion ? 'border-red-500' : 'border-gray-300'
              }`}
              placeholder="Detalle del tratamiento..."
            />
            {errors.descripcion && (
              <p className="text-red-500 mt-1">{errors.descripcion}</p>
            )}
          </div>

          <div>
            <label className="block text-gray-700 mb-2">
              Fecha de Inicio <span className="text-red-500">*</span>
            </label>
            <div className="relative">
              <Calendar className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-gray-400" />
              <input
                type="date"
                value={formData.fechaInicio}
                onChange={(e) => setFormData({ ...formData, fechaInicio: e.target.value })}
                className={`w-full pl-10 pr-4 py-2 border rounded-lg ${
                  errors.fechaInicio ? 'border-red-500' : 'border-gray-300'
                }`}
              />
            </div>
            {errors.fechaInicio && (
              <p className="text-red-500 mt-1">{errors.fechaInicio}</p>
            )}
          </div>

          <div>
            <label className="block text-gray-700 mb-2">Estado</label>
            <select
              value={formData.estado}
              onChange={(e) =>
                setFormData({ ...formData, estado: e.target.value as EstadoTratamiento })
              }
              className="w-full px-4 py-2 border border-gray-300 rounded-lg"
            >
              <option value="Activo">Activo</option>
              <option value="Programado">Programado</option>
              <option value="Finalizado">Finalizado</option>
              <option value="Cancelado">Cancelado</option>
            </select>
          </div>
        </div>

        {/* Footer */}
        <div className="bg-gray-50 border-t border-gray-200 p-6 flex justify-end gap-3">
          <button
            onClick={onClose}
            className="px-6 py-2 border border-gray-300 text-gray-700 rounded-lg"
          >
            Cancelar
          </button>
          <button
            onClick={handleSubmit}
            className="px-6 py-2 bg-indigo-600 text-white rounded-lg"
          >
            {mode === 'create' ? 'Agregar' : 'Guardar'}
          </button>
        </div>
      </div>
    </div>
  );
};
