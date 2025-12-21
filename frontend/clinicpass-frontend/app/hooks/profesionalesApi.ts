// api/pacienteApi.ts

// Asegúrate de importar apiFetch y la interfaz Paciente
import { apiFetch } from "./apiFetch"; // Asume que apiFetch está en un archivo llamado apiFetch.ts
import { RegisterPayload } from "../types/registerPayload"; // Ajusta la ruta
import { Profesional } from "../types/profesional";
import { FiltroFechaDTO } from "../types/filtroFechaDTO";

const BASE_URL = "/api";

export interface ActividadProfesionalDTO {
  idProfesional: number;
  nombreProfesional: string;
  especialidad: string;
  cantidadTurnos: number;
  cantidadFicha: number;
}

/**
 * Obtiene la lista completa de pacientes desde la API.
 * @returns {Promise<Profesional[]>} Un array de objetos Paciente.
 */
export async function getProfesionales(): Promise<any[]> {
  try {
    // CORRECCIÓN: Usa la ruta API correcta, típicamente el nombre del controlador en plural
    const data = await apiFetch(`${BASE_URL}/profesionals`);

    return data.map((p: any) => ({
      ...p,
      // Si el backend devuelve 'Id' en PascalCase y esperas 'id' en camelCase:
      id: p.id || p.Id,
    })) as Profesional[];
  } catch (error) {
    console.error("Error al obtener profesionales:", error);
    throw error;
  }
}

export async function getTotalProfesionales(): Promise<number> {
  try {
    const data = await apiFetch(`${BASE_URL}/Reportes/profesionales/total`);
    return data;
  } catch (error) {
    console.error("Error al obtener número de profesionales activos:", error);
    throw error;
  }
}

export async function getProfesionalesActivos(): Promise<number> {
  try {
    const data = await apiFetch(
      `${BASE_URL}/Reportes/profesionales/total-activos`
    );
    return data;
  } catch (error) {
    console.error("Error al obtener número de profesionales activos:", error);
    throw error;
  }
}

export async function getProfesionalesPorEspecialidad(): Promise<
  { especialidad: string; total: number }[]
> {
  try {
    const data = await apiFetch(
      `${BASE_URL}/Reportes/profesionales/total-por-especialidad`,
      {
        method: "GET",
      }
    );
    return data as { especialidad: string; total: number }[];
  } catch (error) {
    console.error("Error al obtener turnos de hoy:", error);
    throw error;
  }
}

export async function getProfesionalesConTurnosYFichas(
  filtro: FiltroFechaDTO
): Promise<ActividadProfesionalDTO[]> {
  try {
    const data = await apiFetch(
      `${BASE_URL}/reportes/profesionales/actividad`,
      {
        method: "POST",
        body: JSON.stringify(filtro),
      }
    );
    return data as ActividadProfesionalDTO[];
  } catch (error) {
    console.error("Error al obtener actividad de profesionales:", error);
    throw error;
  }
}

/**
 * Crea un nuevo paciente.
 * @param {RegisterPayload} userData - Los datos del nuevo paciente.
 * @returns {Promise<Profesional>} El paciente creado, incluyendo su nuevo ID.
 */
export async function registerProfesional(
  payload: RegisterPayload
): Promise<any> {
  return await apiFetch(`${BASE_URL}/Auth/register`, {
    method: "POST",
    body: JSON.stringify(payload),
  });
}

/**
 * Actualiza los datos de un profesional existente.
 * Llama a PUT /api/Profesionals/{id}
 * @param {string} id - El ID del profesional a actualizar.
 * @param {any} profesionalData - Los datos parciales a actualizar (en camelCase o PascalCase).
 * @returns {Promise<void>}
 */
export async function updateProfesional(
  id: string,
  profesionalData: any
): Promise<void> {
  await apiFetch(`${BASE_URL}/Profesionals/${id}`, {
    method: "PUT",
    body: JSON.stringify(profesionalData),
  });
}

export async function updateProfesionalActivo(id: string): Promise<void> {
  await apiFetch(`${BASE_URL}/Profesionals/deactivate-activate/${id}`, {
    method: "PATCH",
  });
}

/**
 * Cambia el estado (activo/inactivo) de un profesional (Soft Delete).
 * Llama a PUT /api/Profesionals/{id}
 * @param {string} id - El ID del profesional.
 * @param {boolean} activo - El nuevo estado (false para "eliminar/desactivar").
 * @returns {Promise<void>}
 */
export async function toggleProfesionalActivo(
  id: string,
  activo: boolean
): Promise<void> {
  // Nota: El DTO de C# probablemente espera PascalCase para 'Activo'
  const updatePayload = { Activo: activo };

  await updateProfesional(id, updatePayload);
}

// --- FUNCIÓN DE ELIMINACIÓN (DELETE) ---

/** * Elimina un profesional por su ID.
 * Llama a DELETE /api/Profesionals/{id}
 * @param {string} id - El ID del paciente (IdPaciente) a eliminar.
 * @returns {Promise<void>}
 */
export async function deleteProfesional(id: string): Promise<void> {
  await apiFetch(`${BASE_URL}/${id}`, {
    method: "DELETE",
  });
}

/**
 * Obtiene un profesional específico por su ID.
 * Llama a GET /api/Profesionals/id/{id}
 * @param {string} id - El ID del profesional.
 * @returns {Promise<Profesional>} El objeto Profesional.
 */
export async function getProfesionalById(id: string): Promise<Profesional> {
  const data = await apiFetch(`${BASE_URL}/id/${id}`);

  return {
    ...data,
    id: data.id || data.Id,
  } as Profesional;
}
// Puedes añadir más funciones como deletePaciente, getPacienteById, etc.
