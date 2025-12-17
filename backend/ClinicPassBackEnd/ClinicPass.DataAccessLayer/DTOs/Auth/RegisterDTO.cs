using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Auth
{
	public class RegisterDTO
	{
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }= "";

        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria")]
        public string RepeatPassword { get; set; }

        // Datos personales
        [Required(ErrorMessage = "El DNI es obligatorio")]
        [MaxLength(8), MinLength(7)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El DNI debe contener solo números")]
        public string Dni { get; set; } = "";

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; } = "";
		public string PhoneNumber { get; set; } = "";

		// Datos profesionales
		public string Especialidad { get; set; } = "";
		public bool Activo { get; set; } = true;
		public string Rol { get; set; } = "Profesional";
	}
}
