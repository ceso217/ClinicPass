
export interface Paciente {
    idPaciente: number; 
    nombreCompleto: string; 
    dni: string; 
    fechaNacimiento: string; 
    localidad: string; 
    provincia: string; 
    calle: string; 
    telefono: string; 
    edad?: number; 
    ultimaConsulta?: string; 
}

// Interfaz para la data que se envía al servidor (sin el ID)
export type PacientePayload = Omit<Paciente, 'idPaciente' | 'edad' | 'ultimaConsulta'>;