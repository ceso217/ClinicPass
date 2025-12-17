using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Turnos
{
    public class ActualizarTurnoDTO
    {
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = null!;
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public int? FichaDeSeguimientoID { get; set; }
    }

}
