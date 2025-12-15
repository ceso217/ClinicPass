'use client';
import React from 'react';
import { X, User } from 'lucide-react';
import { Paciente } from '@/data/mockData';

interface Props {
  isOpen: boolean;
  onClose: () => void;
  pacientes: Paciente[];
  onCreate: (paciente: Paciente) => void;
}

export const CrearHistorialModal: React.FC<Props> = ({
  isOpen,
  onClose,
  pacientes,
  onCreate,
}) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
      <div className="bg-white rounded-xl shadow-xl max-w-xl w-full">
        {/* Header */}
        <div className="p-6 border-b border-gray-200 flex justify-between items-center">
          <h2 className="text-gray-900">Crear Historial Clínico</h2>
          <button onClick={onClose} className="p-2 hover:bg-gray-100 rounded-lg">
            <X className="w-5 h-5 text-gray-600" />
          </button>
        </div>

        {/* Body */}
        <div className="p-6 space-y-3 max-h-[60vh] overflow-y-auto">
          {pacientes.length === 0 ? (
            <p className="text-gray-500 text-center">
              Todos los pacientes ya tienen historial
            </p>
          ) : (
            pacientes.map(p => (
              <button
                key={p.id}
                onClick={() => onCreate(p)}
                className="w-full flex items-center gap-3 p-3 border border-gray-200 rounded-lg hover:border-indigo-400 transition"
              >
                <div className="w-10 h-10 bg-indigo-100 rounded-full flex items-center justify-center">
                  <User className="w-5 h-5 text-indigo-600" />
                </div>
                <div className="text-left">
                  <p className="text-gray-900">{p.nombreCompleto}</p>
                  <p className="text-gray-500 text-sm">DNI: {p.dni}</p>
                </div>
              </button>
            ))
          )}
        </div>
      </div>
    </div>
  );
};
