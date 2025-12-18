using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Reportes
{
    public class TurnosPorProfesionalEstadoDTO
    {
        public int IdProfesional { get; set; }
        public string NombreProfesional { get; set; } = null!;
        public string? Especialidad { get; set; }
        public int CantTurnosCompletados { get; set; }
        public int CantTurnosCancelados { get; set; }
        public int CantTurnosPendientes { get; set; }
        public int CantTurnosProgramados { get; set; }
    }
}
