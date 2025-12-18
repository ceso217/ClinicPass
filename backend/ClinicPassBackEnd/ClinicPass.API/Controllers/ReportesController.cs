using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.Reportes;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using ClinicPass.BusinessLayer.Services;


namespace ClinicPass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ClinicPassContext _db;
        private readonly ReportesService _filtroService;


		public ReportesController(ClinicPassContext db, ReportesService filtroService)
        {
            _db = db;
			_filtroService = filtroService;
		}

        // función para obtener la fecha de incio y de fin según el filtro


        // =======================
        // TURNOS
        // =======================

        // Obtener el total de turnos según el filtro de fecha
        [HttpPost("turnos/total")]
        public async Task<int> TotalTurnosPorFiltro([FromBody] FiltroFechaDTO? filtroFecha)
        {
            var fechasFiltro = await _filtroService.FiltroDeFecha(filtroFecha);

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

            var fechasFiltro = await _filtroService.FiltroDeFecha(filtro);

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
            var fechasFiltro = await _filtroService.FiltroDeFecha(filtro);

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
            var fechasFiltro = await _filtroService.FiltroDeFecha(filtro);
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

        // Obetener turnos de hoy de un profesional, completados y pendientes
        [HttpGet("turnos/profesional/{idProfesional}/hoy")]
        public async Task<IEnumerable<TurnosHoyProfesionalDTO>> TurnosHoyProfesional(int idProfesional)
        {
            var fechaInicio = DateTime.UtcNow.Date;
            var fechaFin = fechaInicio.AddDays(1); // Mañana a las 00:00
            return await _db.Turnos
                .Where(t => t.ProfesionalId == idProfesional)
                .Where(t => t.Fecha >= fechaInicio && t.Fecha < fechaFin)
                .Where(t => t.Estado == EstadoTurno.Completado.ToString() || t.Estado == EstadoTurno.Pendiente.ToString())
                .GroupBy(t => t.Profesional)
                .Select(t => new TurnosHoyProfesionalDTO
                {
                    ProfesionalId = t.Key.Id,
                    ProfesionalNombre = t.Key.NombreCompleto,
                    CantidadTurnosHoy = t.Count(),
                    Completados = t.Count(turno => turno.Estado == EstadoTurno.Completado.ToString()),
                    Pendientes = t.Count(turno => turno.Estado == EstadoTurno.Pendiente.ToString())
                })
                .ToListAsync();
        }

        // Obetener el número total de turnos por especialidad del profesional según el filtro de fecha
        [HttpPost("turnos/total-por-especialidad")]
        public async Task<IEnumerable<TotalEspecialidadDTO>> TotalTurnosEspecialidad([FromBody] FiltroFechaDTO filtro)
        {
            var fechasFiltro = await _filtroService.FiltroDeFecha(filtro);

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
            var fechasFiltro = await _filtroService.FiltroDeFecha(filtro);

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
        public async Task<IActionResult> TotalPacientes()
        {
            try
            {
                var totalPacientes = await _db.Pacientes.CountAsync();
                return Ok(totalPacientes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en TotalPacientes: {ex.Message}");
                return StatusCode(500, "Error al obtener el conteo de pacientes");
            }
        }

        // Obtener total de pacientes atendidos según el filtro de fecha
        [HttpPost("pacientes/total-atendidos")]
        public async Task<int> TotalPacientesAtendidos([FromBody] FiltroFechaDTO filtro)
        {
            var fechasFiltro = await _filtroService.FiltroDeFecha(filtro);
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

        // =======================
        // FICHA DE SEGUIMIENTO
        // =======================

        // Obtener el total de fichas de seguimiento
        [HttpGet("fichas/total")]
        public async Task<int> TotalFichasDeSeguimiento()
        {
            var totalFichas = await _db.FichasDeSeguimiento.CountAsync();
            return totalFichas;
        }

        // =======================
        // TRATAMIENTO
        // =======================

        // Obtener total de pacientes por tratamiento y cuantos pacientes lo finalizaron
        [HttpGet("tratamientos/pacientes-por-tratamiento")]
        public async Task<IEnumerable<PacientesPorTratamientoDTO>> PacientesPorTratamiento()
        {
            return await _db.HistorialClinicoTratamientos
                .Include(hct => hct.Tratamiento)
                .Include(hct => hct.HistoriaClinica)
                .ThenInclude(hc => hc.Paciente)
                .GroupBy(hct => hct.Tratamiento.Nombre)
                .Select(g => new PacientesPorTratamientoDTO
                {
                    TratamientoNombre = g.Key,
                    CantidadPacientes = g.Select(hct => hct.HistoriaClinica.IdPaciente).Distinct().Count(),
                    PacientesFinalizados = g.Count(hct => hct.FechaFin != null)
                })
                .ToListAsync();
        }
    }
}