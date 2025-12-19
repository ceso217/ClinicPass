import { apiFetch } from "./apiFetch";
import { Turno } from "../types/turno";
import { FiltroFecha } from "../types/filtroFecha";
import { FiltroFechaDTO } from "../types/filtroFechaDTO";

const BASE_URL = "/api";

export interface TurnosEstadosDTO {
  estado: string;
  cantidadTurnos: number;
}

/**
 * Obtiene la lista de turnos y los mapea a la interfaz local
 */
export async function getTurnos(): Promise<Turno[]> {
  try {
    const data = await apiFetch(`${BASE_URL}/Turnos`);

    return data.map((t: any) => ({
      id: t.idTurno,
      pacienteId: t.idPaciente,
      pacienteNombre: t.nombrePaciente,
      profesionalId: t.profesionalId,
      profesionalNombre: t.nombreProfesional,
      // Separamos la fecha y hora si vienen juntas en el string ISO
      fecha: t.fecha.split("T")[0],
      hora: t.fecha.split("T")[1].substring(0, 5),
      estado: t.estado,
      // Determinamos si tiene ficha si el idFichaSeguimiento no es nulo o 0
      fichaCreada: !!t.idFichaSeguimiento && t.idFichaSeguimiento !== 0,
    })) as Turno[];
  } catch (error) {
    console.error("Error al obtener turnos:", error);
    throw error;
  }
}

export async function getTurnosPeriodo(
  filtro: FiltroFechaDTO
): Promise<number> {
  try {
    const data = await apiFetch(`${BASE_URL}/Reportes/turnos/total`, {
      method: "POST",
      body: JSON.stringify(filtro),
    });
    return data;
  } catch (error) {
    console.error("Error al obtener el n�mero de turnos:", error);
    throw error;
  }
}

// export async function getTurnosProgramadosCompletados(
//   filtro: FiltroFecha
// ): Promise<TurnosEstadosDTO[]> {
//   try {
//     const data = await apiFetch(
//       `${BASE_URL}/Reportes/turnos/programados-completados`,
//       {
//         method: "POST",
//         body: JSON.stringify({ filtro }),
//       }
//     );
//     return data as TurnosEstadosDTO[];
//   } catch (error) {
//     console.error("Error al obtener turnos de hoy:", error);
//     throw error;
//   }
// }

export async function updateProfesionalActivo(id: string): Promise<void> {
  await apiFetch(`${BASE_URL}/Profesionals/deactivate-activate/${id}`, {
    method: "PATCH",
  });
}

/**
 * Crea un nuevo turno
 */
export async function createTurno(turnoData: Partial<Turno>): Promise<any> {
  // El backend suele esperar PascalCase: Fecha, Estado, IdPaciente, ProfesionalId
  const payload = {
    Fecha: `${turnoData.fecha}T${turnoData.hora}:00Z`,
    Estado: turnoData.estado || "Pendiente",
    PacienteId: turnoData.pacienteId,
    ProfesionalId: turnoData.profesionalId,
  };

  return await apiFetch(`${BASE_URL}/turnos`, {
    method: "POST",
    body: JSON.stringify(payload),
  });
}

/**
 * Actualiza el estado de un turno (ej: para cancelar o completar)
 */
export async function updateEstadoTurno(
  id: number,
  nuevoEstado: string
): Promise<void> {
  await apiFetch(`${BASE_URL}/turnos/${id}`, {
    method: "PUT",
    body: JSON.stringify({ Estado: nuevoEstado }),
  });
}

/**
 * Elimina un turno
 */
export async function deleteTurno(id: number): Promise<void> {
  await apiFetch(`${BASE_URL}/turnos/${id}`, {
    method: "DELETE",
  });
}
