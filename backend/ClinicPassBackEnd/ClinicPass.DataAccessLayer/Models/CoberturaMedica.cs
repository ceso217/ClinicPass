using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class CoberturaMedica
    {
        [Key]
        public int IdCobertura { get; set; }

        public string? NombreCobertura { get; set; }
        public string? TipoCobertura { get; set; }

        public ICollection<PacienteCobertura> PacienteCoberturas = new List<PacienteCobertura>();

    }
}
