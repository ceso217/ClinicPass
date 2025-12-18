using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Reportes
{
    public class PacientesPorTratamientoDTO
    {
        public string TratamientoNombre { get; set; } = null!;
        public int CantidadPacientes { get; set; }
        public int PacientesFinalizados { get; set; }

    }
}
