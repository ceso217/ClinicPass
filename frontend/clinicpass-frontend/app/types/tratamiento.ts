export interface tratamiento {
    idPaciente: number; 
    nombreCompleto: string; 
    dni: string; 
    fechaNacimiento: string | null; 
    localidad: string; 
    provincia: string; 
    calle: string; 
    telefono: string; 
    edad?: number; 
    ultimaConsulta?: string; 
}
