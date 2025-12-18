using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.Reportes;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
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
        [HttpPost("turnos/total")]
        public async Task<int> TotalTurnosPorFiltro([FromBody] FiltroFechaDTO? filtroFecha)
        {
            var fechasFiltro = await FiltroDeFecha(filtroFecha);

            var totalTurnos = await _db.Turnos
                    .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                    .CountAsync();

            return totalTurnos;
        }

        // Obtener turnos de estado programado y completado según el filtro de fecha (sin personalizada)
        [HttpPost("turnos/programados-completados")]
        public async Task<IEnumerable<EstadoTurnosDTO>> TurnosProgramadosCompletados([FromBody] FiltroFechaDTO filtro)
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

        // Obtener el número total de turnos por el estado según el filtro de fecha
        [HttpPost("turnos/total-por-estado")]
        public async Task<IEnumerable<EstadoTurnosDTO>> TotalTurnosEstado([FromBody] FiltroFechaDTO filtro)
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

        // Obtener turnos con estado por profesional según el filtro de fecha
        [HttpPost("turnos/por-profesional-estado")]
        public async Task<IEnumerable<TurnosPorProfesionalEstadoDTO>> ObtenerTurnosPorProfesionalEstado([FromBody] FiltroFechaDTO filtro)
        {
            var fechasFiltro = await FiltroDeFecha(filtro);
            return await _db.Turnos
                .Include(t => t.Profesional)
                .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                .GroupBy(t => t.Profesional)
                .Select(grupo => new TurnosPorProfesionalEstadoDTO
                {
                    IdProfesional = grupo.Key.Id,
                    NombreProfesional = grupo.Key.NombreCompleto,
                    Especialidad = grupo.Key.Especialidad,
                    CantTurnosCompletados = grupo.Count(t => t.Estado == EstadoTurno.Completado.ToString()),
                    CantTurnosCancelados = grupo.Count(t => t.Estado == EstadoTurno.Cancelado.ToString()),
                    CantTurnosPendientes = grupo.Count(t => t.Estado == EstadoTurno.Pendiente.ToString()),
                    CantTurnosProgramados = grupo.Count(t => t.Estado == EstadoTurno.Programado.ToString())
                })
                .ToListAsync();
        }


        // Obetener el número total de turnos por especialidad del profesional según el filtro de fecha
        [HttpPost("turnos/total-por-especialidad")]
        public async Task<IEnumerable<TotalEspecialidadDTO>> TotalTurnosEspecialidad([FromBody] FiltroFechaDTO filtro)
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

        // Obtener el total de profesionales con especialidad, turnos, y fichas de seguimiento
        [HttpPost("profesionales/actividad")]
        public async Task<IEnumerable<ProfesionalTurnosYFichasDTO>> TotalProfesionalesConTurnosYFichas([FromBody] FiltroFechaDTO filtro)
        {
            var fechasFiltro = await FiltroDeFecha(filtro);

            return await _db.Turnos
                    .Include(t => t.Profesional)
                    .Include(t => t.FichaDeSeguimiento)
                    .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                    .GroupBy(t => t.Profesional)
                    .Select(grupo => new ProfesionalTurnosYFichasDTO
                    {
                        IdProfesional = grupo.Key.Id,
                        NombreProfesional = grupo.Key.NombreCompleto,
                        Especialidad = grupo.Key.Especialidad,
                        CantidadTurnos = grupo.Count(),
                        CantidadFichasDeSeguimiento = grupo.Count(t => t.IdFichaSeguimiento != null)
                    }).ToListAsync();
        }

        // Obtener el total de profesionales activos
        [HttpGet("profesionales/total-activos")]
        public async Task<int> TotalProfesionalesActivos()
        {
            var totalProfesionales = await _db.Profesionales
                    .Where(p => p.Activo)
                    .CountAsync();
            return totalProfesionales;
        }

        // Obtener el total de profesionales por especialidad
        [HttpGet("profesionales/total-por-especialidad")]
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

        // =======================
        // PACIENTES
        // =======================

        // Obtener total de pacientes general
        [HttpGet("pacientes/total")]
        public async Task<int> TotalPacientes()
        {
            var totalPacientes = await _db.Pacientes.CountAsync();
            return totalPacientes;
        }

        // Obtener total de pacientes atendidos según el filtro de fecha
        [HttpPost("pacientes/total-atendidos")]
        public async Task<int> TotalPacientesAtendidos([FromBody] FiltroFechaDTO filtro)
        {
            var fechasFiltro = await FiltroDeFecha(filtro);
            var totalPacientes = await _db.Turnos
                .Where(t => t.Estado == EstadoTurno.Completado.ToString())
                .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                .Select(t => t.IdPaciente)
                .Distinct()
                .CountAsync();

            return totalPacientes;
        }

        // Obtener el número total de pacientes por localidad
        [HttpGet("pacientes/total-por-localidad")]
        public async Task<IEnumerable<PacientesLocalidadDTO>> TotalPacientesPorLocalidad()
        {
            return await _db.Pacientes
                .GroupBy(p => new { p.Localidad, p.Provincia })
                .Select(g => new PacientesLocalidadDTO
                {
                    Provincia = g.Key.Provincia ?? "Sin provincia",
                    Localidad = g.Key.Localidad ?? "Sin localidad",
                    CantidadPacientes = g.Count()
                })
                .ToListAsync();
        }
    }
}