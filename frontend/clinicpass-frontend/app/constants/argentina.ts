
export interface SelectOption {
  value: string;
  label: string;
}

export interface Provincia {
  value: string;
  label: string;
  localidades: SelectOption[];
}

/* =========================
   PROVINCIAS + LOCALIDADES
   ========================= */

export const provinciasArgentina: Provincia[] = [
  {
    value: "Buenos Aires",
    label: "Buenos Aires",
    localidades: [
      { value: "La Plata", label: "La Plata" },
      { value: "Mar del Plata", label: "Mar del Plata" },
      { value: "Bahía Blanca", label: "Bahía Blanca" },
      { value: "Quilmes", label: "Quilmes" },
      { value: "Avellaneda", label: "Avellaneda" },
      { value: "San Isidro", label: "San Isidro" },
    ],
  },
  {
    value: "CABA",
    label: "Ciudad Autónoma de Buenos Aires",
    localidades: [
      { value: "Palermo", label: "Palermo" },
      { value: "Recoleta", label: "Recoleta" },
      { value: "Belgrano", label: "Belgrano" },
      { value: "Caballito", label: "Caballito" },
      { value: "Microcentro", label: "Microcentro" },
    ],
  },
  {
    value: "Catamarca",
    label: "Catamarca",
    localidades: [
      { value: "San Fernando del Valle", label: "San Fernando del Valle de Catamarca" },
    ],
  },
  {
    value: "Chaco",
    label: "Chaco",
    localidades: [
      { value: "Resistencia", label: "Resistencia" },
      { value: "Presidencia Roque Sáenz Peña", label: "P. R. Sáenz Peña" },
    ],
  },
  {
    value: "Chubut",
    label: "Chubut",
    localidades: [
      { value: "Comodoro Rivadavia", label: "Comodoro Rivadavia" },
      { value: "Trelew", label: "Trelew" },
      { value: "Puerto Madryn", label: "Puerto Madryn" },
    ],
  },
  {
    value: "Córdoba",
    label: "Córdoba",
    localidades: [
      { value: "Córdoba Capital", label: "Córdoba Capital" },
      { value: "Villa Carlos Paz", label: "Villa Carlos Paz" },
      { value: "Río Cuarto", label: "Río Cuarto" },
    ],
  },
  {
    value: "Corrientes",
    label: "Corrientes",
    localidades: [
      { value: "Corrientes Capital", label: "Corrientes Capital" },
      { value: "Goya", label: "Goya" },
    ],
  },
  {
    value: "Entre Ríos",
    label: "Entre Ríos",
    localidades: [
      { value: "Paraná", label: "Paraná" },
      { value: "Concordia", label: "Concordia" },
    ],
  },
  {
    value: "Formosa",
    label: "Formosa",
    localidades: [
      { value: "Formosa Capital", label: "Formosa Capital" },
    ],
  },
  {
    value: "Jujuy",
    label: "Jujuy",
    localidades: [
      { value: "San Salvador de Jujuy", label: "San Salvador de Jujuy" },
      { value: "Palpalá", label: "Palpalá" },
    ],
  },
  {
    value: "La Pampa",
    label: "La Pampa",
    localidades: [
      { value: "Santa Rosa", label: "Santa Rosa" },
      { value: "General Pico", label: "General Pico" },
    ],
  },
  {
    value: "La Rioja",
    label: "La Rioja",
    localidades: [
      { value: "La Rioja Capital", label: "La Rioja Capital" },
    ],
  },
  {
    value: "Mendoza",
    label: "Mendoza",
    localidades: [
      { value: "Mendoza Capital", label: "Mendoza Capital" },
      { value: "San Rafael", label: "San Rafael" },
      { value: "Godoy Cruz", label: "Godoy Cruz" },
    ],
  },
  {
    value: "Misiones",
    label: "Misiones",
    localidades: [
      { value: "Posadas", label: "Posadas" },
      { value: "Oberá", label: "Oberá" },
      { value: "Puerto Iguazú", label: "Puerto Iguazú" },
    ],
  },
  {
    value: "Neuquén",
    label: "Neuquén",
    localidades: [
      { value: "Neuquén Capital", label: "Neuquén Capital" },
      { value: "San Martín de los Andes", label: "San Martín de los Andes" },
    ],
  },
  {
    value: "Río Negro",
    label: "Río Negro",
    localidades: [
      { value: "Viedma", label: "Viedma" },
      { value: "San Carlos de Bariloche", label: "Bariloche" },
    ],
  },
  {
    value: "Salta",
    label: "Salta",
    localidades: [
      { value: "Salta Capital", label: "Salta Capital" },
      { value: "Tartagal", label: "Tartagal" },
    ],
  },
  {
    value: "San Juan",
    label: "San Juan",
    localidades: [
      { value: "San Juan Capital", label: "San Juan Capital" },
    ],
  },
  {
    value: "San Luis",
    label: "San Luis",
    localidades: [
      { value: "San Luis Capital", label: "San Luis Capital" },
      { value: "Villa Mercedes", label: "Villa Mercedes" },
    ],
  },
  {
    value: "Santa Cruz",
    label: "Santa Cruz",
    localidades: [
      { value: "Río Gallegos", label: "Río Gallegos" },
      { value: "Caleta Olivia", label: "Caleta Olivia" },
    ],
  },
  {
    value: "Santa Fe",
    label: "Santa Fe",
    localidades: [
      { value: "Rosario", label: "Rosario" },
      { value: "Santa Fe Capital", label: "Santa Fe Capital" },
    ],
  },
  {
    value: "Santiago del Estero",
    label: "Santiago del Estero",
    localidades: [
      { value: "Santiago del Estero Capital", label: "Santiago del Estero Capital" },
      { value: "La Banda", label: "La Banda" },
    ],
  },
  {
    value: "Tierra del Fuego",
    label: "Tierra del Fuego",
    localidades: [
      { value: "Ushuaia", label: "Ushuaia" },
      { value: "Río Grande", label: "Río Grande" },
    ],
  },
  {
    value: "Tucumán",
    label: "Tucumán",
    localidades: [
      { value: "San Miguel de Tucumán", label: "San Miguel de Tucumán" },
      { value: "Yerba Buena", label: "Yerba Buena" },
    ],
  },
];