using ClinicPass.DataAccessLayer.Data;
using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;    

namespace ClinicPass.BusinessLayer.Services
{
    public class FichaDeSeguimientoService
    {
        private readonly ClinicPassContext _context;
        private readonly UserManager<Profesional> _userManager;


        public FichaDeSeguimientoService(ClinicPassContext context, UserManager<Profesional> userManager)
        {
            _context = context;
            _userManager = userManager;  
        }

        // creo ficha de seguimiento
        public async Task<FichaDeSeguimientoDTO> CrearFichaAsync(FichaDeSeguimientoCreateDTO dto)
        {
            var ficha = new FichaDeSeguimiento
            {
                UsuarioId = dto.UsuarioId,
                HistorialClinicoId = dto.HistorialClinicoId,
                FechaPase = dto.FechaPase,
                FechaCreacion = DateTime.UtcNow,
                Observaciones = dto.Observaciones
            };

            _context.FichasDeSeguimiento.Add(ficha);
            await _context.SaveChangesAsync();

            // Traer el nombre del profesional
            var profesional = await _userManager.FindByIdAsync(ficha.UsuarioId.ToString()); 

            return new FichaDeSeguimientoDTO
            {
                IdFichaSeguimiento = ficha.IdFichaSeguimiento,
                UsuarioId = ficha.UsuarioId,
                NombreProfesional = profesional?.NombreCompleto ?? "Desconocido",
                HistorialClinicoId = ficha.HistorialClinicoId,
                FechaPase = ficha.FechaPase,
                FechaCreacion = ficha.FechaCreacion,
                Observaciones = ficha.Observaciones
            };
        }

        //obtengo fichas por id historia clinica
        public async Task<List<FichaDeSeguimientoDTO>> GetByHistoriaAsync(int idHistoria)
        {
            return await _context.FichasDeSeguimiento
                .Where(f => f.HistorialClinicoId.Equals(idHistoria))
                .Include(f => f.Profesional)
                .Select(f => new FichaDeSeguimientoDTO
                {
                    IdFichaSeguimiento = f.IdFichaSeguimiento,
                    UsuarioId = f.UsuarioId,
                    NombreProfesional = f.Profesional.NombreCompleto,
                    HistorialClinicoId = f.HistorialClinicoId,
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
                    UsuarioId = f.UsuarioId,
                    NombreProfesional = f.Profesional.NombreCompleto,
                    HistorialClinicoId = f.HistorialClinicoId,
                    FechaPase = f.FechaPase,
                    FechaCreacion = f.FechaCreacion,
                    Observaciones = f.Observaciones
                })
                .ToListAsync();
        }
    }
}

