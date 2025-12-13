using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.Turnos;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.BusinessLayer.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ClinicPassContext _context;
        private readonly UserManager<Profesional> _userManager;

        public TurnoService(ClinicPassContext context, UserManager<Profesional> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<TurnoResponseDTO>> ObtenerPorPacienteAsync(int idPaciente)
        {
            return await _context.Turnos
                .Include(t => t.Paciente)
                .Where(t => t.IdPaciente == idPaciente)
                .Select(t => new TurnoResponseDTO
                {
                    IdTurno = t.IdTurno,
                    Fecha = t.Fecha,
                    Estado = t.Estado,
                    IdPaciente = t.IdPaciente,
                    NombrePaciente = t.Paciente.NombreCompleto,
                    IdFichaSeguimiento = t.IdFichaSeguimiento
                })
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<TurnoResponseDTO>> ObtenerTodosAsync()
        {
            return await _context.Turnos
                .Include(t => t.Paciente)
                .Select(t => new TurnoResponseDTO
                {
                    IdTurno = t.IdTurno,
                    Fecha = t.Fecha,
                    Estado = t.Estado,
                    IdPaciente = t.IdPaciente,
                    NombrePaciente = t.Paciente.NombreCompleto,
                    IdFichaSeguimiento = t.IdFichaSeguimiento
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TurnoResponseDTO> ObtenerPorIdAsync(int idTurno)
        {
            var turno = await _context.Turnos
                .Include(t => t.Paciente)
                .FirstOrDefaultAsync(t => t.IdTurno == idTurno);

            if (turno == null)
                throw new KeyNotFoundException("El turno no existe.");

            return new TurnoResponseDTO
            {
                IdTurno = turno.IdTurno,
                Fecha = turno.Fecha,
                Estado = turno.Estado,
                IdPaciente = turno.IdPaciente,
                NombrePaciente = turno.Paciente.NombreCompleto,
                IdFichaSeguimiento = turno.IdFichaSeguimiento
            };
        }

        public async Task<TurnoResponseDTO> CrearTurnoAsync(CrearTurnosDTO dto)
        {
            var paciente = await _context.Pacientes.FindAsync(dto.IdPaciente);
            if (paciente == null)
                throw new ArgumentException("El paciente no existe.");

            var fechaUtc = DateTime.SpecifyKind(dto.Fecha, DateTimeKind.Utc);

            if (await _context.Turnos.AnyAsync(t => t.Fecha == fechaUtc))
                throw new InvalidOperationException("Ya existe un turno en esa fecha.");

            var turno = new Turno
            {
                Fecha = fechaUtc,
                Estado = "Pendiente",
                IdPaciente = dto.IdPaciente,
                IdFichaSeguimiento = dto.IdFichaDeSeguimiento
            };

            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();

            return new TurnoResponseDTO
            {
                IdTurno = turno.IdTurno,
                Fecha = turno.Fecha,
                Estado = turno.Estado,
                IdPaciente = turno.IdPaciente,
                NombrePaciente = paciente.NombreCompleto,
                IdFichaSeguimiento = turno.IdFichaSeguimiento
            };
        }

        public async Task<Turno> ActualizarEstadoAsync(int idTurno, string estado)
        {
            var turno = await ObtenerTurnoAsync(idTurno);
            turno.Estado = estado;
            await _context.SaveChangesAsync();
            return turno;
        }

        public async Task<Turno> ActualizarFechaAsync(int idTurno, DateTime nuevaFecha)
        {
            if (nuevaFecha < DateTime.UtcNow)
                throw new ArgumentException("La fecha no puede ser anterior.");

            var turno = await ObtenerTurnoAsync(idTurno);
            turno.Fecha = DateTime.SpecifyKind(nuevaFecha, DateTimeKind.Utc);
            await _context.SaveChangesAsync();
            return turno;
        }

        public async Task<Turno> ActualizarFichaAsync(int idTurno, int? fichaId)
        {
            var turno = await ObtenerTurnoAsync(idTurno);
            turno.IdFichaSeguimiento = fichaId;
            await _context.SaveChangesAsync();
            return turno;
        }

        public async Task<Turno> ActualizarCompletoAsync(int idTurno, ActualizarTurnoCompletoDTO dto)
        {
            var turno = await ObtenerTurnoAsync(idTurno);

            turno.Fecha = DateTime.SpecifyKind(dto.Fecha, DateTimeKind.Utc);
            turno.Estado = dto.Estado;
            turno.IdPaciente = dto.PacienteId;
            turno.IdFichaSeguimiento = dto.FichaDeSeguimientoID;

            await _context.SaveChangesAsync();
            return turno;
        }

        public async Task EliminarAsync(int idTurno)
        {
            var turno = await ObtenerTurnoAsync(idTurno);
            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();
        }

        private async Task<Turno> ObtenerTurnoAsync(int idTurno)
        {
            var turno = await _context.Turnos.FindAsync(idTurno);
            if (turno == null)
                throw new KeyNotFoundException("El turno no existe.");
            return turno;
        }
    }
}