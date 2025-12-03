
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
        private readonly ClinicPassContext _context;
        public TurnoService(ClinicPassContext context)
        {
            _context = context;
        }

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
    }
}
