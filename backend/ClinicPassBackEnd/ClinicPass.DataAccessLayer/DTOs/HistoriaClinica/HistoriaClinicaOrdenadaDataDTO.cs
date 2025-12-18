using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistoriaClinica
{
    public class HistoriaClinicaOrdenadaDataDTO
    {
        public int IdHistorialClinico { get; set; }
        public bool Activa { get; set; }
        public string? AntecedentesFamiliares { get; set; }
        public string? AntecedentesPersonales { get; set; }

        public List<TratamientoConFichasDTO> Tratamientos { get; set; } = new();
        public List<FichaDeSeguimientoDTO> FichasSinTratamiento { get; set; } = new();
    }
}
