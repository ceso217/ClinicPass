using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.Reportes
{
    public enum EstadoTurno
    {
        Pendiente,
        Completado,
        Cancelado,
        Programado
    }

    public class EstadoTurnosDTO
    {
        public string Estado { get; set; } = null!;
        public int CantidadTurnos { get; set; }
    }
}
