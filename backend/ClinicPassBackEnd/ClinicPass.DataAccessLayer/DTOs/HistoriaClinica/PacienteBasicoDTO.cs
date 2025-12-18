using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistoriaClinica
{
    public class PacienteBasicoDTO
    {
        public int IdPaciente { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public int Edad { get; set; }
    }
}
