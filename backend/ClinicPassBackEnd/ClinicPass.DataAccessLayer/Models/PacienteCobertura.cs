namespace ClinicPass.DataAccessLayer.Models
{
    public class PacienteCobertura
    {
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public int IdCobertura { get; set; }
        public CoberturaMedica Cobertura { get; set; } = null!;

        public string? NumeroAfiliado { get; set; }
    }
}
