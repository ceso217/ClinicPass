export interface HistoriaClinicaResumen {
  idHistorialClinico: number;
  activa: boolean;
  paciente: {
    idPaciente: number;
    nombreCompleto: string;
    dni: string;
    edad: number;
  };
}