//using ClinicPass.DataAccessLayer.Data;
//using ClinicPass.DataAccessLayer.Models;
//using ClinicPass.BusinessLayer.DTOs;
//using Microsoft.EntityFrameworkCore;

//namespace ClinicPass.BusinessLayer.Services
//{
//    public class TurnoService
//    {
//        private readonly ClinicPassContext _context;

//        public TurnoService(ClinicPassContext context)
//        {
//            _context = context;
//        }

//        // Crear turno con validación
//        public async Task<TurnoDTO?> CrearAsync(TurnoCreateDTO dto)
//        {
//            // Validación: evitar 2 turnos el mismo día/hora para el mismo paciente
//            if (await _context.Turnos.AnyAsync(t =>
//                t.IdPaciente == dto.IdPaciente &&
//                t.FechaHora == dto.FechaHora))
//            {
//                return null;
//            }

//            var turno = new Turno
//            {
//                IdPaciente = dto.IdPaciente,
//                IdFichaSeguimiento = dto.IdFichaSeguimiento,
//                FechaHora = dto.FechaHora,
//                Estado = "Pendiente",
//                Frecuencia = dto.Frecuencia
//            };

//            _context.Turnos.Add(turno);
//            await _context.SaveChangesAsync();

//            return new TurnoDTO
//            {
//                IdTurno = turno.IdTurno,
//                IdPaciente = turno.IdPaciente,
//                IdFichaSeguimiento = turno.IdFichaSeguimiento,
//                FechaHora = turno.FechaHora,
//                Estado = turno.Estado,
//                Frecuencia = turno.Frecuencia
//            };
//        }

//        // Listar todos
//        public async Task<List<TurnoDTO>> GetAllAsync()
//        {
//            return await _context.Turnos
//                .Select(t => new TurnoDTO
//                {
//                    IdTurno = t.IdTurno,
//                    IdPaciente = t.IdPaciente,
//                    IdFichaSeguimiento = t.IdFichaSeguimiento,
//                    FechaHora = t.FechaHora,
//                    Estado = t.Estado,
//                    Frecuencia = t.Frecuencia
//                })
//                .ToListAsync();
//        }

//        // Obtener por ID
//        public async Task<TurnoDTO?> GetAsync(int id)
//        {
//            var t = await _context.Turnos.FindAsync(id);
//            if (t == null) return null;

//            return new TurnoDTO
//            {
//                IdTurno = t.IdTurno,
//                IdPaciente = t.IdPaciente,
//                IdFichaSeguimiento = t.IdFichaSeguimiento,
//                FechaHora = t.FechaHora,
//                Estado = t.Estado,
//                Frecuencia = t.Frecuencia
//            };
//        }

//        // Editar
//        public async Task<bool> UpdateAsync(int id, TurnoUpdateDTO dto)
//        {
//            var t = await _context.Turnos.FindAsync(id);
//            if (t == null) return false;

//            t.FechaHora = dto.FechaHora;
//            t.Frecuencia = dto.Frecuencia;
//            t.Estado = dto.Estado;

//            await _context.SaveChangesAsync();
//            return true;
//        }

//        // Borrar
//        public async Task<bool> DeleteAsync(int id)
//        {
//            var t = await _context.Turnos.FindAsync(id);
//            if (t == null) return false;

//            _context.Turnos.Remove(t);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//    }
//}

