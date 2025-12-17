using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class HistorialClinicoTratamiento
    {
        public int IdTratamiento { get; set; }
        public Tratamiento Tratamiento { get; set; } = null!;

        public int IdHistorialClinico { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; } = null!;

        public string Motivo { get; set; } = null!;

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public bool Activo { get; set; } = true;
    }
}

