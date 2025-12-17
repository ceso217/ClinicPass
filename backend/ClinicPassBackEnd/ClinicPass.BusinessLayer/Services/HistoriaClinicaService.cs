using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.BusinessLayer.Services
{
    public class HistoriaClinicaService : IHistoriaClinicaService
    {
        private readonly ClinicPassContext _context;

        public HistoriaClinicaService(ClinicPassContext context)
        {
            _context = context;
        }

        public async Task<HistoriaClinicaDTO?> GetByIdAsync(int id)
        {
            var historia = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                
                .Include(h => h.Fichas)
                    .ThenInclude(f => f.Profesional)
                .FirstOrDefaultAsync(h => h.IdHistorialClinico == id);

            return historia == null ? null : MapToDTO(historia);
        }

        public async Task<HistoriaClinicaDTO?> GetHistoriaClinicaAsync(int idPaciente)
        {
            var historia = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                    
                .Include(h => h.Fichas)
                    .ThenInclude(f => f.Profesional)
                .FirstOrDefaultAsync(h => h.IdPaciente == idPaciente);

            return historia == null ? null : MapToDTO(historia);
        }

        public async Task<HistoriaClinicaDTO> CreateAsync(HistoriaClinicaCreateDTO dto)
        {
            var historia = new HistoriaClinica
            {
                IdPaciente = dto.IdPaciente,
                AntecedentesFamiliares = dto.AntecedentesFamiliares,
                AntecedentesPersonales = dto.AntecedentesPersonales,
                Activa = true
            };

            _context.HistoriasClinicas.Add(historia);
            await _context.SaveChangesAsync();

            await _context.Entry(historia).Reference(h => h.Paciente).LoadAsync();
            
                
               

            return MapToDTO(historia);
        }


        private HistoriaClinicaDTO MapToDTO(HistoriaClinica h)
        {
            return new HistoriaClinicaDTO
            {
                IdHistorialClinico = h.IdHistorialClinico,
                IdPaciente = h.IdPaciente,
                AntecedentesFamiliares = h.AntecedentesFamiliares,
                AntecedentesPersonales = h.AntecedentesPersonales,
                Activa = h.Activa,

                

                Fichas = h.Fichas.Select(f => new FichaDeSeguimientoDTO
                {
                    IdFichaSeguimiento = f.IdFichaSeguimiento,
                    IdUsuario = f.IdUsuario,
                    NombreProfesional = f.Profesional.NombreCompleto,
                    IdHistorialClinico = f.IdHistorialClinico,
                    //FechaPase = f.FechaPase,
                    FechaCreacion = f.FechaCreacion,
                    Observaciones = f.Observaciones
                }).ToList()
            };
        }
        public async Task<bool> UpdateAsync(int idHistorial, HistoriaClinicaUpdateDTO dto)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null) return false;

            historia.AntecedentesFamiliares = dto.AntecedentesFamiliares;
            historia.AntecedentesPersonales = dto.AntecedentesPersonales;
            historia.Activa = dto.Activa;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DesactivarAsync(int idHistorial)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null) return false;

            historia.Activa = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivarAsync(int idHistorial)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null) return false;

            historia.Activa = true;
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}