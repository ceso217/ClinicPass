using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.HistorialClinicoTratamiento;
using ClinicPass.DataAccessLayer.DTOs.Tratamiento;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Services
{
    public class HistorialClinicoTratamientoService : IHistorialClinicoTratamientoService
    {
        private readonly ClinicPassContext _context;

        public HistorialClinicoTratamientoService(ClinicPassContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(HistorialClinicoTratamientoCreateDTO dto)
        {
            var relacion = new HistorialClinicoTratamiento
            {
                IdTratamiento = dto.IdTratamiento,
                IdHistorialClinico = dto.IdHistorialClinico,
                Motivo = dto.Motivo,
                FechaInicio = DateTime.UtcNow,
                FechaFin = null,
                Activo = true
            };

            _context.HistorialClinicoTratamientos.Add(relacion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HistorialClinicoTratamientoDTO>> GetAllAsync()
        {
            return await _context.HistorialClinicoTratamientos
                .Include(h => h.Tratamiento)
                .Select(h => new HistorialClinicoTratamientoDTO
                {
                    IdTratamiento = h.IdTratamiento,
                    NombreTratamiento = h.Tratamiento.Nombre,
                    IdHistorialClinico = h.IdHistorialClinico,
                    Motivo = h.Motivo,
                    FechaInicio = h.FechaInicio,
                    FechaFin = h.FechaFin,
                    Activo = h.Activo
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<HistorialClinicoTratamientoDTO>> GetByHistoriaClinicaAsync(int idHistorialClinico)
        {
            return await _context.HistorialClinicoTratamientos
                .Include(h => h.Tratamiento)
                .Where(h => h.IdHistorialClinico == idHistorialClinico)
                .Select(h => new HistorialClinicoTratamientoDTO
                {
                    IdTratamiento = h.IdTratamiento,
                    NombreTratamiento = h.Tratamiento.Nombre,
                    IdHistorialClinico = h.IdHistorialClinico,
                    Motivo = h.Motivo,
                    FechaInicio = h.FechaInicio,
                    FechaFin = h.FechaFin,
                    Activo = h.Activo
                })
                .ToListAsync();
        }

        public async Task<List<HistorialClinicoTratamientoDTO>> GetCantidadHistoriasPorTratamientoAsync()
        {
            return await _context.HistorialClinicoTratamientos
                .GroupBy(h => h.IdTratamiento)
                .Select(g => new HistorialClinicoTratamientoDTO
                {
                    IdTratamiento = g.Key,
                    IdHistorialClinico = g.Count()
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(int idTratamiento, int idHistorialClinico, HistorialClinicoTratamientoUpdateDTO dto)
        {
            var relacion = await _context.HistorialClinicoTratamientos
                .FindAsync(idTratamiento, idHistorialClinico);

            if (relacion == null) return false;

            relacion.Motivo = dto.Motivo;
            relacion.FechaFin = dto.FechaFin;
            relacion.Activo = dto.Activo;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FinalizarTratamientoAsync(int idTratamiento, int idHistorialClinico)
        {
            var relacion = await _context.HistorialClinicoTratamientos
                .FindAsync(idTratamiento, idHistorialClinico);

            if (relacion == null) return false;

            relacion.FechaFin = DateTime.UtcNow;
            relacion.Activo = false;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int idTratamiento, int idHistorialClinico)
        {
            var relacion = await _context.HistorialClinicoTratamientos
                .FindAsync(idTratamiento, idHistorialClinico);

            if (relacion == null) return false;

            _context.HistorialClinicoTratamientos.Remove(relacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TratamientoEstadisticaDTO?> GetEstadisticasPorTratamientoAsync(int idTratamiento)
        {
            var query = _context.HistorialClinicoTratamientos
                .Include(h => h.Tratamiento)
                .Where(h => h.IdTratamiento == idTratamiento);

            var existe = await query.AnyAsync();
            if (!existe) return null;

            return new TratamientoEstadisticaDTO
            {
                IdTratamiento = idTratamiento,
                NombreTratamiento = query.First().Tratamiento.Nombre,

                TotalHistorias = await query.CountAsync(),
                Activos = await query.CountAsync(h => h.Activo),
                Finalizados = await query.CountAsync(h => !h.Activo)
            };
        }

    }
}
