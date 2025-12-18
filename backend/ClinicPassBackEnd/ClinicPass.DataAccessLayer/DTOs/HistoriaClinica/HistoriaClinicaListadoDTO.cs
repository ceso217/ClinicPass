using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistoriaClinica
{
    public class HistoriaClinicaListadoDTO
    {
        public int IdHistorialClinico { get; set; }
        public bool Activa { get; set; }

        public PacienteBasicoDTO Paciente { get; set; } = null!;
    }
}
