using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Reportes
{
    public class TurnosHoyProfesionalDTO
    {
        public int ProfesionalId { get; set; }
        public string ProfesionalNombre { get; set; }
        public int CantidadTurnosHoy { get; set; }
        public int Completados { get; set; }
        public int Pendientes{ get; set; }
    }
}
