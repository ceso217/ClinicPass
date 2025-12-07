namespace ClinicPass.BusinessLayer.DTOs
{
    public class FichaDeSeguimientoDTO
    {
        public int IdFichaSeguimiento { get; set; }

        public int IdUsuario { get; set; }
        public string NombreProfesional { get; set; } = null!;

        public int IdHistorialClinico { get; set; }

        public DateTime FechaPase { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? FechaObservaciones { get; set; }
    }
}
