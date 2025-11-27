using System.ComponentModel.DataAnnotations;

namespace ClinicPass.Models
{
    public class Paciente
    {
        [Key]
        public int IdPaciente { get; set; }

        [Required, MaxLength(100)]
        public string NombreCompleto { get; set; } = null!;

        [Required, MaxLength(20)]
        public string Dni { get; set; } = null!;

        public DateTime? FechaNacimiento { get; set; }
        public string? Localidad { get; set; }
        public string? Provincia { get; set; }
        public string? Calle { get; set; }
        public string? Telefono { get; set; }

        public HistoriaClinica? HistoriaClinica { get; set; }

        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
        public ICollection<PacienteCobertura> PacienteCoberturas { get; set; } = new List<PacienteCobertura>();
        public ICollection<PacienteTratamiento> PacienteTratamientos { get; set; } = new List<PacienteTratamiento>();
        public ICollection<TutorResponsablePaciente> TutoresResponsables { get; set; } = new List<TutorResponsablePaciente>();
        public ICollection<ProfesionalPaciente> ProfesionalesVinculados { get; set; } = new List<ProfesionalPaciente>();
    }
}
