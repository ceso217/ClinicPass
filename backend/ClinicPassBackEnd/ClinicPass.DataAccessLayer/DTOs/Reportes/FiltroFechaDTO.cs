using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Reportes
{
    public enum FiltroFecha
    {
        Hoy,
        UltimaSemana,
        UltimoMes,
        UltimoTrimestre,
        UltimoAno,
        Personalizado
    }
    public class FiltroFechaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public FiltroFecha TipoFiltro { get; set; }
    }
}
