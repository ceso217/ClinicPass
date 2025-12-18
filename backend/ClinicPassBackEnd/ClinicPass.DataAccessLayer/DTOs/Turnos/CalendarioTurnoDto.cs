using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Turnos
{
    public class CalendarioTurnoDto
    {
        public int IdTurno { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        public int ProfesionalId { get; set; }
        public string ProfesionalNombre { get; set; }

        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; }
    }
}
