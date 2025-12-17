export interface RegisterPayload {
    email: string;
    password: string;
    repeatPassword: string;
    dni: string;
    name: string;
    lastName: string;
    phoneNumber: string;
    especialidad: string;
    activo: boolean;
    rol: 'Admin' | 'Profesional';
}