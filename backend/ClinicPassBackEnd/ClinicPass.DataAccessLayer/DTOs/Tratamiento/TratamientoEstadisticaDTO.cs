using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Tratamiento
{
    public class TratamientoEstadisticaDTO
    {
        public int IdTratamiento { get; set; }
        public string NombreTratamiento { get; set; } = null!;

        public int TotalHistorias { get; set; }
        public int Activos { get; set; }
        public int Finalizados { get; set; }
    }
}
