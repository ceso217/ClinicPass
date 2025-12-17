using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.Ficha;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.BusinessLayer.Services
{
    public class FichaDeSeguimientoService : IFichaDeSeguimientoService
    {
        private readonly ClinicPassContext _context;
        private readonly UserManager<Profesional> _userManager;

        public FichaDeSeguimientoService(
            ClinicPassContext context,
            UserManager<Profesional> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // =========================
        // CREAR FICHA
        // =========================
        public async Task<FichaDeSeguimientoDTO> CrearFichaAsync(FichaDeSeguimientoCreateDTO dto)
        {
            // validar historia clínica
            var historia = await _context.HistoriasClinicas
                .FirstOrDefaultAsync(h => h.IdHistorialClinico == dto.IdHistorialClinico);

            if (historia == null)
                throw new Exception("Historia clínica inexistente.");

            // validar profesional
            var profesional = await _userManager.FindByIdAsync(dto.IdUsuario.ToString());
            if (profesional == null)
                throw new Exception("Profesional inexistente.");

            if (dto.TratamientoId.HasValue)
            {
                var existe = await _context.Tratamientos
                    .AnyAsync(t => t.IdTratamiento == dto.TratamientoId.Value);

                if (!existe)
                    throw new Exception("Tratamiento inexistente.");
            }


            var ficha = new FichaDeSeguimiento
            {
                IdUsuario = dto.IdUsuario,               // ✅ int
                IdHistorialClinico = dto.IdHistorialClinico,
                //FechaPase = dto.FechaPase,
                TratamientoId= dto.TratamientoId,
                FechaCreacion = DateTime.UtcNow,
                Observaciones = dto.Observaciones
            };

            _context.FichasDeSeguimiento.Add(ficha);
            await _context.SaveChangesAsync();

            return new FichaDeSeguimientoDTO
            {
                IdFichaSeguimiento = ficha.IdFichaSeguimiento,
                IdUsuario = ficha.IdUsuario,
                NombreProfesional = profesional.NombreCompleto,
                IdHistorialClinico = ficha.IdHistorialClinico,
                //FechaPase = ficha.FechaPase,
                FechaCreacion = ficha.FechaCreacion,
                Observaciones = ficha.Observaciones
            };
        }

        // =========================
        // FICHAS POR HISTORIA
        // =========================
        public async Task<List<FichaDeSeguimientoDTO>> GetByHistoriaAsync(int idHistoria)
        {
            return await _context.FichasDeSeguimiento
                .Where(f => f.IdHistorialClinico == idHistoria)
                .Include(f => f.Profesional)
                .Select(f => new FichaDeSeguimientoDTO
                {
                    IdFichaSeguimiento = f.IdFichaSeguimiento,
                    IdUsuario = f.IdUsuario,
                    NombreProfesional = f.Profesional.NombreCompleto,
                    IdHistorialClinico = f.IdHistorialClinico,
                    //FechaPase = f.FechaPase,
                    FechaCreacion = f.FechaCreacion,
                    Observaciones = f.Observaciones
                })
                .ToListAsync();
        }

        // =========================
        // FICHAS POR PACIENTE
        // =========================
        public async Task<List<FichaDeSeguimientoDTO>> GetByPacienteAsync(int idPaciente)
        {
            return await _context.FichasDeSeguimiento
                .Include(f => f.HistoriaClinica)
                .Include(f => f.Profesional)
                .Where(f => f.HistoriaClinica.IdPaciente == idPaciente)
                .Select(f => new FichaDeSeguimientoDTO
                {
                    IdFichaSeguimiento = f.IdFichaSeguimiento,
                    IdUsuario = f.IdUsuario,
                    NombreProfesional = f.Profesional.NombreCompleto,
                    IdHistorialClinico = f.IdHistorialClinico,
                    //FechaPase = f.FechaPase,
                    FechaCreacion = f.FechaCreacion,
                    Observaciones = f.Observaciones
                })
                .ToListAsync();
        }
        // =========================
        // PUT FICHA
        // =========================
        public async Task<bool> UpdateAsync(int idFicha, FichaDeSeguimientoUpdateDTO dto)
        {
            var ficha = await _context.FichasDeSeguimiento
                .FirstOrDefaultAsync(f => f.IdFichaSeguimiento == idFicha);

            if (ficha == null)
                return false;

            if (dto.IdUsuario.HasValue)
                ficha.IdUsuario = dto.IdUsuario.Value;

            if (dto.IdHistorialClinico.HasValue)
                ficha.IdHistorialClinico = dto.IdHistorialClinico.Value;

            if (dto.TratamientoId.HasValue)
                ficha.TratamientoId = dto.TratamientoId;
            else if (dto.TratamientoId == null)
                ficha.TratamientoId = null; 

            if (dto.Observaciones != null)
                ficha.Observaciones = dto.Observaciones;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}