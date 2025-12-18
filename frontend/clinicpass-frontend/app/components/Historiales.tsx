'use client'
import React, { useState, useEffect } from 'react';

import type { Paciente } from '../types/paciente';
//import type { HistorialTratamiento } from '../types/tratamiento';
import type { HistoriaClinicaResumen } from '../types/HistoriaClinicaResumen';
import type { HistoriaClinicaDetalle } from '../types/HistoriaClinicaDetalle';
import { TratamientoModal } from './modals/TratamientoModal';
import { FichaSeguimientoModal } from './modals/FichaSeguimientoModal';


import {
  Search,
  FileText,
  Calendar,
  User,
  ChevronRight,
  ArrowLeft,
  Plus,
  Download
} from 'lucide-react';

const API_URL = process.env.NEXT_PUBLIC_API_URL;

// import { FichaSeguimientoModal } from './modals/FichaSeguimientoModal';
// import { TratamientoModal } from './modals/TratamientoModal';

export const Historiales: React.FC = () => {

  // ================================
  // Estados generales
  // ================================
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [searchTerm, setSearchTerm] = useState('');
  const [ordenFichas, setOrdenFichas] = useState<'desc' | 'asc'>('desc');

  // ================================
  // Datos desde API
  // ================================
  const [resumenes, setResumenes] = useState<HistoriaClinicaResumen[]>([]);
  const [detalle, setDetalle] = useState<HistoriaClinicaDetalle | null>(null);

  // ================================
  // Selección
  // ================================
  const [selectedPaciente, setSelectedPaciente] = useState<HistoriaClinicaResumen | null>(null);

  // ================================
  // Modales (a futuro)
  // ================================
  const [showTratamientoModal, setShowTratamientoModal] = useState(false);
  const [showFichaModal, setShowFichaModal] = useState(false);

  const [tratamientoEdit, setTratamientoEdit] = useState<any | null>(null);
  const [fichaEdit, setFichaEdit] = useState<any | null>(null);


  // =========================================================
  // FETCH 1 – Obtener lista de historias clínicas (RESUMEN)
  // GET /HistoriaClinica
  // =========================================================
  const fetchHistoriasClinicas = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/api/HistoriaClinica`);

      if (!res.ok) throw new Error('Error al obtener historias clínicas');

      const data: HistoriaClinicaResumen[] = await res.json();
      setResumenes(data);
    } catch (err) {
      setError('No se pudieron cargar los historiales');
    } finally {
      setLoading(false);
    }
  };

  // =========================================================
  // FETCH 2 – Obtener detalle de una historia clínica
  // GET /HistoriaClinica/{id}
  // =========================================================
  const fetchDetalleHistoriaClinica = async (idHistoriaClinica: number) => {
    try {
      const res = await fetch(`${API_URL}/api/HistoriaClinica/detalle/${idHistoriaClinica}`);

      if (!res.ok) throw new Error('Error al obtener detalle');

      const data: HistoriaClinicaDetalle = await res.json();
      setDetalle(data);
    } catch {
      setDetalle(null);
    }
  };

  // =========================================================
  // Effects
  // =========================================================
  useEffect(() => {
    fetchHistoriasClinicas();
  }, []);

  useEffect(() => {
    if (selectedPaciente) {
      fetchDetalleHistoriaClinica(selectedPaciente.idHistorialClinico);
    } else {
      setDetalle(null);
    }
  }, [selectedPaciente]);

  // =========================================================
  // Helpers
  // =========================================================
  const handleSelectPaciente = (historia: HistoriaClinicaResumen) => {
    setSelectedPaciente(historia);
  };

  const handleBack = () => {
    setSelectedPaciente(null);
  };

  const filteredHistorias = resumenes.filter(h =>
    h.paciente.nombreCompleto.toLowerCase().includes(searchTerm.toLowerCase()) ||
    h.paciente.dni.includes(searchTerm)
  );

  const fichasOrdenadas = detalle?.fichas
    ?.slice()
    .sort((a, b) =>
      ordenFichas === 'desc'
        ? new Date(b.fechaCreacion).getTime() - new Date(a.fechaCreacion).getTime()
        : new Date(a.fechaCreacion).getTime() - new Date(b.fechaCreacion).getTime()
    ) ?? [];

  // =========================================================
  // FUNCIONES VIEJAS (NO SE USAN MÁS)
  // =========================================================
  /*
  const handleSaveTratamiento = () => {}
  
  const handleSaveFicha = () => {}
  */
    const handleSaveTratamiento = async (data: {
      nombre: string;
      descripcion: string;
    }) => {
      try {
        if (!selectedPaciente) return;

        const url = tratamientoEdit
          ? `${API_URL}/api/tratamientos/${tratamientoEdit.idTratamiento}`
          : `${API_URL}/api/tratamientos`;

        const method = tratamientoEdit ? 'PUT' : 'POST';

        const res = await fetch(url, {
          method,
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            nombre: data.nombre,
            descripcion: data.descripcion,
          }),
        });

        if (!res.ok) {
          throw new Error('Error al guardar tratamiento');
        }

        // 🔄 refrescamos el detalle
        await fetchDetalleHistoriaClinica(selectedPaciente.idHistorialClinico);

        setShowTratamientoModal(false);
        setTratamientoEdit(null);
      } catch (error) {
        console.error(error);
        alert('No se pudo guardar el tratamiento');
      }
    };

  


    const handleSaveFicha = async (data: {
  tratamientoId: number;
  observaciones: string;
}) => {
  try {
    if (!selectedPaciente) return;

    const idUsuarioLogueado = 1; // ⚠️ reemplazar por auth real

    const payload = {
      idUsuario: idUsuarioLogueado,
      idHistorialClinico: selectedPaciente.idHistorialClinico,
      tratamientoId: data.tratamientoId,
      observaciones: data.observaciones,
    };

    const url = fichaEdit
      ? `${API_URL}/api/FichasDeSeguimiento/${fichaEdit.idFichaSeguimiento}`
      : `${API_URL}/api/FichasDeSeguimiento`;

    const method = fichaEdit ? 'PUT' : 'POST';

    const res = await fetch(url, {
          method,
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(payload),
        });

        if (!res.ok) {
          throw new Error('Error al guardar ficha');
        }

        // 🔄 refrescamos fichas
        await fetchDetalleHistoriaClinica(selectedPaciente.idHistorialClinico);

        setShowFichaModal(false);
        setFichaEdit(null);
      } catch (error) {
        console.error(error);
        alert('No se pudo guardar la ficha de seguimiento');
      }
    };


  // =========================================================
  // RENDER
  // =========================================================
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
                  ? `${selectedPaciente.paciente.nombreCompleto} - DNI: ${selectedPaciente.paciente.dni}`
                  : `${filteredHistorias.length} paciente${filteredHistorias.length !== 1 ? 's' : ''}`}
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
              {filteredHistorias.map((h) => (
                <div
                  key={h.idHistorialClinico}
                  onClick={() => handleSelectPaciente(h)}
                  className="bg-white rounded-xl shadow-md p-6 hover:shadow-lg transition cursor-pointer"
                >
                  <div className="flex items-start justify-between mb-4">
                    <div className="flex items-center gap-3">
                      <div className="w-12 h-12 bg-indigo-100 rounded-full flex items-center justify-center">
                        <User className="w-6 h-6 text-indigo-600" />
                      </div>

                      <div>
                        <h3 className="text-gray-900">{h.paciente.nombreCompleto}</h3>
                        <p className="text-gray-600">DNI: {h.paciente.dni}</p>
                        <p className="text-gray-500 text-sm">
                          Edad:{h.paciente.edad}
                        </p>
                      </div>
                    </div>

                    <ChevronRight className="w-5 h-5 text-gray-400" />
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
                    <p className="text-gray-900">{detalle?.paciente.nombreCompleto}</p>
                  </div>
                  <div>
                    <p className="text-gray-500">DNI</p>
                    <p className="text-gray-900">{detalle?.paciente.dni}</p>
                  </div>

                  <div>
                    <p className="text-gray-500">Fecha de Nacimiento</p>
                    <p className="text-gray-900">
                      {detalle?.paciente.fechaNacimiento}
                    </p>

                  </div>
                  <div>
                    <p className="text-gray-500">Dirección</p>
                    <p className="text-gray-900">
                      {detalle?.paciente.calle}, {detalle?.paciente.localidad} , {detalle?.paciente.provincia}
                    </p>
                  </div>
                  <div>
                    <p className="text-gray-500">Teléfono</p>
                   
                    <p className="text-gray-900">{detalle?.paciente.telefono}</p> 
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
                {detalle?.tratamientos.length === 0 ? (
                  <p className="text-gray-500 text-center py-4">
                    Sin tratamientos
                  </p>
                ) : (
                  <div className="space-y-3">
                    {detalle?.tratamientos.map((t) => (
                      <div
                        key={t.idTratamiento}
                        className="p-3 border border-gray-200 rounded-lg"
                      >
                        <div className="flex justify-between mb-1">
                          <p className="text-gray-900">
                            {t.nombre}
                          </p>
                          <span
                            className={`text-xs px-2 py-1 rounded-full ${t.activo
                              ? 'bg-green-100 text-green-700'
                              : 'bg-gray-100 text-gray-700'
                              }`}
                          >
                            {t.activo ? 'Activo' : 'Finalizado'}
                          </span>
                        </div>

                        <p className="text-gray-600">{t.descripcion}</p>
                        <p className="text-gray-500 text-sm">
                          Inicio:{' '}
                          {new Date(t.fechaInicio).toLocaleDateString('es-AR')}
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
                  {detalle?.fichas.length === 0 ? (
                    <div className="text-center py-12 text-gray-500">
                      <FileText className="w-12 h-12 mx-auto mb-4 text-gray-400" />
                      <p>No hay fichas de seguimiento registradas</p>
                    </div>
                  ) : (
                    <div className="space-y-6">
                      {detalle?.fichas.map((ficha, index) => (
                        <div
                          key={ficha.idFichaSeguimiento}
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
                                    {new Date(ficha.fechaCreacion).toLocaleDateString('es-AR', {
                                      weekday: 'long',
                                      year: 'numeric',
                                      month: 'long',
                                      day: 'numeric',
                                    })}
                                  </p>
                                </div>
                                <div className="flex items-center gap-2 text-gray-600">
                                  <User className="w-4 h-4" />
                                  <p>{ficha.nombreProfesional}</p>
                                </div>
                              </div>
                            </div>

                            <div className="space-y-3">
                              <div>
                                <p className="text-gray-500 mb-1">Diagnóstico</p>
                                {/* <p className="text-gray-900">{ficha.idUsuario}</p> */}
                              </div>
                              <div>
                                <p className="text-gray-500 mb-1">Observaciones</p>
                                <p className="text-gray-900">{ficha.observaciones}</p>
                              </div>
                              <div>
                                <p className="text-gray-500 mb-1">Antecedentes</p>
                                {/* <p className="text-gray-900">{ficha.}</p> */}
                              </div>
                              {/* {ficha.proximaConsulta && (
                                <div className="pt-3 border-t border-gray-200">
                                  <p className="text-gray-500 mb-1">Próxima Consulta</p>
                                  <p className="text-indigo-600">
                                    {new Date(ficha.proximaConsulta).toLocaleDateString('es-AR')}
                                  </p>
                                </div>
                              )} */}
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

