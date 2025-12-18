export interface HistorialTratamiento {
  idTratamiento: number;
  motivo: string;
  fechaInicio: string;
  fechaFin?: string;
  activo: boolean;
  tratamiento: {
    id: number;
    nombre: string;
  };
}
