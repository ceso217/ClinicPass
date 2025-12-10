using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }


        public int? FichaDeSeguimientoID { get; set; }
        public FichaDeSeguimiento? FichaDeSeguimiento { get; set; }


        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public ICollection<ProfesionalTurno> ProfesionalTurnos { get; set; } = new List<ProfesionalTurno>();
        public ICollection<PaseDiario> PasesDiarios { get; set; } = new List<PaseDiario>();
    }
}

