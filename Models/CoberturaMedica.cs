using System.ComponentModel.DataAnnotations;

namespace ClinicPass.Models
{
    public class CoberturaMedica
    {
        [Key]
        public int IdCobertura { get; set; }

        public string? NombreCobertura { get; set; }
        public string? TipoCobertura { get; set; }

        public ICollection<PacienteCobertura> Pacientes { get; set; } = new();
    }
}
