using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class Profesional
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Especialidad { get; set; }
        public string? Correo { get; set; }
        public bool Activo { get; set; } = true;

        public ICollection<ProfesionalTurno> ProfesionalTurnos { get; set; } = new List<ProfesionalTurno>();
        public ICollection<ProfesionalPaciente> ProfesionalPacientes { get; set; } = new List<ProfesionalPaciente>();
    }
}
