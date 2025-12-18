using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.Turnos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using ClinicPass.DataAccessLayer.DTOs.Reportes;

namespace ClinicPass.BusinessLayer.Services
{
    public class CalendarioService : ICalendarioService
    {
        private readonly ClinicPassContext _context;

        public CalendarioService(ClinicPassContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CalendarioTurnoDto>> ObtenerTodos()
        {
            return await _context.Turnos
                .Include(t => t.Profesional)
                .Include(t => t.Paciente)
                .Select(t => MapearDto(t))
                .ToListAsync();
        }
		public async Task<TurnoResumenDTO> ObtenerTodosCantidad()
		{
			var totalTurnos = await _context.Turnos.CountAsync();

			var turnosPorMes = await _context.Turnos
				.GroupBy(t => new { t.Fecha.Year, t.Fecha.Month })
				.Select(g => g.Count())
				.ToListAsync();

			var promedioMensual = turnosPorMes.Any()
				? turnosPorMes.Average()
				: 0;

			return new TurnoResumenDTO
			{
				TotalTurnos = totalTurnos,
				PromedioMensual = promedioMensual
			};
		}

		public async Task<IEnumerable<CalendarioTurnoDto>> ObtenerPorRangoFecha(DateTime fechaInicio,DateTime fechaFin)
        {
            // Normalizar fechas a UTC y cubrir el día completo
            fechaInicio = DateTime.SpecifyKind(
                fechaInicio.Date,
                DateTimeKind.Utc);

            fechaFin = DateTime.SpecifyKind(
                fechaFin.Date.AddDays(1).AddTicks(-1),
                DateTimeKind.Utc);

            return await _context.Turnos
                .Include(t => t.Profesional)
                .Include(t => t.Paciente)
                .Where(t =>
                    t.Fecha >= fechaInicio &&
                    t.Fecha <= fechaFin)
                .Select(t => new CalendarioTurnoDto
                {
                    IdTurno = t.IdTurno,
                    Fecha = t.Fecha,
                    Estado = t.Estado,
                    ProfesionalId = t.ProfesionalId,
                    ProfesionalNombre = t.Profesional.NombreCompleto,
                    PacienteId = t.IdPaciente,
                    PacienteNombre = t.Paciente.NombreCompleto
                })
                .ToListAsync();
        }



        public async Task<IEnumerable<CalendarioTurnoDto>> ObtenerPorProfesional(
            int profesionalId)
        {
            return await _context.Turnos
                .Include(t => t.Profesional)
                .Include(t => t.Paciente)
                .Where(t => t.ProfesionalId == profesionalId)
                .Select(t => MapearDto(t))
                .ToListAsync();
        }

        public async Task<IEnumerable<CalendarioTurnoDto>> ObtenerPorProfesionalYRangoFecha(int profesionalId,DateTime fechaInicio,DateTime fechaFin)
        {
            // Normalizar fechas a UTC y cubrir el día completo
            fechaInicio = DateTime.SpecifyKind(
                fechaInicio.Date,
                DateTimeKind.Utc);

            fechaFin = DateTime.SpecifyKind(
                fechaFin.Date.AddDays(1).AddTicks(-1),
                DateTimeKind.Utc);

            return await _context.Turnos
                .Include(t => t.Profesional)
                .Include(t => t.Paciente)
                .Where(t =>
                    t.ProfesionalId == profesionalId &&
                    t.Fecha >= fechaInicio &&
                    t.Fecha <= fechaFin)
                .Select(t => new CalendarioTurnoDto
                {
                    IdTurno = t.IdTurno,
                    Fecha = t.Fecha,
                    Estado = t.Estado,
                    ProfesionalId = t.ProfesionalId,
                    ProfesionalNombre = t.Profesional.NombreCompleto,
                    PacienteId = t.IdPaciente,
                    PacienteNombre = t.Paciente.NombreCompleto
                })
                .ToListAsync();
        }



        private static CalendarioTurnoDto MapearDto(DataAccessLayer.Models.Turno t)
        {
            return new CalendarioTurnoDto
            {
                IdTurno = t.IdTurno,
                Fecha = t.Fecha,
                Estado = t.Estado,
                ProfesionalId = t.ProfesionalId,
                ProfesionalNombre = t.Profesional.NombreCompleto,
                PacienteId = t.IdPaciente,
                PacienteNombre = t.Paciente.NombreCompleto
            };
        }
    }
}
