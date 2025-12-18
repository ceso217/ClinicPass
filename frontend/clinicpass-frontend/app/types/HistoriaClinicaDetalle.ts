// types/HistoriaClinicaDetalle.ts
export interface HistoriaClinicaDetalle {
  paciente: {
    idPaciente: number;
    nombreCompleto: string;
    dni: string;
    fechaNacimiento: string;
    localidad: string;
    provincia: string;
    calle: string | null;
    telefono: string;
  };
  historiaClinica: {
    idHistorialClinico: number;
    antecedentesFamiliares: string;
    antecedentesPersonales: string;
    activa: boolean;
  };
  tratamientos: {
    idTratamiento: number;
    nombre: string;
    descripcion: string;
    activo: boolean;
    fechaInicio: string;
    fechaFin: string | null;
  }[];
  fichas: {
    idFichaSeguimiento: number;
    fechaCreacion: string;
    observaciones: string;
    tratamientoId: number | null;
    idUsuario: number;
    nombreProfesional: string;
  }[];
}
