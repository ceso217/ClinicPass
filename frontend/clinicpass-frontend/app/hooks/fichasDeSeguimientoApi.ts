// api/pacienteApi.ts

// Asegúrate de importar apiFetch y la interfaz Paciente
import { apiFetch } from "./apiFetch"; // Asume que apiFetch está en un archivo llamado apiFetch.ts
import { FichaDeSeguimiento } from "../types/fichaDeSeguimiento"; // Ajusta la ruta

const BASE_URL = "/api"; // Basado en el endpoint /api/Paciente que mostraste

export async function getNumeroFichas(): Promise<number> {
  try {
    const data = await apiFetch(`${BASE_URL}/reportes/fichas/total`);
    return data;
  } catch (error) {
    console.error("Error al obtener número de fichas:", error);
    throw error;
  }
}
