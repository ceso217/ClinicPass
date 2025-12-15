using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }

        public DateTime Fecha { get; set; }
        public string? Estado { get; set; }

        // FK correcta
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;

        // FK opcional a ficha
        public int? IdFichaSeguimiento { get; set; }
        public FichaDeSeguimiento? FichaDeSeguimiento { get; set; }

        public ICollection<ProfesionalTurno> ProfesionalTurnos { get; set; } = new List<ProfesionalTurno>();
        public ICollection<PaseDiario> Pases { get; set; } = new List<PaseDiario>();
    }
}

