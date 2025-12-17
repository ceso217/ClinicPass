using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistorialClinicoTratamiento
{
    public class HistorialClinicoTratamientoDTO
    {
        public int IdTratamiento { get; set; }
        public string NombreTratamiento { get; set; } = null!;

        public int IdHistorialClinico { get; set; }

        public string Motivo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Activo { get; set; }
    }
}
