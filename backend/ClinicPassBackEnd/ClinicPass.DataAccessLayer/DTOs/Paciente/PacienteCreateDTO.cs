using System.ComponentModel.DataAnnotations;

namespace ClinicPass.BusinessLayer.DTOs
{
    public class PacienteCreateDTO
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [MaxLength(8),MinLength(7)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El DNI debe contener solo números")]
        public string Dni { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Calle { get; set; }
        public string Telefono { get; set; }
    }
}
