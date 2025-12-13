namespace ClinicPass.BusinessLayer.DTOs
{
    public class FichaDeSeguimientoCreateDTO
    {
        public string UsuarioId { get; set; }
        public string HistorialClinicoId { get; set; }

        public DateTime FechaPase { get; set; }
        public string? Observaciones { get; set; }
    }
}

