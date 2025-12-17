using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Reportes
{
    public class EstadoTurnosDTO
    {
        public int TurnoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = null!;
    }
}
