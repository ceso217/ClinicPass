using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.Reportes;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.API.Controllers
{
    public class ReportesController
    {
        private readonly ClinicPassContext _db;

        public ReportesController(ClinicPassContext db)
        {
            _db = db;
        }

        // función para obtener la fecha de incio y de fin según el filtro
        public async Task<FiltroFechaDTO> FiltroDeFecha(FiltroFechaDTO? filtro = null)
        {
            DateTime fechaInicio = DateTime.UtcNow.Date;
            DateTime fechaFin = fechaInicio.AddDays(1); // Mañana a las 00:00
            if (filtro.TipoFiltro == FiltroFecha.UltimaSemana)
            {
                fechaInicio = fechaInicio.AddDays(-7);
            }
            else if (filtro.TipoFiltro == FiltroFecha.UltimoMes)
            {
                fechaInicio = fechaInicio.AddDays(-30);
            }
            else if (filtro.TipoFiltro == FiltroFecha.UltimoTrimestre)
            {
                fechaInicio = fechaInicio.AddDays(-90);
            }
            else if (filtro.TipoFiltro == FiltroFecha.UltimoAno)
            {
                fechaInicio = fechaInicio.AddDays(-365);
            }
            else if (filtro.TipoFiltro == FiltroFecha.Personalizado)
            {
                if (filtro == null)
                {
                    throw new ArgumentException("Para un filtro personalizado, las fechas son obligatorias.");
                }
                fechaInicio = DateTime.SpecifyKind(filtro.FechaInicio, DateTimeKind.Utc);
                fechaFin = DateTime.SpecifyKind(filtro.FechaFin, DateTimeKind.Utc);
            }

            return new FiltroFechaDTO
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };
        }

        // =======================
        // TURNOS
        // =======================

        // Obtener el total de turnos según el filtro de fecha
        public async Task<int> TotalTurnosPorFiltro(FiltroFecha filtro, FiltroFechaDTO? filtroFecha)
        {
            var fechasFiltro = await FiltroDeFecha(filtroFecha);

            var totalTurnos = await _db.Turnos
                    .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                    .CountAsync();

            return totalTurnos;
        }

        // Obtener turnos de estado programado y completado según el filtro de fecha (sin personalizada)
        public async Task<IEnumerable<EstadoTurnosDTO>> TurnosProgramadosCompletados(FiltroFechaDTO filtro)
        {
            var estadosValidos = new[] {
                EstadoTurno.Programado.ToString(),
                EstadoTurno.Completado.ToString()
            };

            var fechasFiltro = await FiltroDeFecha(filtro);

            return await _db.Turnos
                .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                .Where(t => estadosValidos.Contains(t.Estado))
                .GroupBy(t => t.Estado)
                .Select(grupo => new EstadoTurnosDTO
                {
                    Estado = grupo.Key,
                    CantidadTurnos = grupo.Count()
                })
                .ToListAsync();
        }

        // Obtener el total de turnos por el estado según el filtro de fecha
        public async Task<IEnumerable<EstadoTurnosDTO>> TotalTurnosEstado(FiltroFechaDTO filtro)
        {
            var fechasFiltro = await FiltroDeFecha(filtro);

            return await _db.Turnos
                .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                .GroupBy(t => t.Estado)
                .Select(grupo => new EstadoTurnosDTO
                {
                    Estado = grupo.Key,
                    CantidadTurnos = grupo.Count()
                })
                .ToListAsync();
        }

        // Obetener el total de turnos por especialidad del profesional según el filtro de fecha
        public async Task<IEnumerable<TotalEspecialidadDTO>> TotalTurnosEspecialidad(FiltroFechaDTO filtro)
        {
            var fechasFiltro = await FiltroDeFecha(filtro);

            return await _db.Turnos
                .Include(t => t.Profesional)
                .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                .GroupBy(t => t.Profesional.Especialidad)
                .Select(grupo => new TotalEspecialidadDTO
                {
                    Especialidad = grupo.Key ?? "Sin especialidad",
                    Total = grupo.Count()
                })
                .ToListAsync();
        }

        // =======================
        // PROFESIONALES
        // =======================

        // Obtener el total de profesionales activos
        public async Task<int> TotalProfesionalesActivos()
        {
            var totalProfesionales = await _db.Profesionales
                    .Where(p => p.Activo)
                    .CountAsync();
            return totalProfesionales;
        }

        // Obtener el total de profesionales por especialidad
        public async Task<IEnumerable<TotalEspecialidadDTO>> TotalProfesionalesPorEspecialidad()
        {
            return await _db.Profesionales
                .Where(p => p.Activo)
                .GroupBy(p => p.Especialidad)
                .Select(grupo => new TotalEspecialidadDTO
                {
                    Especialidad = grupo.Key ?? "Sin especialidad",
                    Total = grupo.Count()
                })
                .ToListAsync();
        }
    }
}