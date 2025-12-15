using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.DTOs
{
    public class AgregarTratamientoaHistoriaDTO
    {
        public int IdTratamiento { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime? FechaFin { get; set; }
    }
}

