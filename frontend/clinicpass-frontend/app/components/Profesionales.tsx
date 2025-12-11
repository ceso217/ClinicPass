'use client'
import React, { useState } from 'react';
import { Search, Plus, Edit, UserX, UserCheck, Filter, X } from 'lucide-react';
import { mockProfesionales, type Profesional } from '../data/mockData';

export const Profesionales: React.FC = () => {
  const [profesionales, setProfesionales] = useState<Profesional[]>(mockProfesionales);
  const [filteredProfesionales, setFilteredProfesionales] = useState<Profesional[]>(mockProfesionales);
  const [searchTerm, setSearchTerm] = useState('');
  const [filterEspecialidad, setFilterEspecialidad] = useState('');
  const [filterActivo, setFilterActivo] = useState<string>('');
  const [showModal, setShowModal] = useState(false);
  const [selectedProfesional, setSelectedProfesional] = useState<Profesional | null>(null);
  const [formData, setFormData] = useState<Partial<Profesional>>({});

  // Filtrar profesionales
  const handleSearch = (term: string, especialidad: string, activo: string) => {
    let filtered = profesionales;

    if (term) {
      filtered = filtered.filter(
        (p) =>
          p.nombreCompleto.toLowerCase().includes(term.toLowerCase()) ||
          p.dni.includes(term) ||
          p.correo.toLowerCase().includes(term.toLowerCase())
      );
    }

    if (especialidad) {
      filtered = filtered.filter((p) =>
        p.especialidad.toLowerCase().includes(especialidad.toLowerCase())
      );
    }

    if (activo !== '') {
      const isActivo = activo === 'true';
      filtered = filtered.filter((p) => p.activo === isActivo);
    }

    setFilteredProfesionales(filtered);
  };

  const handleSearchChange = (value: string) => {
    setSearchTerm(value);
    handleSearch(value, filterEspecialidad, filterActivo);
  };

  const handleEspecialidadChange = (value: string) => {
    setFilterEspecialidad(value);
    handleSearch(searchTerm, value, filterActivo);
  };

  const handleActivoChange = (value: string) => {
    setFilterActivo(value);
    handleSearch(searchTerm, filterEspecialidad, value);
  };

  const handleClearFilters = () => {
    setSearchTerm('');
    setFilterEspecialidad('');
    setFilterActivo('');
    setFilteredProfesionales(profesionales);
  };

  // Abrir modal para agregar/editar
  const handleOpenModal = (profesional?: Profesional) => {
    if (profesional) {
      setSelectedProfesional(profesional);
      setFormData(profesional);
    } else {
      setSelectedProfesional(null);
      setFormData({ activo: true, rol: 2 });
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedProfesional(null);
    setFormData({});
  };

  const handleSave = () => {
    if (selectedProfesional) {
      // Editar
      const updated = profesionales.map((p) =>
        p.id === selectedProfesional.id ? { ...p, ...formData } as Profesional : p
      );
      setProfesionales(updated);
      setFilteredProfesionales(updated);
    } else {
      // Agregar
      const newProfesional: Profesional = {
        id: profesionales.length + 1,
        nombreCompleto: formData.nombreCompleto || '',
        dni: formData.dni || '',
        correo: formData.correo || '',
        especialidad: formData.especialidad || '',
        telefono: formData.telefono || '',
        rol: formData.rol || 2,
        activo: formData.activo !== undefined ? formData.activo : true,
      };
      const updated = [...profesionales, newProfesional];
      setProfesionales(updated);
      setFilteredProfesionales(updated);
    }
    handleCloseModal();
  };

  const handleToggleActivo = (profesional: Profesional) => {
    const updated = profesionales.map((p) =>
      p.id === profesional.id ? { ...p, activo: !p.activo } : p
    );
    setProfesionales(updated);
    handleSearch(searchTerm, filterEspecialidad, filterActivo);
  };

  const especialidades = Array.from(new Set(profesionales.map((p) => p.especialidad)));
  const activosCount = filteredProfesionales.filter((p) => p.activo).length;
  const inactivosCount = filteredProfesionales.filter((p) => !p.activo).length;

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="bg-white border-b border-gray-200 px-8 py-6">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-gray-900">Gestión de Profesionales</h1>
            <p className="text-gray-600 mt-1">
              {filteredProfesionales.length} profesional{filteredProfesionales.length !== 1 ? 'es' : ''} •{' '}
              <span className="text-green-600">{activosCount} activo{activosCount !== 1 ? 's' : ''}</span> •{' '}
              <span className="text-red-600">{inactivosCount} inactivo{inactivosCount !== 1 ? 's' : ''}</span>
            </p>
          </div>
          <button
            onClick={() => handleOpenModal()}
            className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
          >
            <Plus className="w-5 h-5" />
            Agregar Profesional
          </button>
        </div>
      </div>

      <div className="p-8">
        {/* Filtros */}
        <div className="bg-white rounded-xl shadow-md p-6 mb-6">
          <div className="flex items-center gap-2 mb-4">
            <Filter className="w-5 h-5 text-gray-600" />
            <h2 className="text-gray-900">Filtros de Búsqueda</h2>
          </div>
          <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
            <div className="relative">
              <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
              <input
                type="text"
                placeholder="Buscar por nombre, DNI o correo..."
                value={searchTerm}
                onChange={(e) => handleSearchChange(e.target.value)}
                className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
              />
            </div>
            <select
              value={filterEspecialidad}
              onChange={(e) => handleEspecialidadChange(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
            >
              <option value="">Todas las especialidades</option>
              {especialidades.map((esp) => (
                <option key={esp} value={esp}>
                  {esp}
                </option>
              ))}
            </select>
            <select
              value={filterActivo}
              onChange={(e) => handleActivoChange(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
            >
              <option value="">Todos los estados</option>
              <option value="true">Activos</option>
              <option value="false">Inactivos</option>
            </select>
            <button
              onClick={handleClearFilters}
              className="border border-gray-300 text-gray-700 px-4 py-2 rounded-lg hover:bg-gray-50 transition flex items-center justify-center gap-2"
            >
              <X className="w-4 h-4" />
              Limpiar Filtros
            </button>
          </div>
        </div>

        {/* Tabla */}
        <div className="bg-white rounded-xl shadow-md overflow-hidden">
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead className="bg-gray-50 border-b border-gray-200">
                <tr>
                  <th className="px-6 py-4 text-left text-gray-900">Nombre Completo</th>
                  <th className="px-6 py-4 text-left text-gray-900">DNI</th>
                  <th className="px-6 py-4 text-left text-gray-900">Especialidad</th>
                  <th className="px-6 py-4 text-left text-gray-900">Correo</th>
                  <th className="px-6 py-4 text-left text-gray-900">Teléfono</th>
                  <th className="px-6 py-4 text-center text-gray-900">Estado</th>
                  <th className="px-6 py-4 text-center text-gray-900">Acciones</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-200">
                {filteredProfesionales.length === 0 ? (
                  <tr>
                    <td colSpan={7} className="px-6 py-8 text-center text-gray-500">
                      No se encontraron profesionales
                    </td>
                  </tr>
                ) : (
                  filteredProfesionales.map((profesional) => (
                    <tr key={profesional.id} className="hover:bg-gray-50 transition">
                      <td className="px-6 py-4 text-gray-900">{profesional.nombreCompleto}</td>
                      <td className="px-6 py-4 text-gray-700">{profesional.dni}</td>
                      <td className="px-6 py-4 text-gray-700">{profesional.especialidad}</td>
                      <td className="px-6 py-4 text-gray-700">{profesional.correo}</td>
                      <td className="px-6 py-4 text-gray-700">{profesional.telefono}</td>
                      <td className="px-6 py-4">
                        <div className="flex justify-center">
                          {profesional.activo ? (
                            <span className="px-3 py-1 bg-green-100 text-green-700 rounded-full">
                              Activo
                            </span>
                          ) : (
                            <span className="px-3 py-1 bg-red-100 text-red-700 rounded-full">
                              Inactivo
                            </span>
                          )}
                        </div>
                      </td>
                      <td className="px-6 py-4">
                        <div className="flex items-center justify-center gap-2">
                          <button
                            onClick={() => handleOpenModal(profesional)}
                            className="p-2 text-blue-600 hover:bg-blue-50 rounded-lg transition"
                            title="Editar"
                          >
                            <Edit className="w-4 h-4" />
                          </button>
                          <button
                            onClick={() => handleToggleActivo(profesional)}
                            className={`p-2 rounded-lg transition ${
                              profesional.activo
                                ? 'text-red-600 hover:bg-red-50'
                                : 'text-green-600 hover:bg-green-50'
                            }`}
                            title={profesional.activo ? 'Dar de baja' : 'Dar de alta'}
                          >
                            {profesional.activo ? (
                              <UserX className="w-4 h-4" />
                            ) : (
                              <UserCheck className="w-4 h-4" />
                            )}
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        </div>
      </div>

      {/* Modal */}
      {showModal && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-xl shadow-xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
            <div className="p-6 border-b border-gray-200">
              <h2 className="text-gray-900">
                {selectedProfesional ? 'Editar Profesional' : 'Agregar Profesional'}
              </h2>
            </div>
            <div className="p-6 space-y-4">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="block text-gray-700 mb-2">Nombre Completo</label>
                  <input
                    type="text"
                    value={formData.nombreCompleto || ''}
                    onChange={(e) => setFormData({ ...formData, nombreCompleto: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                    placeholder="Dr. Juan Pérez"
                  />
                </div>
                <div>
                  <label className="block text-gray-700 mb-2">DNI</label>
                  <input
                    type="text"
                    value={formData.dni || ''}
                    onChange={(e) => setFormData({ ...formData, dni: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                    placeholder="12345678"
                  />
                </div>
                <div>
                  <label className="block text-gray-700 mb-2">Correo Electrónico</label>
                  <input
                    type="email"
                    value={formData.correo || ''}
                    onChange={(e) => setFormData({ ...formData, correo: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                    placeholder="doctor@clinicpass.com"
                  />
                </div>
                <div>
                  <label className="block text-gray-700 mb-2">Teléfono</label>
                  <input
                    type="text"
                    value={formData.telefono || ''}
                    onChange={(e) => setFormData({ ...formData, telefono: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                    placeholder="3794-123456"
                  />
                </div>
                <div>
                  <label className="block text-gray-700 mb-2">Especialidad</label>
                  <input
                    type="text"
                    value={formData.especialidad || ''}
                    onChange={(e) => setFormData({ ...formData, especialidad: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                    placeholder="Cardiología"
                  />
                </div>
                <div>
                  <label className="block text-gray-700 mb-2">Estado</label>
                  <select
                    value={formData.activo ? 'true' : 'false'}
                    onChange={(e) => setFormData({ ...formData, activo: e.target.value === 'true' })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                  >
                    <option value="true">Activo</option>
                    <option value="false">Inactivo</option>
                  </select>
                </div>
              </div>
            </div>
            <div className="p-6 border-t border-gray-200 flex justify-end gap-3">
              <button
                onClick={handleCloseModal}
                className="px-4 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 transition"
              >
                Cancelar
              </button>
              <button
                onClick={handleSave}
                className="px-4 py-2 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 transition"
              >
                Guardar
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};
