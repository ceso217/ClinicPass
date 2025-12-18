'use client';
import React, { useEffect, useState } from 'react';
import { X, FileText, Calendar } from 'lucide-react';

export interface FichaSeguimientoForm {
  idUsuario: number;
  idHistorialClinico: number;
  tratamientoId: number;
  observaciones: string;
}
// "idUsuario": 0,
//   "idHistorialClinico": 0,
//   "tratamientoId": 0,
//   // "observaciones": "string"

interface FichaSeguimientoModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (data: FichaSeguimientoForm) => void;
  data?: FichaSeguimientoForm | null;
  mode: 'create' | 'edit';
}

export const FichaSeguimientoModal: React.FC<FichaSeguimientoModalProps> = ({
  isOpen,
  onClose,
  onSave,
  data,
  mode,
}) => {

const [formData, setFormData] = useState<FichaSeguimientoForm>({
    idUsuario: 0,
    idHistorialClinico: 0,
    tratamientoId: 0,
    observaciones: '',
});

  const [errors, setErrors] = useState<Record<string, string>>({});

  useEffect(() => {
    if (data && mode === 'edit') {
        setFormData({
            idUsuario: data.idUsuario,
            idHistorialClinico: data.idHistorialClinico,
            tratamientoId: data.tratamientoId,
            observaciones: data.observaciones,
        });
    } else {
        setFormData({
            idHistorialClinico: 0,
            tratamientoId: 0,
            idUsuario: 0,
            observaciones: '',
        });
    }
    setErrors({});
  }, [data, mode, isOpen]);

  const validate = () => {
    const newErrors: Record<string, string> = {};
    if (!formData.observaciones.trim())
      newErrors.observaciones = 'Las observaciones son requeridas';

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
              <FileText className="w-6 h-6 text-indigo-600" />
            </div>
            <div>
              <h2 className="text-gray-900">
                {mode === 'create' ? 'Nueva Ficha de Seguimiento' : 'Editar Ficha'}
              </h2>
              <p className="text-gray-600">Evolución del paciente</p>
            </div>
          </div>

          <button onClick={onClose} className="p-2 hover:bg-gray-100 rounded-lg">
            <X className="w-5 h-5 text-gray-600" />
          </button>
        </div>
        {/* Body */}
        <div className="p-6 space-y-4">
        {/* Fecha */}
        {/* <div>
            <label className="block text-gray-700 mb-2">
            Fecha <span className="text-red-500">*</span>
            </label>
            <div className="relative">
            <Calendar className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-gray-400" />
            <input
                type="date"
                value={formData.fecha}
                onChange={(e) => setFormData({ ...formData, fecha: e.target.value })}
                className={`w-full pl-10 pr-4 py-2 border rounded-lg ${
                errors.fecha ? 'border-red-500' : 'border-gray-300'
                }`}
            />
            </div>
            {errors.fecha && <p className="text-red-500 mt-1">{errors.fecha}</p>}
        </div> */}

        {/* Diagnóstico */}
        <div>
            <label className="block text-gray-700 mb-2">
              HistorialClinico
            </label>
            <input
            type="text"
            value={formData.idHistorialClinico}
            onChange={(e) =>
                setFormData({ ...formData, idHistorialClinico: Number(e.target.value) })
            }
            className="w-full px-4 py-2 border border-gray-300 rounded-lg"
            placeholder="Diagnóstico clínico"
            />
        </div>

        {/* Antecedentes */}
        <div>
            <label className="block text-gray-700 mb-2">
            Tratamiento
            </label>
            <textarea
            rows={3}
            value={formData.tratamientoId}
            onChange={(e) =>
                setFormData({ ...formData, tratamientoId: Number(e.target.value) })
            }
            className="w-full px-4 py-2 border border-gray-300 rounded-lg"
            placeholder="Antecedentes relevantes del paciente"
            />
        </div>

        {/* Observaciones */}
        <div>
            <label className="block text-gray-700 mb-2">
            Observaciones <span className="text-red-500">*</span>
            </label>
            <textarea
            rows={5}
            value={formData.observaciones}
            onChange={(e) =>
                setFormData({ ...formData, observaciones: e.target.value })
            }
            className={`w-full px-4 py-2 border rounded-lg ${
                errors.observaciones ? 'border-red-500' : 'border-gray-300'
            }`}
            placeholder="Evolución, respuestas al tratamiento, observaciones clínicas..."
            />
            {errors.observaciones && (
            <p className="text-red-500 mt-1">{errors.observaciones}</p>
            )}
        </div>

        {/* Próxima consulta */}
        <div>
            <label className="block text-gray-700 mb-2">
            profesional
            </label>
            <input
            type="text"
            value={formData.idUsuario}
            onChange={(e) =>
                setFormData({ ...formData, idUsuario: Number(e.target.value) })
            }
            className="w-full px-4 py-2 border border-gray-300 rounded-lg"
            />
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
            {mode === 'create' ? 'Guardar' : 'Actualizar'}
          </button>
        </div>
      </div>
    </div>
  );
};
