using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistorialClinicoTratamiento
{
    public class HistorialClinicoTratamientoCreateDTO
    {
        public int IdTratamiento { get; set; }
        public int IdHistorialClinico { get; set; }
        public string Motivo { get; set; } = null!;
    }
}
