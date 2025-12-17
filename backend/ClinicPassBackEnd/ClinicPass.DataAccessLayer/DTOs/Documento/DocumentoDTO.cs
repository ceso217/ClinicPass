namespace ClinicPass.BusinessLayer.DTOs
{
    public class DocumentoDTO
    {
        public int IdDocumento { get; set; }
        public int IdFichaSeguimiento { get; set; }
        public int TipoDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Ruta { get; set; }
    }
}

