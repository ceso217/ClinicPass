using System.ComponentModel.DataAnnotations;

namespace ClinicPass.Models
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }
        public DateTime Fecha { get; set; }
        public string? Estado { get; set; }

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public ICollection<ProfesionalTurno> ProfesionalTurnos { get; set; } = new List<ProfesionalTurno>();
        public ICollection<PaseDiario> PasesDiarios { get; set; } = new List<PaseDiario>();
    }
}
