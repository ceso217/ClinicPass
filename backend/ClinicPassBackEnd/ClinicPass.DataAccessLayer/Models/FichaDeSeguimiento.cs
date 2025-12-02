using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class FichaDeSeguimiento
    {
        [Key]
        public int IdFichaSeguimiento { get; set; }

        public int IdUsuario { get; set; }
        public Profesional Profesional { get; set; } = null!;

        public int IdHistorialClinico { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; } = null!;

        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Observaciones { get; set; }

        public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
        public ICollection<PaseDiario> PasesDiarios { get; set; } = new List<PaseDiario>();
    }
}
