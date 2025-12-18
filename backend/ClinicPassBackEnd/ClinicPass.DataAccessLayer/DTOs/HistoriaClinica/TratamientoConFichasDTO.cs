using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistoriaClinica
{
    public class TratamientoConFichasDTO
    {
        public int IdTratamiento { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaInicio { get; set; }

        public List<FichaDeSeguimientoDTO> FichasDeSeguimiento { get; set; } = new();
    }
}
