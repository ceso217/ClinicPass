using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Turnos
{
    public class TurnoDTO
    {
		

		public DateTime Fecha { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public int? IdFichaDeSeguimiento { get; set; }
		public string Estado { get; set; }
	}
}
