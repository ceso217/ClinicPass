// Usuarios de prueba para testing sin backend

export interface MockUser {
  id: number;
  nombreCompleto: string;
  dni: string;
  correo: string;
  password: string; // Solo para mock, nunca en producción
  especialidad?: string;
  rol: number; // 1 = Admin, 2 = Profesional
  telefono?: string;
}

export const mockUsers: MockUser[] = [
  {
    id: 1,
    nombreCompleto: 'Carlos Administrador',
    dni: '12345678',
    correo: 'admin@clinicpass.com',
    password: 'admin123',
    rol: 1,
    telefono: '3794-123456',
  },
  {
    id: 2,
    nombreCompleto: 'Dra. María González',
    dni: '23456789',
    correo: 'profesional@clinicpass.com',
    password: 'prof123',
    especialidad: 'Cardiología',
    rol: 2,
    telefono: '3794-654321',
  },
  {
    id: 3,
    nombreCompleto: 'Dr. Juan Pérez',
    dni: '34567890',
    correo: 'juan.perez@clinicpass.com',
    password: 'juan123',
    especialidad: 'Pediatría',
    rol: 2,
    telefono: '3794-111222',
  },
  {
    id: 4,
    nombreCompleto: 'Laura Admin',
    dni: '45678901',
    correo: 'laura@clinicpass.com',
    password: 'laura123',
    rol: 1,
    telefono: '3794-333444',
  },
];

// Simular generación de JWT (no es un JWT real, solo para testing)
export const generateMockToken = (userId: number): string => {
  const mockPayload = {
    userId,
    timestamp: Date.now(),
  };
  return btoa(JSON.stringify(mockPayload));
};

// Función para autenticar con datos mock
export const mockLogin = (correo: string, password: string): { token: string; user: Omit<MockUser, 'password'> } | null => {
  const user = mockUsers.find(
    (u) => u.correo === correo && u.password === password
  );

  if (!user) {
    return null;
  }

  // Remover password antes de devolver
  const { password: _, ...userWithoutPassword } = user;

  return {
    token: generateMockToken(user.id),
    user: userWithoutPassword,
  };
};
