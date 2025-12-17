using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Reportes
{
    public class TotalEspecialidadDTO
    {
        public string Especialidad { get; set; } = null!;
        public int Total { get; set; }
    }
}
