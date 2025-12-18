export interface Turno {
    id: number;
    pacienteId: number;
    pacienteNombre: string;
    profesionalId: number;
    profesionalNombre: string;
    fecha: string;
    hora: string;
    estado: 'Pendiente' | 'Confirmado' | 'Completado' | 'Cancelado';
    fichaCreada: boolean;
}

