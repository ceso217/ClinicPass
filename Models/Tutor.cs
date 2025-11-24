using System.ComponentModel.DataAnnotations;

namespace ClinicPass.Models
{
    public class Tutor
    {
        [Key]
        public int DNI { get; set; }

        public string? Parentesco { get; set; }
        public string? NumeroCompleto { get; set; }
        public string? Telefono { get; set; }
        public string? Ocupacion { get; set; }

        public ICollection<TutorResponsablePaciente> Pacientes { get; set; } = new List<TutorResponsablePaciente>();
    }
}
