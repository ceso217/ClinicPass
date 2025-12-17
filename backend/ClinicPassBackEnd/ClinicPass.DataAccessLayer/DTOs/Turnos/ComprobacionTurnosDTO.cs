using ClinicPass.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Turnos
{
    public class ComprobacionTurnosDTO
    {
        public DateTime Fecha { get; set; }
        public int ProfesionalId { get; set; }
        public int PacienteId { get; set; }
        public Profesional Profesional { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!;
    }
}
