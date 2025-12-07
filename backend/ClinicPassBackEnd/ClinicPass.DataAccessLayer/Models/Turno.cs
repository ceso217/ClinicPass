using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }

        //Turno pertenece a un Paciente
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;

        //Turno puede tener una ficha (después de ser atendido)
        public int? IdFichaSeguimiento { get; set; }
        public FichaDeSeguimiento? FichaDeSeguimiento { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string? Frecuencia { get; set; }
        public ICollection<ProfesionalTurno> Profesionales { get; set; } = new List<ProfesionalTurno>();

        // RELACIÓN N–N con Tratamiento mediante Pase
        public ICollection<PaseDiario> Pases { get; set; } = new List<PaseDiario>();
    }
}

