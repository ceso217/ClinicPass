// api/historialesApi.ts
import { apiFetch } from './apiFetch';

const BASE_URL = '/api/HistoriaClinica';

export interface HistoriaClinicaPayload {
  idPaciente: number;
  antecedentesFamiliares?: string;
  antecedentesPersonales?: string;
}

export async function createHistoriaClinica(
  payload: HistoriaClinicaPayload
): Promise<void> {
  await apiFetch(BASE_URL, {
    method: 'POST',
    body: JSON.stringify(payload),
  });
}