using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ClinicPass.DataAccessLayer.Models
{
    public class Profesional :IdentityUser<int>
    {
        //[Key]
        //public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string? Especialidad { get; set; }
        public bool Activo { get; set; } = true;

        // Estas propiedades ya son heredadas del IdentityUser<int>
        //public string? Telefono { get; set; }
        //public string? Correo { get; set; }

        // profesional - turno es ahora relación uno a uno
        //public ICollection<ProfesionalTurno> ProfesionalTurnos { get; set; } = new List<ProfesionalTurno>();
        public ICollection<ProfesionalPaciente> ProfesionalPacientes { get; set; } = new List<ProfesionalPaciente>();
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
        public ICollection<FichaDeSeguimiento> FichasDeSeguimiento { get; set; } = new List<FichaDeSeguimiento>();
    }
}
