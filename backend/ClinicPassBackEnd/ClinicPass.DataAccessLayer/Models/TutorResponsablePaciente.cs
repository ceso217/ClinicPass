namespace ClinicPass.DataAccessLayer.Models
{
    public class TutorResponsablePaciente
    {
        public int DNITutor { get; set; }
        public Tutor Tutor { get; set; } = null!;

        public int IdPaciente { get; set; } 
        public Paciente Paciente { get; set; } = null!;
    }
}
