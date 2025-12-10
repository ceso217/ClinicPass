using ClinicPass.DataAccessLayer.Data;
using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.BusinessLayer.Services
{
    public class FichaDeSeguimientoService
    {
        private readonly ClinicPassContext _context;

        public FichaDeSeguimientoService(ClinicPassContext context)
        {
            _context = context;
        }

        // creo ficha de seguimiento
        public async Task<FichaDeSeguimientoDTO> CrearFichaAsync(FichaDeSeguimientoCreateDTO dto)
        {
            var ficha = new FichaDeSeguimiento
            {
                IdUsuario = dto.IdUsuario,
                IdHistorialClinico = dto.IdHistorialClinico,
                FechaPase = dto.FechaPase,
                FechaCreacion = DateTime.UtcNow,
                Observaciones = dto.Observaciones
            };

            _context.FichasDeSeguimiento.Add(ficha);
            await _context.SaveChangesAsync();

            // Traer el nombre del profesional
            var profesional = await _context.Profesionales.FindAsync(dto.IdUsuario);

            return new FichaDeSeguimientoDTO
            {
                IdFichaSeguimiento = ficha.IdFichaSeguimiento,
                IdUsuario = ficha.IdUsuario,
                NombreProfesional = profesional?.NombreCompleto ?? "Desconocido",
                IdHistorialClinico = ficha.IdHistorialClinico,
                FechaPase = ficha.FechaPase,
                FechaCreacion = ficha.FechaCreacion,
                Observaciones = ficha.Observaciones
            };
        }

        //obtengo fichas por id historia clinica
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
                    FechaPase = f.FechaPase,
                    FechaCreacion = f.FechaCreacion,
                    Observaciones = f.Observaciones
                })
                .ToListAsync();
        }

        //obtengo fichas por id paciente
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
                    FechaPase = f.FechaPase,
                    FechaCreacion = f.FechaCreacion,
                    Observaciones = f.Observaciones
                })
                .ToListAsync();
        }
    }
}

