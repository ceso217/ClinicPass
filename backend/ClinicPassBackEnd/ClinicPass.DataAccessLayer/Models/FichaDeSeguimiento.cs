using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class FichaDeSeguimiento
    {
        [Key]
        public int IdFichaSeguimiento { get; set; }

        // FK Profesional
        public int IdUsuario { get; set; }
        public Profesional Profesional { get; set; } = null!;

        // FK Historia Clínica
        public int IdHistorialClinico { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; } = null!;

        public DateTime FechaPase { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Observaciones { get; set; }

        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
        public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
    }
}