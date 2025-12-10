using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class Tratamiento
    {
        [Key]
        public int IdTratamiento { get; set; }

        public string TipoTratamiento { get; set; } = null!;
        public string Motivo { get; set; } = null!;
        public string? Descripcion { get; set; }

        // Relación N–N con Paciente a través de PacienteTratamiento
        public ICollection<PacienteTratamiento> Pacientes { get; set; } = new List<PacienteTratamiento>();
        public ICollection<PaseDiario> Pases { get; set; } = new List<PaseDiario>(); // Relación 1–N con PaseDiario

    }
}


