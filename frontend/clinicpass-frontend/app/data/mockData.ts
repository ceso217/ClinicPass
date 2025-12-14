// Datos mock para testing de ClinicPass

export interface Paciente {
  id: number;
  nombreCompleto: string;
  dni: string;
  fechaNacimiento: string;
  edad: number;
  localidad: string;
  provincia: string;
  calle: string;
  telefono: string;
  ultimaConsulta?: string;
  tutorId?: number;
}

export interface Tutor {
  id: number;
  nombreCompleto: string;
  dni: string;
  telefono: string;
  relacion: string;
}

export interface Turno {
  id: number;
  pacienteId: number;
  pacienteNombre: string;
  profesionalId: number;
  profesionalNombre: string;
  fecha: string;
  hora: string;
  estado: 'Programado' | 'Confirmado' | 'Completado' | 'Cancelado';
  fichaCreada: boolean;
}

export interface Profesional {
  id: number;
  nombreCompleto: string;
  dni: string;
  correo: string;
  especialidad: string;
  telefono: string;
  rol: number;
  activo: boolean;
}

export interface Tratamiento {
  id: number;
  pacienteId: number;
  tipo: string;
  descripcion: string;
  estado: 'Programado' | 'Activo' | 'Finalizado' | 'Cancelado';
  fechaInicio: string;
  fechaFin?: string;
  profesionalId: number;
}

export interface FichaSeguimiento {
  id: number;
  tratamientoId: number;
  pacienteId: number;
  fecha: string;
  diagnostico: string;
  observaciones: string;
  profesionalId: number;
  profesionalNombre: string;
  antecedentes: string;
  proximaConsulta?: string;
}

export const mockPacientes: Paciente[] = [
  {
    id: 1,
    nombreCompleto: 'María González',
    dni: '12345678',
    fechaNacimiento: '1985-03-15',
    edad: 39,
    localidad: 'Resistencia',
    provincia: 'Chaco',
    calle: 'Av. Alberdi 1234',
    telefono: '3794-123456',
    ultimaConsulta: '2025-01-15',
  },
  {
    id: 2,
    nombreCompleto: 'Juan Pérez',
    dni: '23456789',
    fechaNacimiento: '1990-07-22',
    edad: 34,
    localidad: 'Resistencia',
    provincia: 'Chaco',
    calle: 'Sarmiento 567',
    telefono: '3794-234567',
    ultimaConsulta: '2025-02-10',
  },
  {
    id: 3,
    nombreCompleto: 'Ana Martínez',
    dni: '34567890',
    fechaNacimiento: '1978-11-30',
    edad: 46,
    localidad: 'Corrientes',
    provincia: 'Corrientes',
    calle: 'Junín 890',
    telefono: '3794-345678',
    ultimaConsulta: '2025-01-28',
  },
  {
    id: 4,
    nombreCompleto: 'Carlos Rodríguez',
    dni: '45678901',
    fechaNacimiento: '1995-05-10',
    edad: 29,
    localidad: 'Resistencia',
    provincia: 'Chaco',
    calle: 'Brown 234',
    telefono: '3794-456789',
    ultimaConsulta: '2025-02-05',
  },
  {
    id: 5,
    nombreCompleto: 'Lucía Fernández',
    dni: '56789012',
    fechaNacimiento: '2010-09-18',
    edad: 14,
    localidad: 'Barranqueras',
    provincia: 'Chaco',
    calle: 'Mitre 456',
    telefono: '3794-567890',
    ultimaConsulta: '2025-02-12',
    tutorId: 1,
  },
  {
    id: 6,
    nombreCompleto: 'Roberto Silva',
    dni: '67890123',
    fechaNacimiento: '1982-12-05',
    edad: 42,
    localidad: 'Resistencia',
    provincia: 'Chaco',
    calle: 'Belgrano 789',
    telefono: '3794-678901',
    ultimaConsulta: '2025-01-20',
  },
  {
    id: 7,
    nombreCompleto: 'Sofía López',
    dni: '78901234',
    fechaNacimiento: '1992-04-25',
    edad: 32,
    localidad: 'Corrientes',
    provincia: 'Corrientes',
    calle: 'San Martín 345',
    telefono: '3794-789012',
    ultimaConsulta: '2025-02-08',
  },
  {
    id: 8,
    nombreCompleto: 'Diego Ramírez',
    dni: '89012345',
    fechaNacimiento: '1988-08-14',
    edad: 36,
    localidad: 'Resistencia',
    provincia: 'Chaco',
    calle: 'Moreno 678',
    telefono: '3794-890123',
    ultimaConsulta: '2025-01-30',
  },
];

export const mockTutores: Tutor[] = [
  {
    id: 1,
    nombreCompleto: 'Patricia Fernández',
    dni: '20123456',
    telefono: '3794-567890',
    relacion: 'Madre',
  },
];

export const mockProfesionales: Profesional[] = [
  {
    id: 1,
    nombreCompleto: 'Dra. María González',
    dni: '23456789',
    correo: 'profesional@clinicpass.com',
    especialidad: 'Cardiología',
    telefono: '3794-654321',
    rol: 2,
    activo: true,
  },
  {
    id: 2,
    nombreCompleto: 'Dr. Juan Pérez',
    dni: '34567890',
    correo: 'juan.perez@clinicpass.com',
    especialidad: 'Pediatría',
    telefono: '3794-111222',
    rol: 2,
    activo: true,
  },
  {
    id: 3,
    nombreCompleto: 'Dra. Laura Sánchez',
    dni: '45678901',
    correo: 'laura.sanchez@clinicpass.com',
    especialidad: 'Traumatología',
    telefono: '3794-222333',
    rol: 2,
    activo: true,
  },
  {
    id: 4,
    nombreCompleto: 'Dr. Roberto García',
    dni: '56789012',
    correo: 'roberto.garcia@clinicpass.com',
    especialidad: 'Clínica Médica',
    telefono: '3794-333444',
    rol: 2,
    activo: false,
  },
  {
    id: 5,
    nombreCompleto: 'Dra. Carmen Díaz',
    dni: '67890123',
    correo: 'carmen.diaz@clinicpass.com',
    especialidad: 'Dermatología',
    telefono: '3794-444555',
    rol: 2,
    activo: true,
  },
];

export const mockTurnos: Turno[] = [
  {
    id: 1,
    pacienteId: 1,
    pacienteNombre: 'María González',
    profesionalId: 1,
    profesionalNombre: 'Dra. María González',
    fecha: '2025-12-08',
    hora: '09:00',
    estado: 'Completado',
    fichaCreada: true,
  },
  {
    id: 2,
    pacienteId: 2,
    pacienteNombre: 'Juan Pérez',
    profesionalId: 2,
    profesionalNombre: 'Dr. Juan Pérez',
    fecha: '2025-12-08',
    hora: '10:00',
    estado: 'Confirmado',
    fichaCreada: false,
  },
  {
    id: 3,
    pacienteId: 3,
    pacienteNombre: 'Ana Martínez',
    profesionalId: 1,
    profesionalNombre: 'Dra. María González',
    fecha: '2025-12-08',
    hora: '11:30',
    estado: 'Confirmado',
    fichaCreada: false,
  },
  {
    id: 4,
    pacienteId: 4,
    pacienteNombre: 'Carlos Rodríguez',
    profesionalId: 3,
    profesionalNombre: 'Dra. Laura Sánchez',
    fecha: '2025-12-08',
    hora: '14:00',
    estado: 'Programado',
    fichaCreada: false,
  },
  {
    id: 5,
    pacienteId: 5,
    pacienteNombre: 'Lucía Fernández',
    profesionalId: 2,
    profesionalNombre: 'Dr. Juan Pérez',
    fecha: '2025-12-08',
    hora: '15:30',
    estado: 'Programado',
    fichaCreada: false,
  },
  {
    id: 6,
    pacienteId: 6,
    pacienteNombre: 'Roberto Silva',
    profesionalId: 1,
    profesionalNombre: 'Dra. María González',
    fecha: '2025-12-09',
    hora: '09:00',
    estado: 'Confirmado',
    fichaCreada: false,
  },
  {
    id: 7,
    pacienteId: 7,
    pacienteNombre: 'Sofía López',
    profesionalId: 5,
    profesionalNombre: 'Dra. Carmen Díaz',
    fecha: '2025-12-09',
    hora: '10:30',
    estado: 'Confirmado',
    fichaCreada: false,
  },
  {
    id: 8,
    pacienteId: 8,
    pacienteNombre: 'Diego Ramírez',
    profesionalId: 3,
    profesionalNombre: 'Dra. Laura Sánchez',
    fecha: '2025-12-10',
    hora: '11:00',
    estado: 'Programado',
    fichaCreada: false,
  },
];

export const mockTratamientos: Tratamiento[] = [
  {
    id: 1,
    pacienteId: 1,
    tipo: 'Control Cardiológico',
    descripcion: 'Seguimiento post-operatorio',
    estado: 'Activo',
    fechaInicio: '2025-01-10',
    profesionalId: 1,
  },
  {
    id: 2,
    pacienteId: 2,
    tipo: 'Rehabilitación',
    descripcion: 'Rehabilitación post-fractura',
    estado: 'Activo',
    fechaInicio: '2025-02-01',
    profesionalId: 3,
  },
  {
    id: 3,
    pacienteId: 3,
    tipo: 'Tratamiento Dermatológico',
    descripcion: 'Tratamiento para psoriasis',
    estado: 'Activo',
    fechaInicio: '2025-01-15',
    profesionalId: 5,
  },
];

export const mockFichas: FichaSeguimiento[] = [
  {
    id: 1,
    tratamientoId: 1,
    pacienteId: 1,
    fecha: '2025-01-15',
    diagnostico: 'Evolución favorable',
    observaciones: 'Paciente sin síntomas. Control en 30 días.',
    profesionalId: 1,
    profesionalNombre: 'Dra. María González',
    antecedentes: 'Cirugía cardíaca hace 2 meses',
    proximaConsulta: '2025-02-15',
  },
  {
    id: 2,
    tratamientoId: 2,
    pacienteId: 2,
    fecha: '2025-02-10',
    diagnostico: 'Recuperación en progreso',
    observaciones: 'Movilidad mejorada. Continuar fisioterapia.',
    profesionalId: 3,
    profesionalNombre: 'Dra. Laura Sánchez',
    antecedentes: 'Fractura de tibia hace 1 mes',
    proximaConsulta: '2025-03-10',
  },
];
