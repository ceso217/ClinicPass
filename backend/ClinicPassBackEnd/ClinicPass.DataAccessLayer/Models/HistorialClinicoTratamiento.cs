namespace ClinicPass.DataAccessLayer.Models
{
    public class HistorialClinicoTratamiento
    {
        public int IdTratamiento { get; set; }
        public Tratamiento Tratamiento { get; set; } = null!;

        public int IdHistorialClinico { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; } = null!;
    }
}

