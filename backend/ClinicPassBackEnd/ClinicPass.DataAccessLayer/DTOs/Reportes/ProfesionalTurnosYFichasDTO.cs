using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Reportes
{
    public class ProfesionalTurnosYFichasDTO
    {
        public int IdProfesional { get; set; }
        public string NombreProfesional { get; set; } = null!;
        public string? Especialidad { get; set; }
        public int CantidadTurnos { get; set; }
        public int CantidadFichasDeSeguimiento { get; set; }
    }
}