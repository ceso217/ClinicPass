'use client'
import React, { useState } from 'react';
import { Search, FileText, Calendar, User, ChevronRight, ArrowLeft, Plus, Download } from 'lucide-react';
import {
  mockPacientes,
  mockTratamientos,
  mockFichas,
  type Paciente,
  type Tratamiento,
  type FichaSeguimiento,
} from '../data/mockData';
import { FichaSeguimientoModal } from './modals/FichaSeguimientoModal';
import { TratamientoModal } from './modals/TratamientoModal';

export const Historiales: React.FC = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedPaciente, setSelectedPaciente] = useState<Paciente | null>(null);
  const [ordenFichas, setOrdenFichas] = useState<'desc' | 'asc'>('desc');
  const [tratamientos, setTratamientos] = useState(mockTratamientos);
  const [fichas, setFichas] = useState(mockFichas);

    // Tratamientos
  const [showTratamientoModal, setShowTratamientoModal] = useState(false);
  const [tratamientoEdit, setTratamientoEdit] = useState<any | null>(null);

  // Fichas
  const [showFichaModal, setShowFichaModal] = useState(false);
  const [fichaEdit, setFichaEdit] = useState<any | null>(null);

  const filteredPacientes = mockPacientes.filter((p) =>
    p.nombreCompleto.toLowerCase().includes(searchTerm.toLowerCase()) ||
    p.dni.includes(searchTerm)
  );

  const tratamientosPaciente = selectedPaciente
    ? tratamientos.filter(t => t.pacienteId === selectedPaciente.id)
  : [];


  const fichasPaciente = selectedPaciente
  ? fichas.filter(f => f.pacienteId === selectedPaciente.id)
  : [];
    // ? mockFichas.filter((f) => f.pacienteId === selectedPaciente.id).sort((a, b) => {
    //     const dateA = new Date(a.fecha).getTime();
    //     const dateB = new Date(b.fecha).getTime();
    //     return ordenFichas === 'desc' ? dateB - dateA : dateA - dateB;
    //   })
    // : [];

  const handleSelectPaciente = (paciente: Paciente) => {
    setSelectedPaciente(paciente);
  };

  const handleBack = () => {
    setSelectedPaciente(null);
  };

  const getEstadoColor = (estado: string) => {
    switch (estado) {
      case 'Activo':
        return 'bg-green-100 text-green-700';
      case 'Finalizado':
        return 'bg-gray-100 text-gray-700';
      case 'Programado':
        return 'bg-blue-100 text-blue-700';
      case 'Cancelado':
        return 'bg-red-100 text-red-700';
      default:
        return 'bg-gray-100 text-gray-700';
    }
  };
  const handleSaveTratamiento = (data: any) => {
    if (!selectedPaciente) return;

    if (tratamientoEdit) {
      // Editar
      setTratamientos(prev =>
        prev.map(t =>
          t.id === tratamientoEdit.id ? { ...t, ...data } : t
        )
      );
    } else {
      // Crear
      const newTratamiento = {
        id: tratamientos.length + 1,
        pacienteId: selectedPaciente.id,
        ...data,
      };
      setTratamientos(prev => [...prev, newTratamiento]);
    }
  };


  const handleSaveFicha = (data: any) => {
    if (!selectedPaciente) return;

    if (fichaEdit) {
      setFichas(prev =>
        prev.map(f =>
          f.id === fichaEdit.id ? { ...f, ...data } : f
        )
      );
    } else {
      const newFicha = {
        id: fichas.length + 1,
        pacienteId: selectedPaciente.id,
        profesionalNombre: 'Dr. Mock',
        ...data,
      };

      setFichas(prev => [...prev, newFicha]);
    }
  };


  //*************************************************************************** */
  //TSX
  //*************************************************************************** */
  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <div className="bg-white border-b border-gray-200 px-8 py-6">
        <div className="flex items-center justify-between">
          <div className="flex items-center gap-4">
            {selectedPaciente && (
              <button
                onClick={handleBack}
                className="p-2 hover:bg-gray-100 rounded-lg transition"
              >
                <ArrowLeft className="w-5 h-5 text-gray-600" />
              </button>
            )}
            <div>
              <h1 className="text-gray-900">
                {selectedPaciente ? 'Historial Clínico' : 'Historiales Clínicos'}
              </h1>
              <p className="text-gray-600 mt-1">
                {selectedPaciente
                  ? `${selectedPaciente.nombreCompleto} - DNI: ${selectedPaciente.dni}`
                  : `${filteredPacientes.length} paciente${filteredPacientes.length !== 1 ? 's' : ''}`}
              </p>
            </div>
          </div>
          {selectedPaciente && (
            <button
              onClick={() => console.log('Exportar historial')}
              className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
            >
              <Download className="w-5 h-5" />
              Exportar a DOCX
            </button>
          )}
        </div>
      </div>

      <div className="p-8">
        {!selectedPaciente ? (
          // Lista de pacientes
          <>
            <div className="bg-white rounded-xl shadow-md p-6 mb-6">
              <div className="relative">
                <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
                <input
                  type="text"
                  placeholder="Buscar paciente por nombre o DNI..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
                />
              </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {filteredPacientes.map((paciente) => (
                <div
                  key={paciente.id}
                  onClick={() => handleSelectPaciente(paciente)}
                  className="bg-white rounded-xl shadow-md p-6 hover:shadow-lg transition cursor-pointer"
                >
                  <div className="flex items-start justify-between mb-4">
                    <div className="flex items-center gap-3">
                      <div className="w-12 h-12 bg-indigo-100 rounded-full flex items-center justify-center">
                        <User className="w-6 h-6 text-indigo-600" />
                      </div>
                      <div>
                        <h3 className="text-gray-900">{paciente.nombreCompleto}</h3>
                        <p className="text-gray-600">DNI: {paciente.dni}</p>
                      </div>
                    </div>
                    <ChevronRight className="w-5 h-5 text-gray-400" />
                  </div>
                  <div className="space-y-2 text-gray-600">
                    <p>Edad: {paciente.edad} años</p>
                    <p>
                      Última consulta:{' '}
                      {paciente.ultimaConsulta
                        ? new Date(paciente.ultimaConsulta).toLocaleDateString('es-AR')
                        : 'Sin consultas'}
                    </p>
                  </div>
                </div>
              ))}
            </div>
          </>
        ) : (
          // Vista de historial detallado
          <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
            {/* Información del paciente */}
            <div className="lg:col-span-1 space-y-6">
              <div className="bg-white rounded-xl shadow-md p-6">
                <h3 className="text-gray-900 mb-4">Información del Paciente</h3>
                <div className="space-y-3 text-gray-600">
                  <div>
                    <p className="text-gray-500">Nombre Completo</p>
                    <p className="text-gray-900">{selectedPaciente.nombreCompleto}</p>
                  </div>
                  <div>
                    <p className="text-gray-500">DNI</p>
                    <p className="text-gray-900">{selectedPaciente.dni}</p>
                  </div>
                  <div>
                    <p className="text-gray-500">Edad</p>
                    <p className="text-gray-900">{selectedPaciente.edad} años</p>
                  </div>
                  <div>
                    <p className="text-gray-500">Fecha de Nacimiento</p>
                    <p className="text-gray-900">
                      {new Date(selectedPaciente.fechaNacimiento).toLocaleDateString('es-AR')}
                    </p>
                  </div>
                  <div>
                    <p className="text-gray-500">Dirección</p>
                    <p className="text-gray-900">
                      {selectedPaciente.calle}, {selectedPaciente.localidad}
                    </p>
                  </div>
                  <div>
                    <p className="text-gray-500">Teléfono</p>
                    <p className="text-gray-900">{selectedPaciente.telefono}</p>
                  </div>
                </div>
                <button
                  onClick={() => console.log('Editar paciente')}
                  className="w-full mt-4 px-4 py-2 border border-indigo-600 text-indigo-600 rounded-lg hover:bg-indigo-50 transition"
                >
                  Editar Datos
                </button>
              </div>

              {/* Tratamientos */}
              <div className="bg-white rounded-xl shadow-md p-6">
                <div className="flex items-center justify-between mb-4">
                  <h3 className="text-gray-900">Tratamientos</h3>
                  {/* <button
                    onClick={() => console.log('Agregar tratamiento')}
                    className="p-2 text-indigo-600 hover:bg-indigo-50 rounded-lg transition"
                  >
                    <Plus className="w-5 h-5" />
                  </button> */}
                  <button
                    onClick={() => {
                      setTratamientoEdit(null);
                      setShowTratamientoModal(true);
                    }}
                    className="p-2 text-indigo-600 hover:bg-indigo-50 rounded-lg transition"
                  >
                    <Plus className="w-5 h-5" />
                  </button>
                </div>
                {tratamientosPaciente.length === 0 ? (
                  <p className="text-gray-500 text-center py-4">Sin tratamientos</p>
                ) : (
                  <div className="space-y-3">
                    {tratamientosPaciente.map((tratamiento) => (
                      <div
                        onClick={() => {
                          setTratamientoEdit(tratamiento);
                          setShowTratamientoModal(true);
                        }}
                        key={tratamiento.id}
                        className="p-3 border border-gray-200 rounded-lg hover:border-indigo-300 transition cursor-pointer"
                      >
                        <div className="flex items-start justify-between mb-2">
                          <p className="text-gray-900">{tratamiento.tipo}</p>
                          <span className={`px-2 py-1 rounded-full text-xs ${getEstadoColor(tratamiento.estado)}`}>
                            {tratamiento.estado}
                          </span>
                        </div>
                        <p className="text-gray-600 mb-2">{tratamiento.descripcion}</p>
                        <p className="text-gray-500">
                          Inicio: {new Date(tratamiento.fechaInicio).toLocaleDateString('es-AR')}
                        </p>
                      </div>
                    ))}
                  </div>
                )}
              </div>
            </div>

            {/* Fichas de seguimiento */}
            <div className="lg:col-span-2">
              <div className="bg-white rounded-xl shadow-md">
                <div className="p-6 border-b border-gray-200">
                  <div className="flex items-center justify-between">
                    <h3 className="text-gray-900">Fichas de Seguimiento</h3>
                    <div className="flex items-center gap-3">
                      <select
                        value={ordenFichas}
                        onChange={(e) => setOrdenFichas(e.target.value as 'desc' | 'asc')}
                        className="px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                      >
                        <option value="desc">Más recientes primero</option>
                        <option value="asc">Más antiguas primero</option>
                      </select>
                      {/* <button
                        // onClick={() => console.log('Agregar ficha')}
                        
                        className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
                      >
                        <Plus className="w-5 h-5" />
                        Nueva Ficha
                      </button> */}
                      <button
                        onClick={() => {
                          setFichaEdit(null);
                          setShowFichaModal(true);
                        }}
                        className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
                      >
                        <Plus className="w-5 h-5" />
                        Nueva Ficha
                      </button>
                    </div>
                  </div>
                </div>

                <div className="p-6">
                  {fichasPaciente.length === 0 ? (
                    <div className="text-center py-12 text-gray-500">
                      <FileText className="w-12 h-12 mx-auto mb-4 text-gray-400" />
                      <p>No hay fichas de seguimiento registradas</p>
                    </div>
                  ) : (
                    <div className="space-y-6">
                      {fichasPaciente.map((ficha, index) => (
                        <div
                          key={ficha.id}
                          className="relative pl-8 pb-6 border-l-2 border-gray-200 last:border-l-0 last:pb-0"
                        >
                          {/* Punto en la línea de tiempo */}
                          <div className="absolute left-0 top-0 w-4 h-4 bg-indigo-600 rounded-full -ml-[9px]"></div>

                          <div className="bg-gray-50 rounded-lg p-6">
                            <div className="flex items-start justify-between mb-4">
                              <div>
                                <div className="flex items-center gap-3 mb-2">
                                  <Calendar className="w-5 h-5 text-indigo-600" />
                                  <p className="text-gray-900">
                                    {new Date(ficha.fecha).toLocaleDateString('es-AR', {
                                      weekday: 'long',
                                      year: 'numeric',
                                      month: 'long',
                                      day: 'numeric',
                                    })}
                                  </p>
                                </div>
                                <div className="flex items-center gap-2 text-gray-600">
                                  <User className="w-4 h-4" />
                                  <p>{ficha.profesionalNombre}</p>
                                </div>
                              </div>
                            </div>

                            <div className="space-y-3">
                              <div>
                                <p className="text-gray-500 mb-1">Diagnóstico</p>
                                <p className="text-gray-900">{ficha.diagnostico}</p>
                              </div>
                              <div>
                                <p className="text-gray-500 mb-1">Observaciones</p>
                                <p className="text-gray-900">{ficha.observaciones}</p>
                              </div>
                              <div>
                                <p className="text-gray-500 mb-1">Antecedentes</p>
                                <p className="text-gray-900">{ficha.antecedentes}</p>
                              </div>
                              {ficha.proximaConsulta && (
                                <div className="pt-3 border-t border-gray-200">
                                  <p className="text-gray-500 mb-1">Próxima Consulta</p>
                                  <p className="text-indigo-600">
                                    {new Date(ficha.proximaConsulta).toLocaleDateString('es-AR')}
                                  </p>
                                </div>
                              )}
                            </div>
                          </div>
                        </div>
                      ))}
                    </div>
                  )}
                </div>
              </div>
            </div>
          </div>
        )}
      </div>
      <TratamientoModal
        isOpen={showTratamientoModal}
        onClose={() => setShowTratamientoModal(false)}
        onSave={handleSaveTratamiento}
        data={tratamientoEdit}
        mode={tratamientoEdit ? 'edit' : 'create'}
      />

      <FichaSeguimientoModal
        isOpen={showFichaModal}
        onClose={() => setShowFichaModal(false)}
        onSave={handleSaveFicha}
        data={fichaEdit}
        mode={fichaEdit ? 'edit' : 'create'}
      />
    </div>
  );
};
