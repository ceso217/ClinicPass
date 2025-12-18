using ClinicPass.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistoriaClinica
{
    public class HistoriaClinicaOrdenadaDTO
    {
        public PacienteDTO Paciente { get; set; } = null!;
        public HistoriaClinicaOrdenadaDataDTO HistoriaClinica { get; set; } = null!;
    }
}
