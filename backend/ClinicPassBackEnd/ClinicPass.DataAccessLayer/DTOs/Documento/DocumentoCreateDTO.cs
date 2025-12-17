namespace ClinicPass.BusinessLayer.DTOs
{
    public class DocumentoCreateDTO
    {
        public int IdFichaSeguimiento { get; set; }
        public int TipoDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Ruta { get; set; }
    }
}
