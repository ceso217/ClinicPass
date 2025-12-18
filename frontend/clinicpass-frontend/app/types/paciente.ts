export interface Paciente {
  idPaciente: number;
  nombreCompleto: string;
  dni: string;
  fechaNacimiento?: string;
  localidad?: string;
  provincia?: string;
  calle?: string;
  telefono?: string;

  // Campos que existen SOLO en mocks / UI (opcionales)
  id?: number;
  edad?: number;
  ultimaConsulta?: string;
}
