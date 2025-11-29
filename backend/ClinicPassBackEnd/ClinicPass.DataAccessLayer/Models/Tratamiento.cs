using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class Tratamiento
    {
        [Key]
        public int IdTratamiento { get; set; }

        public int TipoTratamiento { get; set; }
        public string? Descripcion { get; set; }

        public ICollection<HCTratamiento> HCTratamientos { get; set; } = new List<HCTratamiento>();
        public ICollection<PacienteTratamiento> PacienteTratamientos { get; set; } = new List<PacienteTratamiento>();
    }
}
