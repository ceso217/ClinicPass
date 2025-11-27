namespace ClinicPass.Models
{
    public class TutorResponsablePaciente
    {
        public int DNITutor { get; set; }
        public Tutor Tutor { get; set; } = null!;

        public int DNIPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;
    }
}
