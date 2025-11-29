namespace ClinicPass.DataAccessLayer.Models
{

    public class ProfesionalPaciente
    {
        public int IdUsuario { get; set; }
        public Profesional Profesional { get; set; } = null!;

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;
    }
}