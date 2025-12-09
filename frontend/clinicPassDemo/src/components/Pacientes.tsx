import React, { useState } from 'react';
import { Search, Plus, Edit, Eye, Filter, X } from 'lucide-react';
import { mockPacientes, type Paciente } from '../data/mockData';

export const Pacientes: React.FC = () => {
  const [pacientes, setPacientes] = useState<Paciente[]>(mockPacientes);
  const [filteredPacientes, setFilteredPacientes] = useState<Paciente[]>(mockPacientes);
  const [searchTerm, setSearchTerm] = useState('');
  const [filterLocalidad, setFilterLocalidad] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [selectedPaciente, setSelectedPaciente] = useState<Paciente | null>(null);
  const [formData, setFormData] = useState<Partial<Paciente>>({});

  // Filtrar pacientes
  const handleSearch = (term: string, localidad: string) => {
    let filtered = pacientes;

    if (term) {
      filtered = filtered.filter(
        (p) =>
          p.nombreCompleto.toLowerCase().includes(term.toLowerCase()) ||
          p.dni.includes(term) ||
          p.telefono.includes(term)
      );
    }

    if (localidad) {
      filtered = filtered.filter((p) =>
        p.localidad.toLowerCase().includes(localidad.toLowerCase())
      );
    }

    setFilteredPacientes(filtered);
  };

  const handleSearchChange = (value: string) => {
    setSearchTerm(value);
    handleSearch(value, filterLocalidad);
  };

  const handleLocalidadChange = (value: string) => {
    setFilterLocalidad(value);
    handleSearch(searchTerm, value);
  };

  const handleClearFilters = () => {
    setSearchTerm('');
    setFilterLocalidad('');
    setFilteredPacientes(pacientes);
  };

  // Abrir modal para agregar/editar
  const handleOpenModal = (paciente?: Paciente) => {
    if (paciente) {
      setSelectedPaciente(paciente);
      setFormData(paciente);
    } else {
      setSelectedPaciente(null);
      setFormData({});
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedPaciente(null);
    setFormData({});
  };

  const handleSave = () => {
    if (selectedPaciente) {
      // Editar
      const updated = pacientes.map((p) =>
        p.id === selectedPaciente.id ? { ...p, ...formData } as Paciente : p
      );
      setPacientes(updated);
      setFilteredPacientes(updated);
    } else {
      // Agregar
      const newPaciente: Paciente = {
        id: pacientes.length + 1,
        nombreCompleto: formData.nombreCompleto || '',
        dni: formData.dni || '',
        fechaNacimiento: formData.fechaNacimiento || '',
        edad: formData.edad || 0,
        localidad: formData.localidad || '',
        provincia: formData.provincia || '',
        calle: formData.calle || '',
        telefono: formData.telefono || '',
      };
      const updated = [...pacientes, newPaciente];
      setPacientes(updated);
      setFilteredPacientes(updated);
    }
    handleCloseModal();
  };

  const localidades = Array.from(new Set(pacientes.map((p) => p.localidad)));

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="bg-white border-b border-gray-200 px-8 py-6">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-gray-900">Gestión de Pacientes</h1>
            <p className="text-gray-600 mt-1">
              {filteredPacientes.length} paciente{filteredPacientes.length !== 1 ? 's' : ''} encontrado{filteredPacientes.length !== 1 ? 's' : ''}
            </p>
          </div>
          <button
            onClick={() => handleOpenModal()}
            className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
          >
            <Plus className="w-5 h-5" />
            Agregar Paciente
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
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <div className="relative">
              <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
              <input
                type="text"
                placeholder="Buscar por nombre, DNI o teléfono..."
                value={searchTerm}
                onChange={(e) => handleSearchChange(e.target.value)}
                className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
              />
            </div>
            <select
              value={filterLocalidad}
              onChange={(e) => handleLocalidadChange(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
            >
              <option value="">Todas las localidades</option>
              {localidades.map((loc) => (
                <option key={loc} value={loc}>
                  {loc}
                </option>
              ))}
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
                  <th className="px-6 py-4 text-left text-gray-900">Edad</th>
                  <th className="px-6 py-4 text-left text-gray-900">Localidad</th>
                  <th className="px-6 py-4 text-left text-gray-900">Teléfono</th>
                  <th className="px-6 py-4 text-left text-gray-900">Última Consulta</th>
                  <th className="px-6 py-4 text-center text-gray-900">Acciones</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-200">
                {filteredPacientes.length === 0 ? (
                  <tr>
                    <td colSpan={7} className="px-6 py-8 text-center text-gray-500">
                      No se encontraron pacientes
                    </td>
                  </tr>
                ) : (
                  filteredPacientes.map((paciente) => (
                    <tr key={paciente.id} className="hover:bg-gray-50 transition">
                      <td className="px-6 py-4 text-gray-900">{paciente.nombreCompleto}</td>
                      <td className="px-6 py-4 text-gray-700">{paciente.dni}</td>
                      <td className="px-6 py-4 text-gray-700">{paciente.edad} años</td>
                      <td className="px-6 py-4 text-gray-700">{paciente.localidad}</td>
                      <td className="px-6 py-4 text-gray-700">{paciente.telefono}</td>
                      <td className="px-6 py-4 text-gray-700">
                        {paciente.ultimaConsulta
                          ? new Date(paciente.ultimaConsulta).toLocaleDateString('es-AR')
                          : 'Sin consultas'}
                      </td>
                      <td className="px-6 py-4">
                        <div className="flex items-center justify-center gap-2">
                          <button
                            onClick={() => console.log('Ver historial', paciente.id)}
                            className="p-2 text-indigo-600 hover:bg-indigo-50 rounded-lg transition"
                            title="Ver historial"
                          >
                            <Eye className="w-4 h-4" />
                          </button>
                          <button
                            onClick={() => handleOpenModal(paciente)}
                            className="p-2 text-blue-600 hover:bg-blue-50 rounded-lg transition"
                            title="Editar"
                          >
                            <Edit className="w-4 h-4" />
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
                {selectedPaciente ? 'Editar Paciente' : 'Agregar Paciente'}
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
                    placeholder="Juan Pérez"
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
                  <label className="block text-gray-700 mb-2">Fecha de Nacimiento</label>
                  <input
                    type="date"
                    value={formData.fechaNacimiento || ''}
                    onChange={(e) => setFormData({ ...formData, fechaNacimiento: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                  />
                </div>
                <div>
                  <label className="block text-gray-700 mb-2">Edad</label>
                  <input
                    type="number"
                    value={formData.edad || ''}
                    onChange={(e) => setFormData({ ...formData, edad: parseInt(e.target.value) })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                    placeholder="30"
                  />
                </div>
                <div>
                  <label className="block text-gray-700 mb-2">Localidad</label>
                  <input
                    type="text"
                    value={formData.localidad || ''}
                    onChange={(e) => setFormData({ ...formData, localidad: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                    placeholder="Resistencia"
                  />
                </div>
                <div>
                  <label className="block text-gray-700 mb-2">Provincia</label>
                  <input
                    type="text"
                    value={formData.provincia || ''}
                    onChange={(e) => setFormData({ ...formData, provincia: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                    placeholder="Chaco"
                  />
                </div>
                <div>
                  <label className="block text-gray-700 mb-2">Calle</label>
                  <input
                    type="text"
                    value={formData.calle || ''}
                    onChange={(e) => setFormData({ ...formData, calle: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                    placeholder="Av. Alberdi 1234"
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
