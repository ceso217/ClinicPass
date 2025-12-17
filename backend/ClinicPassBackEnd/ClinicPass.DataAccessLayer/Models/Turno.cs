using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }

        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        // FK correcta
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;

        // FK opcional a ficha
        public int? IdFichaSeguimiento { get; set; }
        public FichaDeSeguimiento? FichaDeSeguimiento { get; set; }

        // profesional - turno es ahora relación uno a uno
        //public ICollection<ProfesionalTurno> ProfesionalTurnos { get; set; } = new List<ProfesionalTurno>();

        // fk profesional uno a uno
        public int ProfesionalId { get; set; }
        public Profesional Profesional { get; set; }
        public ICollection<PaseDiario> Pases { get; set; } = new List<PaseDiario>();
    }
}