using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistorialClinicoTratamiento
{
    public class HistorialClinicoTratamientoUpdateDTO
    {
        public string Motivo { get; set; } = null!;
        public DateTime? FechaFin { get; set; }
        public bool Activo { get; set; }
    }
}
