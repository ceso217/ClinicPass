using System.ComponentModel.DataAnnotations;

namespace ClinicPass.Models
{
    public class Documento
    {
        [Key]
        public int IdDocumento { get; set; }

        public int IdFichaSeguimiento { get; set; }
        public FichaDeSeguimiento FichaSeguimiento { get; set; } = null!;

        public int TipoDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Ruta { get; set; }
    }
}
