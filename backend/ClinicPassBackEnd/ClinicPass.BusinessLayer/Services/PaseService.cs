using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.BusinessLayer.Services
{
    public class PaseService
    {
        private readonly ClinicPassContext _context;

        public PaseService(ClinicPassContext context)
        {
            _context = context;
        }

        public async Task<PaseDTO> CrearAsync(PaseCreateDTO dto)
        {
            var pase = new PaseDiario
            {
                IdTratamiento = dto.IdTratamiento,
                IdTurno = dto.IdTurno,
                FrecuenciaTurno = dto.FrecuenciaTurno
            };

            _context.Pases.Add(pase);
            await _context.SaveChangesAsync();

            var turno = await _context.Turnos.Include(t => t.Profesional)
                                             .FirstAsync(t => t.IdTurno == dto.IdTurno);

            return new PaseDTO
            {
                IdTratamiento = dto.IdTratamiento,
                IdTurno = dto.IdTurno,
                FrecuenciaTurno = dto.FrecuenciaTurno,
                FechaTurno = turno.Fecha,
                ProfesionalNombre = turno.Profesional.NombreCompleto
            };
        }

        public async Task<List<PaseDTO>> GetByTratamiento(int idTratamiento)
        {
            return await _context.Pases
                .Where(p => p.IdTratamiento == idTratamiento)
                .Include(p => p.Turno)
                .ThenInclude(t => t.Profesional)
                .Select(p => new PaseDTO
                {
                    IdTratamiento = p.IdTratamiento,
                    IdTurno = p.IdTurno,
                    FrecuenciaTurno = p.FrecuenciaTurno,
                    FechaTurno = p.Turno.Fecha,
                    ProfesionalNombre = p.Turno.Profesional.NombreCompleto
                })
                .ToListAsync();
        }
    }
}
