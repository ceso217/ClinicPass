'use client'
import React, { useCallback, useEffect, useState } from 'react';
import { Search, Plus, Edit, UserX, UserCheck, Filter, X } from 'lucide-react';
import { getProfesionales, registerProfesional, updateProfesional} from '../hooks/profesionalesApi'; 
import {  type Profesional } from '../types/profesional';
import { RegisterPayload } from '../types/registerPayload';

type RoleType = 'Admin' | 'Profesional';

// Este tipo combina Profesional (para edición) con campos adicionales (para registro)
type FormDataType = Partial<Profesional> & {
    password?: string;
    repeatPassword?: string;
    // Si usas 'correo' en el formulario pero la API usa 'email', asegúrate de que el mapeo se haga
    email?: string; 
};

export const Profesionales: React.FC = () => {
  const [formData, setFormData] = useState<FormDataType>({});
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showPasswordFields, setShowPasswordFields] = useState(false);

  const [profesionales, setProfesionales] = useState<Profesional[]>([]);
  const [filteredProfesionales, setFilteredProfesionales] = useState<Profesional[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [filterEspecialidad, setFilterEspecialidad] = useState('');
  const [filterActivo, setFilterActivo] = useState<string>('');
  const [showModal, setShowModal] = useState(false);
  const [selectedProfesional, setSelectedProfesional] = useState<Profesional | null>(null);

  // Función para cargar los datos (usada en useEffect)
    const fetchProfesionales = useCallback(async () => {
        setIsLoading(true);
        try {
            const data: Profesional[] = await getProfesionales(); 
            setProfesionales(data);
            setFilteredProfesionales(data);
        } catch (err) {
            console.error("Error al cargar profesionales", err);
            setError("No se pudieron cargar los profesionales. Intente de nuevo.");
        } finally {
            setIsLoading(false);
        }
    }, []);

    useEffect(() => {
        fetchProfesionales();
    }, [fetchProfesionales]);

  // Filtrar profesionales
  const handleSearch = (term: string, especialidad: string, activo: string, listToFilter?: Profesional[]) => {

    // Si se pasa una lista (listToFilter), úsala. Si no, usa el estado 'profesionales' actual.
    let filtered = listToFilter ?? profesionales; // Utiliza Nullish Coalescing (??) para la lista

    if (term) {
      filtered = filtered.filter(
        (p) =>
          p.nombreCompleto?.toLowerCase().includes(term.toLowerCase()) ||
          p.dni?.includes(term) ||
          p.email?.toLowerCase().includes(term.toLowerCase()) ||
          p.phoneNumber?.includes(term)
      );
    }

    if (especialidad) {
      filtered = filtered.filter((p) =>
        p.especialidad?.toLowerCase().includes(especialidad.toLowerCase())
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
      setShowPasswordFields(false);
    } else {
      setSelectedProfesional(null);
      setFormData({ activo: true, rol: "Profesional" });
      setShowPasswordFields(true);
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedProfesional(null);
    setFormData({});
  };

  const handleSave = async () => {
    // 1. VALIDACIÓN de los campos requeridos)
    try{
      if (selectedProfesional && selectedProfesional.id) {
      // --- MODO EDICIÓN ---
            
            // 2. Crear un payload filtrado solo con los campos editables
            const updatePayload = {
                nombreCompleto: formData.nombreCompleto,
                dni: formData.dni,
                email: formData.email,
                phoneNumber: formData.phoneNumber,
                especialidad: formData.especialidad,
                activo: formData.activo,
                rol: formData.rol,
                // No incluimos password/repeatPassword a menos que el usuario lo haya activado
            };
            
            // 3. Llamada a la API de actualización
            // Nota: Aquí asumimos que updateProfesional no devuelve nada (Promise<void>), solo confirma el éxito.
            await updateProfesional(selectedProfesional.id, updatePayload);
            
            
            // 4. Actualizar el estado local (usamos el payload para la actualización)
            const updatedList = profesionales.map((p) =>
                p.id === selectedProfesional!.id ? { ...p, ...updatePayload } as Profesional : p
            );
            setProfesionales(updatedList);
            // Reaplicar filtros
            handleSearch(searchTerm, filterEspecialidad, filterActivo, updatedList);
        } else {
            // --- MODO REGISTRO ---

                    // --- MODO REGISTRO ---

            // 5. Validación de contraseñas
            if (!formData.password || !formData.repeatPassword || formData.password !== formData.repeatPassword) {
                alert('Las contraseñas no coinciden o están vacías.');
                return;
            }

            // --- CORRECCIÓN DE NOMBRE Y APELLIDO ---
            
            // 1. Asegurar que sea string (evitar undefined)
            const nombreLimpio = formData.nombreCompleto?.trim() || "";
            
            // 2. Buscar el espacio
            const espacioIndex = nombreLimpio.indexOf(" ");

            // 3. Validar que exista el espacio
            if (espacioIndex === -1) {
                alert("Por favor, ingrese el Nombre y el Apellido separados por un espacio (Ej: 'Juan Perez').");
                return; 
            }

            // 4. Cortar el string usando el índice encontrado (Sin el '?')
            const namePayload = nombreLimpio.substring(0, espacioIndex);
            const lastNamePayload = nombreLimpio.substring(espacioIndex + 1);

            // 6. Mapear formData a RegisterPayload
            const payload: RegisterPayload = {
                email: formData.email || '',
                password: formData.password,
                repeatPassword: formData.repeatPassword,
                dni: formData.dni || '',
                
                // --- AQUÍ ESTABA EL OTRO ERROR: USAR LAS VARIABLES NUEVAS ---
                name: namePayload,          // Usar la variable calculada arriba
                lastName: lastNamePayload,  // Usar la variable calculada arriba
                
                phoneNumber: formData.phoneNumber || '',
                especialidad: formData.especialidad || '',
                activo: formData.activo ?? true,
                rol: formData.rol || 'Profesional',
            };
            // 7. Llamada a la API de registro
            const response = await registerProfesional(payload);
            console.log(response);

            // 8. Reconstruir el objeto Profesional a partir de la respuesta para el estado local
            const nuevoProfesional: Profesional = {
                id: response.id, // ID devuelto por el servidor
                nombreCompleto: `${response.nombreCompleto}`,
                dni: response.dni ?? '',
                email: response.email ?? '',
                phoneNumber: response.phoneNumber ?? '',
                especialidad: payload.especialidad,
                activo: payload.activo,
                rol: payload.rol as RoleType,
            };

            // 9. Actualizar estado local
            const updatedList = [...profesionales, nuevoProfesional];
            setProfesionales(updatedList);
            // Reaplicar filtros
            handleSearch(searchTerm, filterEspecialidad, filterActivo, updatedList);
        }
        alert("Cambios Guardados Correctamente");
        handleCloseModal();
    } catch(error){
    
    console.error("Error al guardar profesional:", error);
    alert(`Ocurrió un error al guardar los datos. Detalles: ${error instanceof Error ? error.message : 'Error desconocido'}`);
  }
  };


const handleToggleActivo = async (profesional: Profesional) => {
    // Asegúrate de tener implementada la función toggleProfesionalStatus en pacienteApi.ts
    // usando la función updateProfesional o un endpoint dedicado.

    try {
        const newStatus = !profesional.activo;

        // 1. Llamada a la API de actualización
        // ASUMIMOS que tienes una función updateProfesional que recibe el ID y el nuevo estado
        const updatePayload = { activo: newStatus };
        await updateProfesional(profesional.id as string, updatePayload); 

        // 2. Actualiza el estado local
        const updatedList = profesionales.map((p) =>
            p.id === profesional.id ? { ...p, activo: newStatus } : p
        );
        setProfesionales(updatedList);
        // Reaplicar filtros para reflejar el cambio en la tabla
        handleSearch(searchTerm, filterEspecialidad, filterActivo, updatedList); 
        
    } catch (error) {
        console.error("Error al cambiar estado:", error);
        alert("Error al cambiar el estado del profesional.");
    }
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
                                        <td className="px-6 py-4 text-gray-700">{profesional.email}</td>
                                        <td className="px-6 py-4 text-gray-700">{profesional.phoneNumber}</td>
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
                                <label className="block text-gray-700 mb-2">Nombre Completo*</label>
                                <input
                                    type="text"
                                    value={formData.nombreCompleto || ''}
                                    onChange={(e) => setFormData({ ...formData, nombreCompleto: e.target.value })}
                                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                                    placeholder="Dr. Juan Pérez"
                                />
                            </div>
                            <div>
                                <label className="block text-gray-700 mb-2">DNI*</label>
                                <input
                                    type="text"
                                    value={formData.dni || ''}
                                    onChange={(e) => setFormData({ ...formData, dni: e.target.value })}
                                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                                    placeholder="12345678"
                                />
                            </div>
                            <div>
                                <label className="block text-gray-700 mb-2">Correo Electrónico*</label>
                                <input
                                    type="email"
                                    value={formData.email || ''}
                                    onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                                    placeholder="doctor@clinicpass.com"
                                />
                            </div>
                            <div>
                                <label className="block text-gray-700 mb-2">Teléfono</label>
                                <input
                                    type="text"
                                    value={formData.phoneNumber || ''}
                                    onChange={(e) => setFormData({ ...formData, phoneNumber: e.target.value })}
                                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                                    placeholder="3794-123456"
                                />
                            </div>
                            <div>
                                <label className="block text-gray-700 mb-2">Especialidad*</label>
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
                        <div className='grid grid-cols-1 md:grid-cols-2 gap-4'>
                            <div>
                                <label className="block text-gray-700 mb-2">Rol</label>
                                <select
                                    value={formData.rol || 'Profesional'}
                                    onChange={(e) => setFormData({ ...formData, rol: e.target.value as RoleType})}
                                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                                >
                                    <option value="Profesional">Profesional</option>
                                    <option value="Admin">Administrador</option>
                                </select>
                            </div>

                            {/* Opcional: Para edición, mostrar un botón que active el input de contraseña
                            */}
                            {selectedProfesional && !showPasswordFields && (
                                <div className="md:col-span-2 mt-4">
                                    <button
                                        onClick={() => {
                                            setShowPasswordFields(true);
                                        }}
                                        type="button"
                                        className="w-full px-4 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-100 transition"
                                    >
                                        Cambiar Contraseña
                                    </button>
                                </div>
                            )}

                            {/* Contraseñas: Abierto en Crear O si showPasswordFields está activo */}
                            {(!selectedProfesional || showPasswordFields) && (
                                <>
                                    {selectedProfesional && ( // Mensaje opcional para el modo Edición
                                        <div className="md:col-span-2 text-sm text-gray-600 mt-2">
                                            Deja los campos vacíos si no deseas cambiar la contraseña.
                                        </div>
                                    )}
                                    
                                    <div>
                                        <label className="block text-gray-700 mb-2">Nueva Contraseña*</label>
                                        <input
                                            type="password"
                                            value={formData?.password || ''}
                                            onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                                            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                                            placeholder="Contraseña"
                                        />
                                    </div>
                                    <div>
                                        <label className="block text-gray-700 mb-2">Repetir contraseña*</label>
                                        <input
                                            type="password"
                                            value={formData?.repeatPassword || ''}
                                            onChange={(e) => setFormData({ ...formData, repeatPassword: e.target.value })}
                                            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                                            placeholder="Repite contraseña"
                                        />
                                    </div>

                                    {selectedProfesional && showPasswordFields && (
                                        <div className="md:col-span-2">
                                            <button
                                                onClick={() => {
                                                    setShowPasswordFields(false);
                                                    setFormData({ ...formData, password: '', repeatPassword: '' });
                                                }}
                                                type="button"
                                                className="mt-2 text-sm text-red-500 hover:text-red-700 transition"
                                            >
                                                Cancelar cambio de contraseña
                                            </button>
                                        </div>
                                    )}
                                </>
                            )} 
                        </div>
                    </div>
                    {/* FOOTER DEL MODAL */}
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
);}