export interface Profesional {
    id?: string;
    nombreCompleto?: string;
    dni?: string;
    email?: string;
    phoneNumber?: string;
    especialidad?: string;
    activo?: boolean;
    password?: string;
    repeatPassword?: string;
    rol?: 'Admin' | 'Profesional';
}

