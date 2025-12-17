using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class Tratamiento
    {
        [Key]
        public int IdTratamiento { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;

        // Relación N–N con Paciente a través de PacienteTratamiento
        public ICollection<HistorialClinicoTratamiento> Historiales { get; set; } = new List<HistorialClinicoTratamiento>();
    }
}


