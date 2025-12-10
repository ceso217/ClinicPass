namespace ClinicPass.BusinessLayer.DTOs
{
    public class FichaDeSeguimientoCreateDTO
    {
        public int IdUsuario { get; set; }
        public int IdHistorialClinico { get; set; }

        public DateTime FechaPase { get; set; }
        public string? Observaciones { get; set; }
    }
}

