using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs
{
    public class ProfesionalDTO
    {
        public string NombreCompleto { get; set; }
        public string Dni { get; set; }
        public string? Especialidad { get; set; }
        public bool Activo { get; set; }
        public string Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public string? UserName { get; set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }

    }
}
