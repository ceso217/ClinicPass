using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs
{
    public class RegisterSuccessDTO
    {
        public string Dni { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
