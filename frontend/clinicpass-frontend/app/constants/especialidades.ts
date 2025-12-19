export interface Especialidad {
  value: string;
  label: string;
}

export const especialidadesClinica: Especialidad[] = [
  // Rehabilitación / Movimiento
  { value: 'fisioterapia', label: 'Fisioterapia' },
  { value: 'kinesiologia', label: 'Kinesiología' },
  { value: 'terapia_ocupacional', label: 'Terapia Ocupacional' },
  { value: 'rehabilitacion_deportiva', label: 'Rehabilitación Deportiva' },

  // Salud Mental
  { value: 'psicologia', label: 'Psicología' },
  { value: 'psiquiatria', label: 'Psiquiatría' },
  { value: 'psicopedagogia', label: 'Psicopedagogía' },

  // Clínicas Médicas
  { value: 'medicina_general', label: 'Medicina General' },
  { value: 'clinica_medica', label: 'Clínica Médica' },
  { value: 'pediatria', label: 'Pediatría' },
  { value: 'geriatria', label: 'Geriatría' },

  // Diagnóstico / Apoyo
  { value: 'fonoaudiologia', label: 'Fonoaudiología' },
  { value: 'nutricion', label: 'Nutrición' },
  { value: 'enfermeria', label: 'Enfermería' },

  // Otros frecuentes en clínicas privadas
  { value: 'odontologia', label: 'Odontología' },
  { value: 'trabajo_social', label: 'Trabajo Social' },
];
