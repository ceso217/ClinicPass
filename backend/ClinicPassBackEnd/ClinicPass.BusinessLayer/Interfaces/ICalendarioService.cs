using ClinicPass.DataAccessLayer.DTOs.Turnos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface ICalendarioService
    {
        Task<IEnumerable<CalendarioTurnoDto>> ObtenerTodos();

        Task<IEnumerable<CalendarioTurnoDto>> ObtenerPorRangoFecha(
            DateTime fechaInicio,
            DateTime fechaFin);

        Task<IEnumerable<CalendarioTurnoDto>> ObtenerPorProfesional(
            int profesionalId);

        Task<IEnumerable<CalendarioTurnoDto>> ObtenerPorProfesionalYRangoFecha(
            int profesionalId,
            DateTime fechaInicio,
            DateTime fechaFin);
    }
}
