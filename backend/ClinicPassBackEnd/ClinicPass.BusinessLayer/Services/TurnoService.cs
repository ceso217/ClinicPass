using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.Turnos;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
                .Include(t => t.Profesional)
                .Where(t => t.IdPaciente == idPaciente)
                .Select(t => new TurnoResponseDTO
                {
                    IdTurno = t.IdTurno,
                    Fecha = t.Fecha,
                    Estado = t.Estado,
                    IdPaciente = t.IdPaciente,
                    NombrePaciente = t.Paciente.NombreCompleto,
                    IdFichaSeguimiento = t.IdFichaSeguimiento,
                    ProfesionalId = t.ProfesionalId,
                    NombreProfesional = t.Profesional.NombreCompleto
                })
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<TurnoResponseDTO>> ObtenerTodosAsync()
        {
            return await _context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Profesional)
                .Select(t => new TurnoResponseDTO
                {
                    IdTurno = t.IdTurno,
                    Fecha = t.Fecha,
                    Estado = t.Estado,
                    IdPaciente = t.IdPaciente,
                    NombrePaciente = t.Paciente.NombreCompleto,
                    IdFichaSeguimiento = t.IdFichaSeguimiento,
                    ProfesionalId = t.ProfesionalId,
                    NombreProfesional = t.Profesional.NombreCompleto
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TurnoResponseDTO> ObtenerPorIdAsync(int idTurno)
        {
            var turno = await _context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Profesional)
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
                IdFichaSeguimiento = turno.IdFichaSeguimiento,
                ProfesionalId = turno.ProfesionalId,
                NombreProfesional = turno.Profesional.NombreCompleto
            };
        }

        public async Task<ComprobacionTurnosDTO> comprobarConflictosTurno(int pacienteId, int profesionalId, DateTime fecha)
        {
            var inicioNuevoTurno = DateTime.SpecifyKind(fecha, DateTimeKind.Utc);
            const int duracionMinutos = 30;
            var finNuevoTurno = inicioNuevoTurno.AddMinutes(duracionMinutos);

            if (inicioNuevoTurno < DateTime.UtcNow)
                throw new ArgumentException("La fecha no puede ser anterior a la fecha actual.");
            var paciente = await _context.Pacientes.FindAsync(pacienteId);

            if (paciente == null)
                throw new ArgumentException("El paciente no existe.");

            var profesional = await _userManager.FindByIdAsync(profesionalId.ToString());

            if (profesional == null)
                throw new ArgumentException("El profesional no existe.");


            var turnosConflictoProfesional = await _context.Turnos
                .Where(t => t.ProfesionalId == profesionalId)
                .Where(t =>
                    t.Fecha < finNuevoTurno &&
                    t.Fecha.AddMinutes(duracionMinutos) > inicioNuevoTurno
                )
                .AnyAsync();

            if (turnosConflictoProfesional)
            {
                throw new InvalidOperationException("El profesional ya tiene ocupado con un turno ese horario.");
            }

            var turnosConflictoPaciente = await _context.Turnos
                .Where(t => t.IdPaciente == pacienteId)
                .Where(t =>
                    t.Fecha < finNuevoTurno &&
                    t.Fecha.AddMinutes(duracionMinutos) > inicioNuevoTurno
                )
                .AnyAsync();

            if (turnosConflictoPaciente)
            {
                throw new InvalidOperationException("El paciente ya tiene otro turno a ese horario.");
            }

            var dto = new ComprobacionTurnosDTO
            {
                Fecha = inicioNuevoTurno,
                Profesional = profesional,
                Paciente = paciente,
            };

            return dto;
        }

        public async Task<TurnoResponseDTO> CrearTurnoAsync(TurnoDTO dto)
        {
            var comprobacion = await comprobarConflictosTurno(dto.PacienteId, dto.ProfesionalId, dto.Fecha);

            var turno = new Turno
            {
                Fecha = comprobacion.Fecha,
                Estado = "Pendiente",
                IdPaciente = dto.PacienteId,
                ProfesionalId = dto.ProfesionalId,
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
                NombrePaciente = comprobacion.Paciente.NombreCompleto,
                IdFichaSeguimiento = turno.IdFichaSeguimiento,
                ProfesionalId = turno.ProfesionalId,
                NombreProfesional = comprobacion.Profesional.NombreCompleto
            };
        }

        public async Task<Turno> ActualizarEstadoAsync(int idTurno, string estado)
        {
            var turno = await ObtenerTurnoAsync(idTurno);
            turno.Estado = estado;
            await _context.SaveChangesAsync();
            return turno;
        }

        public async Task<Turno> ActualizarFechaAsync(int idTurno, ActualizarTurnoDTO dto)
        {
            var comprobacion = await comprobarConflictosTurno(dto.PacienteId, dto.ProfesionalId, dto.Fecha);

            var turno = await ObtenerTurnoAsync(idTurno);
            turno.Fecha = DateTime.SpecifyKind(comprobacion.Fecha, DateTimeKind.Utc);
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

        public async Task<TurnoResponseDTO> ActualizarCompletoAsync(int idTurno, ActualizarTurnoDTO dto)
        {
            var comprobacion = await comprobarConflictosTurno(dto.PacienteId, dto.ProfesionalId, dto.Fecha);

            var turno = await ObtenerTurnoAsync(idTurno);

            turno.Fecha = comprobacion.Fecha;
            turno.Estado = dto.Estado;
            turno.IdPaciente = dto.PacienteId;
            turno.IdFichaSeguimiento = dto.FichaDeSeguimientoID;
            turno.ProfesionalId = dto.ProfesionalId;

            await _context.SaveChangesAsync();

            var turnoResponse = new TurnoResponseDTO
            {
                IdTurno = turno.IdTurno,
                Fecha = turno.Fecha,
                Estado = turno.Estado,
                IdPaciente = turno.IdPaciente,
                NombrePaciente = comprobacion.Paciente.NombreCompleto,
                IdFichaSeguimiento = turno.IdFichaSeguimiento,
                ProfesionalId = turno.ProfesionalId,
                NombreProfesional = comprobacion.Profesional.NombreCompleto
            };
            return turnoResponse;
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