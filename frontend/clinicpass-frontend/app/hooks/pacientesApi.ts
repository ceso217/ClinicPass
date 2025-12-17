// api/pacienteApi.ts

// Asegúrate de importar apiFetch y la interfaz Paciente
import { apiFetch } from './apiFetch'; // Asume que apiFetch está en un archivo llamado apiFetch.ts
import { Paciente, PacientePayload } from '../types/paciente'; // Ajusta la ruta

const BASE_URL = '/api/Paciente'; // Basado en el endpoint /api/Paciente que mostraste

/**
 * Obtiene la lista completa de pacientes desde la API.
 * @returns {Promise<Paciente[]>} Un array de objetos Paciente.
 */
export async function getPacientes(): Promise<Paciente[]> {
    try {
        const data = await apiFetch(BASE_URL);
        
        // **NOTA DE TRANSFORMACIÓN:**
        // La API devuelve IdPaciente. El frontend usa 'id'. 
        // Mapeamos IdPaciente a id (minúscula) y calculamos la edad si es necesario.
        return data.map((p: any) => ({
        ...p,
        id: p.idPaciente, // Creamos la propiedad 'id' requerida por tu componente
        // Si necesitas calcular la edad aquí
        // edad: calcularEdad(p.fechaNacimiento), 
        // Si la API devuelve 'IdPaciente' en PascalCase y el frontend espera 'id' en camelCase.
        })) as Paciente[];
    } catch (error) {
        console.error("Error al obtener pacientes:", error);
        throw error;
    }
}

/**
 * Crea un nuevo paciente.
 * @param {PacientePayload} pacienteData - Los datos del nuevo paciente.
 * @returns {Promise<Paciente>} El paciente creado, incluyendo su nuevo ID.
 */
export async function createPaciente(pacienteData: PacientePayload): Promise<Paciente> {
    const data = await apiFetch(BASE_URL, {
        method: 'POST',
        body: JSON.stringify(pacienteData),

    });
    
    // Devolvemos el paciente creado con la misma transformación (si aplica)
    return { 
        ...data, 
        id: data.idPaciente 
    } as Paciente;
}

/**
 * Actualiza un paciente existente.
 * @param {number} id - El ID del paciente a actualizar (IdPaciente).
 * @param {PacientePayload} pacienteData - Los datos a actualizar.
 * @returns {Promise<void>}
 */
export async function updatePaciente(id: number, pacienteData: Partial<PacientePayload>): Promise<void> {
    await apiFetch(`${BASE_URL}/${id}`, {
        method: 'PUT',
        body: JSON.stringify(pacienteData),
    });   
  // No devuelve contenido, solo verifica que la respuesta sea OK
}
/** 
* @param {number} id - El ID del paciente (IdPaciente) a eliminar.
 * @returns {Promise<void>}
 */
export async function deletePaciente(id: number): Promise<void> {
    await apiFetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
    });
    // La API devuelve 204 No Content si es exitoso, manejado por apiFetch.
}

// Puedes añadir más funciones como deletePaciente, getPacienteById, etc.