using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Ficha
{
    public class FichaDeSeguimientoUpdateDTO
    {
        public int? IdUsuario { get; set; }
        public int? IdHistorialClinico { get; set; }
        public int? TratamientoId { get; set; }
        public string? Observaciones { get; set; }
    }

}
