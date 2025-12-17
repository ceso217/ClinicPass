import { apiFetch } from './apiFetch';

export interface CrearTurnoDTO {
  idPaciente: number;
  fechaHora: string; 
  idFichaSeguimiento?: number | null;
}

export async function crearTurno(data: CrearTurnoDTO) {
  return apiFetch('/api/Turnos', {
    method: 'POST',
    body: JSON.stringify(data),
  });
}
