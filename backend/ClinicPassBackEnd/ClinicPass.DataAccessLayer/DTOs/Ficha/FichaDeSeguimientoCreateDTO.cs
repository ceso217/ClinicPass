
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ClinicPass.BusinessLayer.DTOs
{
    public class FichaDeSeguimientoCreateDTO
    {
        public int IdUsuario { get; set; }   
        public int IdHistorialClinico { get; set; }

        public DateTime FechaPase { get; set; }
        public string? Observaciones { get; set; }
    }
}

