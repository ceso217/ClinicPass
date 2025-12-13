namespace ClinicPass.BusinessLayer.DTOs
{
    public class FichaDeSeguimientoDTO
    {
        public int IdFichaSeguimiento { get; set; }

        public string UsuarioId { get; set; }
        public string NombreProfesional { get; set; } = null!;

        public string HistorialClinicoId { get; set; }

        public DateTime FechaPase { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Observaciones { get; set; }
    }
}
