//using ClinicPass.DataAccessLayer.Data;
//using ClinicPass.DataAccessLayer.Models;
//using ClinicPass.BusinessLayer.DTOs;
//using Microsoft.EntityFrameworkCore;

//namespace ClinicPass.BusinessLayer.Services
//{
//    public class ProfesionalTurnoService
//    {
//        private readonly ClinicPassContext _context;

//        public ProfesionalTurnoService(ClinicPassContext context)
//        {
//            _context = context;
//        }

//        // Asignar un profesional a un turno
//        public async Task<bool> AsignarAsync(AsignarProfesionalDTO dto)
//        {
//            // Evitar duplicados
//            bool yaExiste = await _context.ProfesionalTurnos
//                .AnyAsync(pt => pt.IdUsuario == dto.IdUsuario && pt.IdTurno == dto.IdTurno);

//            if (yaExiste) return false;

//            var asignacion = new ProfesionalTurno
//            {
//                IdUsuario = dto.IdUsuario,
//                IdTurno = dto.IdTurno
//            };

//            _context.ProfesionalTurnos.Add(asignacion);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        // Quitar un profesional de un turno
//        public async Task<bool> QuitarAsync(int idUsuario, int idTurno)
//        {
//            var pt = await _context.ProfesionalTurnos
//                .FirstOrDefaultAsync(x => x.IdUsuario == idUsuario && x.IdTurno == idTurno);

//            if (pt == null) return false;

//            _context.ProfesionalTurnos.Remove(pt);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        // Listar profesionales de un turno
//        public async Task<List<ProfesionalTurnoDTO>> GetProfesionalesPorTurno(int idTurno)
//        {
//            return await _context.ProfesionalTurnos
//                .Where(pt => pt.IdTurno == idTurno)
//                .Include(pt => pt.Profesional)
//                .Include(pt => pt.Turno)
//                .Select(pt => new ProfesionalTurnoDTO
//                {
//                    IdUsuario = pt.IdUsuario,
//                    IdTurno = pt.IdTurno,
//                    NombreProfesional = pt.Profesional.NombreCompleto,
//                    FechaTurno = pt.Turno.FechaHora
//                })
//                .ToListAsync();
//        }

//        // Listar turnos de un profesional
//        public async Task<List<ProfesionalTurnoDTO>> GetTurnosPorProfesional(int idUsuario)
//        {
//            return await _context.ProfesionalTurnos
//                .Where(pt => pt.IdUsuario == idUsuario)
//                .Include(pt => pt.Profesional)
//                .Include(pt => pt.Turno)
//                .Select(pt => new ProfesionalTurnoDTO
//                {
//                    IdUsuario = pt.IdUsuario,
//                    IdTurno = pt.IdTurno,
//                    NombreProfesional = pt.Profesional.NombreCompleto,
//                    FechaTurno = pt.Turno.FechaHora
//                })
//                .ToListAsync();
//        }
//    }
//}
