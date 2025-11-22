namespace ClinicPass.Models
{
    public class PacienteTratamiento
    {
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public int IdTratamiento { get; set; }
        public Tratamiento Tratamiento { get; set; } = null!;

        public DateTime? FechaInicio { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
