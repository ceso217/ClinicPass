export interface FichaDeSeguimiento {
  id: number;
  idUsuario: number;
  idHistoriaClinica: number;
  idTratamiento: number;
  fecha: string;
  observaciones?: string;
}
