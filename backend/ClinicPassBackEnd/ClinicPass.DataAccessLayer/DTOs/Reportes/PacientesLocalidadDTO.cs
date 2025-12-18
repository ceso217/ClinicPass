using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Reportes
{
    public class PacientesLocalidadDTO
    {
        public string Provincia { get; set; } = null!;
        public string Localidad { get; set; } = null!;
        public int CantidadPacientes { get; set; }
    }
}
