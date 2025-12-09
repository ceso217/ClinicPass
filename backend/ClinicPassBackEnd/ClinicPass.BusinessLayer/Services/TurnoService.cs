
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.Data;
using ClinicPass.DataAccessLayer.DTOs.Turnos;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ClinicPass.BusinessLayer.Services
{
    public class TurnoService : ITurnoService

    {
        // Inyección de dependencia del contexto de la base de datos
        private readonly ClinicPassContext _context;
        public TurnoService(ClinicPassContext context)
        {
            _context = context;
        }


        // Método para obtener todos los turnos
        
        public async Task<IEnumerable<TurnoResponseDTO>> ObtenerTodosAsync()
        {
            return await _context.Turnos
                .Select(t => new TurnoResponseDTO
                {
                    IdTurno = t.IdTurno,
                    Fecha = t.Fecha,
                    Estado = t.Estado,
                    PacienteId = t.PacienteId,
                    NombrePaciente = t.Paciente.NombreCompleto,
                    FichaDeSeguimientoID = t.FichaDeSeguimientoID
                })
                .AsNoTracking()
                .ToListAsync();
        }


        // Método para obtener un turno por su ID
        public async Task<TurnoResponseDTO> ObtenerPorIdAsync(int idTurno)
        {
            var turno = await _context.Turnos
                .Where(t => t.IdTurno == idTurno)
                .Select(t => new TurnoResponseDTO
                {
                    IdTurno = t.IdTurno,
                    Fecha = t.Fecha,
                    Estado = t.Estado,
                    PacienteId = t.PacienteId,
                    NombrePaciente = t.Paciente.NombreCompleto,
                    FichaDeSeguimientoID = t.FichaDeSeguimientoID
                })
                .FirstOrDefaultAsync();

            if (turno == null)
                throw new KeyNotFoundException("El turno no existe.");

            return turno;
        }


        // Método para crear un nuevo turno
        public async Task<Turno> CrearTurnoAsync(CrearTurnosDTO dto)
        {
            // Convertir la fecha a UTC para evitar el error de PostgreSQL
            DateTime fechaUtc = dto.Fecha.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dto.Fecha, DateTimeKind.Utc)
                : dto.Fecha.ToUniversalTime();

            if (fechaUtc < DateTime.Now)
            {
                throw new ArgumentException("La fecha del turno no puede ser en el pasado.");
            }
            
            var paciente = await _context.Pacientes.FindAsync(dto.IdPaciente);
            if (paciente == null)
                throw new ArgumentException("El paciente no existe.");

            if (dto.IdFichaDeSeguimiento.HasValue)
            {
                var ficha = await _context.FichasDeSeguimiento.FindAsync(dto.IdFichaDeSeguimiento.Value);
                if (ficha == null)
                    throw new ArgumentException("La ficha de seguimiento especificada no existe.");
            }

            bool existeTurno = _context.Turnos.Any(t => t.Fecha == fechaUtc);

            if (existeTurno) {
                throw new InvalidOperationException("Ya existe un turno en la fecha y hora especificadas.");
            }

            var nuevoturno = new Turno
            {
                Fecha = fechaUtc,
                PacienteId = dto.IdPaciente,
                FichaDeSeguimientoID = dto.IdFichaDeSeguimiento,
                Estado = "Pendiente"

            };
            _context.Turnos.Add(nuevoturno);
            await _context.SaveChangesAsync();
            return nuevoturno;
        }
        
        private async Task<Turno> ObtenerTurnoAsync(int idTurno)
        {
            var turno = await _context.Turnos.FindAsync(idTurno);

            if (turno == null)
                throw new KeyNotFoundException("El turno no existe.");

            return turno;
        }
        //Método para actualizar el estado de un turno
        public async Task<Turno> ActualizarEstadoAsync(int idTurno, string estado)
        {
            var turno = await ObtenerTurnoAsync(idTurno);

            turno.Estado = estado;

            await _context.SaveChangesAsync();
            return turno;
        }

        public async Task<Turno> ActualizarFechaAsync(int idTurno, DateTime nuevaFecha)
        {
            if (nuevaFecha < DateTime.Now)
                throw new ArgumentException("La fecha del turno no puede ser anterior.");

            var conflicto = await _context.Turnos.AnyAsync(t => t.Fecha == nuevaFecha && t.IdTurno != idTurno);
            if (conflicto)
                throw new InvalidOperationException("Ya existe un turno en esa fecha y hora.");

            var turno = await ObtenerTurnoAsync(idTurno);

            turno.Fecha = nuevaFecha;

            await _context.SaveChangesAsync();
            return turno;
        }

        public async Task<Turno> ActualizarFichaAsync(int idTurno, int? fichaId)
        {
            var turno = await ObtenerTurnoAsync(idTurno);

            turno.FichaDeSeguimientoID = fichaId;

            await _context.SaveChangesAsync();
            return turno;
        }

        public async Task<Turno> ActualizarCompletoAsync(int idTurno, ActualizarTurnoCompletoDTO dto)
        {
            var turno = await ObtenerTurnoAsync(idTurno);

            if (dto.Fecha < DateTime.Now)
                throw new ArgumentException("La fecha del turno no puede ser anterior.");

            bool conflicto = await _context.Turnos
                .AnyAsync(t => t.Fecha == dto.Fecha && t.IdTurno != idTurno);

            if (conflicto)
                throw new InvalidOperationException("Ya existe un turno en esa fecha y hora.");

            bool pacienteExiste = await _context.Pacientes
                .AnyAsync(p => p.IdPaciente == dto.PacienteId);

            if (!pacienteExiste)
                throw new InvalidOperationException("El paciente no existe.");

            turno.Fecha = dto.Fecha;
            turno.Estado = dto.Estado;
            turno.PacienteId = dto.PacienteId;
            turno.FichaDeSeguimientoID = dto.FichaDeSeguimientoID;

            await _context.SaveChangesAsync();
            return turno;
        }

        // Metodo para eliminar turnos
        public async Task EliminarAsync(int idTurno)
        {
            var turno = await _context.Turnos
                .FirstOrDefaultAsync(t => t.IdTurno == idTurno);

            if (turno == null)
                throw new KeyNotFoundException("El turno no existe.");

            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();
        }

    }
}
