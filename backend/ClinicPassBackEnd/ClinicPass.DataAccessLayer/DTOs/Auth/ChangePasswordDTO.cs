using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Auth
{
    public class ChangePasswordDTO
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        public required string ConfirmNewPassword { get; set; }
    }
}
