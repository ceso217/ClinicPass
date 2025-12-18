using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.DTOs.Tratamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistoriaClinica
{
    public class HistoriaClinicaDetalleDTO
    {
        public PacienteDTO Paciente { get; set; } = null!;
        public HistoriaClinicaDTO HistoriaClinica { get; set; } = null!;

        public List<TratamientoDetalleDTO> Tratamientos { get; set; } = new();

        public List<FichaDeSeguimientoHistorialDTO> Fichas { get; set; } = new();
    }
}
