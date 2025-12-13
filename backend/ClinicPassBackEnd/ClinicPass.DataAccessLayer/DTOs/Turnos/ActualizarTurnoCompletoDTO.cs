using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Turnos
{
    public class ActualizarTurnoCompletoDTO
    {
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = null!;
        public int PacienteId { get; set; }
        public int? FichaDeSeguimientoID { get; set; }
    }

}
