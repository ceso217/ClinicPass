'use client'
import React, { useCallback, useEffect, useState } from 'react';
import { Search, Plus, Edit, Eye, Filter, X, Trash2 } from 'lucide-react';
// IMPORTAMOS TODAS LAS FUNCIONES API NECESARIAS
import { getPacientes, createPaciente, updatePaciente } from '../hooks/pacientesApi'; 
import { createHistoriaClinica } from '../hooks/historialesApi';
import { Paciente, PacientePayload } from '../types/paciente'; // Asegúrate de tener PacientePayload en tu types/paciente
import toast from 'react-hot-toast';
import { toastConfirm } from '../utils/toastConfirm';

export const Pacientes: React.FC = () => {
  // Usamos un estado para el loading
  const [loading, setLoading] = useState(true);
  const [pacientes, setPacientes] = useState<Paciente[]>([]);
  const [filteredPacientes, setFilteredPacientes] = useState<Paciente[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [filterLocalidad, setFilterLocalidad] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [selectedPaciente, setSelectedPaciente] = useState<Paciente | null>(null);
  const [formData, setFormData] = useState<Partial<Paciente>>({});

  // FUNCIÓN PARA CARGAR PACIENTES DESDE LA API
  const fetchPacientes = useCallback(async () => {
    setLoading(true);
    try {
      const data = await getPacientes();
      setPacientes(data);
      setFilteredPacientes(data);
    } catch (error) {
      console.error('Fallo al cargar pacientes:', error);
    } finally {
      setLoading(false);
    }
  }, []);

  // useEffect para cargar datos al inicio
  useEffect(() => {
    fetchPacientes();
  }, [fetchPacientes]);
  
  // Filtrar pacientes (Lógica sin cambios)
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

  // Abrir modal para agregar/editar (Lógica sin cambios)
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





  // 3. FUNCIÓN DE GUARDADO CORREGIDA (ASÍNCRONA Y CON API)
const handleSave = async () => {
    // 1. Validaciones básicas antes de enviar (DNI y Nombre Completo son [Required] en C#)
    if (!formData.nombreCompleto || !formData.dni) {
      toast.error("El Nombre Completo y DNI son campos obligatorios.");
      return;
    }

   

    // Desestructuración: Excluimos propiedades solo del frontend/cálculo (id, edad, etc.)
    const { idPaciente, edad, ultimaConsulta, ...rawPayload } = formData; 

    // --- 2. Mapeo a PascalCase y Limpieza del Payload para la API de C# ---
    let payloadToSend: any = {
        // Mapeo de camelCase (frontend) a PascalCase (DTO de C#)
        NombreCompleto: rawPayload.nombreCompleto || '',
        Dni: rawPayload.dni || '',
        Localidad: rawPayload.localidad || '',
        Provincia: rawPayload.provincia || '',
        Calle: rawPayload.calle || '',
        Telefono: rawPayload.telefono || '',
    };
   

    // 3. Ajuste CRÍTICO para FechaNacimiento (DateTime en C#)
    if (rawPayload.fechaNacimiento) {
        let fecha = rawPayload.fechaNacimiento;
        
        // Si el input date solo dio YYYY-MM-DD, le añadimos la hora UTC
        if (!fecha.includes('T')) {
            fecha = fecha + 'T00:00:00Z';
        }
        payloadToSend.FechaNacimiento = fecha;
    } else {
        // Si el campo FechaNacimiento no está marcado como [Required] en el DTO, 
        // y el frontend lo envía vacío, enviamos null (o no lo enviamos si fuera una actualización parcial).
        // Lo enviamos explícitamente como null si el campo es DateTime? en C#. 
        // Si es DateTime no nullable, C# tomará DateTime.MinDate (0001-01-01).
        payloadToSend.FechaNacimiento = null; 
    }
    
    // 4. Ejecución de la API
    try {
        if (selectedPaciente) {
            // EDITAR (PUT)
            const idToUse = selectedPaciente.idPaciente; 

            if (!idToUse) {
                // Si selectedPaciente existe pero no tiene ID, algo falló en la carga.
                throw new Error("ID del paciente no disponible para la edición.");
            }
             // AGREGAR (POST)
            const confirmado = await toastConfirm(
              "¿Estás seguro de que deseas editar el paciente?"
            );

            if (!confirmado) {
              return null
            }
             setLoading(true);
            // Nota: Usamos payloadToSend directamente, ya que contiene solo las propiedades mapeadas.
            await updatePaciente(idToUse, payloadToSend);
            toast.success("El paciente fue actualizado correctamente");
            
        } else {
            // AGREGAR (POST)
            const confirmado = await toastConfirm(
              "¿Estás seguro de que deseas crear un nuevo paciente? Luego de la creación no se eliminará por motivos de seguridad."
            );

            if (!confirmado) 
            { return null}
            else{
              const nuevoPaciente = await createPaciente(payloadToSend);
              // Crear historia clínica automáticamente
              try {
                await createHistoriaClinica({
                  idPaciente: nuevoPaciente.idPaciente,
                  antecedentesFamiliares: '',
                  antecedentesPersonales: '',
                });
              } catch (e) {
                toast.error(
                  'Paciente creado, pero falló la creación de la historia clínica'
                );
            }
            toast.success("El paciente fue creado correctamente");
            }
        }

        // 5. Recarga y Cierre
        await fetchPacientes(); // Recargar la lista completa
        handleCloseModal();
        
    } catch (error) {
        console.error("Error al guardar el paciente:", error);
        toast.error("Ocurrió un error al guardar los datos del paciente. Verifique la consola para detalles del error 400.");
    } finally {
        setLoading(false);
    }
};

  const localidades = Array.from(new Set(pacientes.map((p) => p.localidad)));

    // Mostrar estado de carga
    if (loading) {
        return (
            <div className="flex justify-center items-center min-h-screen text-xl text-indigo-600">
                Cargando pacientes...
            </div>
        );
    }

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

  <div className="p-8">{/* Filtros */}
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
    </div>{/* <--- CIERRE DEL BLOQUE DE FILTROS */}

    {/* TABLA: INICIO DIRECTAMENTE DESPUÉS DEL CIERRE DE FILTROS */}
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
              <td colSpan={7} className="px-6 py-4 text-center text-gray-500">
              No se encontraron pacientes
              </td>
            </tr>
          ) : (
          filteredPacientes.map((paciente) => (
            <tr key={paciente.idPaciente} className="hover:bg-gray-50 transition"> 
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
                  onClick={() => console.log('Ver historial', paciente.idPaciente)}
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

                {/* BOTON ELIMINAR, SOLO PARA DEBUG */}
                {/* <button 
                  onClick={() => handleDelete(paciente.idPaciente, paciente.nombreCompleto)}
                  className="p-2 text-red-600 hover:bg-red-50 rounded-lg transition"
                  title="Eliminar paciente"
                >
                <Trash2 className="w-4 h-4" />
                </button> */}
                
              </div>
              </td>
            </tr>
          ))
          )}
          </tbody>
        </table>
      </div>
    </div>
  </div> {/* Modal */}
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
            value={calcularEdad(formData.fechaNacimiento) || '' }
            onChange={(e) => setFormData({ ...formData, edad: parseInt(e.target.value) || undefined })}
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

export function calcularEdad(fechaNacimientoString?: string | null): number {
  if (!fechaNacimientoString) {
    return 0; // Devolver 0 o un valor por defecto si la fecha es inválida/vacía
  }

  // Convertir la cadena de fecha a un objeto Date de JavaScript.
  // Usamos Date.parse() para manejar diferentes formatos ISO que podría enviar el backend.
  const fechaNacimiento = new Date(fechaNacimientoString);
  const hoy = new Date();

  // 1. Obtener la diferencia en años
  let edad = hoy.getFullYear() - fechaNacimiento.getFullYear();

  // 2. Ajustar la edad si aún no ha cumplido años este año.
  // Comparamos el mes y día actual con el mes y día de nacimiento.
  const mesActual = hoy.getMonth();
  const mesNacimiento = fechaNacimiento.getMonth();

  if (mesActual < mesNacimiento) {
    // Si el mes actual es menor que el de nacimiento, aún no cumplió.
    edad--;
  } else if (mesActual === mesNacimiento) {
    // Si estamos en el mismo mes, verificamos el día.
    const diaActual = hoy.getDate();
    const diaNacimiento = fechaNacimiento.getDate();
    
    if (diaActual < diaNacimiento) {
      // Si el día actual es menor que el de nacimiento, aún no cumplió.
      edad--;
    }
  }

  // Nos aseguramos de que la edad no sea negativa (ej. si la fecha de nacimiento es futura)
  return Math.max(0, edad);
}

//FUNCION PARA ELIMINAR PACIENTE SOLO PARA DEBUG
// const handleDelete = async (idPaciente: number, nombre: string) => {
//     if (!window.confirm(`¿Estás seguro de que deseas eliminar al paciente ${nombre}? Esta acción es irreversible.`)) {
//         return;
//     }
    
//     setLoading(true);

//     try {
//         await deletePaciente(idPaciente);

//         // Recargar la lista después de la eliminación
//         await fetchPacientes();
        
//         console.log(`Paciente ${nombre} eliminado con éxito.`);
//         alert(`Paciente ${nombre} eliminado con éxito.`);
        
//     } catch (error) {
//         console.error(`Error al eliminar el paciente ${nombre}:`, error);
//         alert(`Error al intentar eliminar al paciente ${nombre}. Verifique la consola.`);
//     } finally {
//         setLoading(false);
//     }
// };