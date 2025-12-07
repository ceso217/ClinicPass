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

        // Campos clínicos
        public DateTime FechaPase { get; set; }
        public DateTime FechaCreacion { get; set; }      // cuando se cargó la ficha
        public string? FechaObservaciones { get; set; }  // notas del profesional
    }
}
