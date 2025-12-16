using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Turnos
{
    public class CrearTurnosDTO
    {
        public DateTime Fecha { get; set; }
        public int IdPaciente { get; set; }
        public int ProfesionalId { get; set; }
        public int? IdFichaDeSeguimiento { get; set; }
    }
}
