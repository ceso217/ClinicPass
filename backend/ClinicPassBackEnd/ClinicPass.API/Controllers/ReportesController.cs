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

        enum EstadoTurno
        {
            Pendiente,
            Completado,
            Cancelado,
            Programado
        }

        public enum FiltroFecha
        {
            Hoy,
            UltimaSemana,
            UltimoMes,
            UltimoTrimestre,
            UltimoAno,
            Personalizado
        }

        // función para obtener la fecha de inicio según el filtro
        public async Task<FiltroFechaDTO> FiltroDeFecha(FiltroFecha filtro, FiltroFechaDTO? filtroFechaDTO = null)
        {
            DateTime fechaInicio = DateTime.UtcNow.Date;
            DateTime fechaFin = fechaInicio.AddDays(1); // Mañana a las 00:00
            if (filtro == FiltroFecha.UltimaSemana)
            {
                fechaInicio = fechaInicio.AddDays(-7);
            }
            else if (filtro == FiltroFecha.UltimoMes)
            {
                fechaInicio = fechaInicio.AddDays(-30);
            }
            else if (filtro == FiltroFecha.UltimoTrimestre)
            {
                fechaInicio = fechaInicio.AddDays(-90);
            }
            else if (filtro == FiltroFecha.UltimoAno)
            {
                fechaInicio = fechaInicio.AddDays(-365);
            }
            else if(filtro == FiltroFecha.Personalizado )
            {
                if (filtroFechaDTO == null)
                {
                    throw new ArgumentException("Para un filtro personalizado, las fechas son obligatorias.");
                }
                fechaInicio = DateTime.SpecifyKind(filtroFechaDTO.FechaInicio, DateTimeKind.Utc);
                fechaFin = DateTime.SpecifyKind(filtroFechaDTO.FechaFin, DateTimeKind.Utc);
            }
            
            return new FiltroFechaDTO
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };
        }

        public async Task<IEnumerable<EstadoTurnosDTO>> TurnosProgramadosCompletados(FiltroFecha filtro)
        {
            var estadosValidos = new[] {
                EstadoTurno.Programado.ToString(),
                EstadoTurno.Completado.ToString()
            };

            var fechasFiltro = await FiltroDeFecha(filtro);

            return await _db.Turnos
                .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                .Where(t => estadosValidos.Contains(t.Estado))
                .Select(t => new EstadoTurnosDTO
                {
                    TurnoId = t.IdTurno,
                    Fecha = t.Fecha,
                    Estado = t.Estado
                })
                .ToListAsync();
        }

        public async Task<int> TotalTurnosPorFiltro(FiltroFecha filtro, DateTime? fechaInicioPersonalizada, FiltroFechaDTO? fechasPersonalizadas)
        {
            var fechasFiltro = await FiltroDeFecha(filtro, fechasPersonalizadas);

            var totalTurnos = await _db.Turnos
                    .Where(t => t.Fecha >= fechasFiltro.FechaInicio && t.Fecha < fechasFiltro.FechaFin)
                    .CountAsync();

            return totalTurnos;
        }
    }
}
