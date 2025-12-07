namespace ClinicPass.BusinessLayer.DTOs
{
    public class HistoriaClinicaDTO
    {
        public int IdHistorialClinico { get; set; }
        public int IdPaciente { get; set; }

        public int TipoPaciente { get; set; }
        public string? AntecedentesFamiliares { get; set; }
        public string? AntecedentesPersonales { get; set; }
        public bool Activa { get; set; }

        // Lista de tratamientos del paciente
        public List<TratamientoDTO> Tratamientos { get; set; } = new();

        // Lista de fichas clínicas del paciente
        public List<FichaDeSeguimientoDTO> Fichas { get; set; } = new();
    }
}


