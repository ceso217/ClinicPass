using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using ClinicPass.BusinessLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ClinicPass.BusinessLayer.Services
{
    public class ProfesionalService
    {
        private readonly ClinicPassContext _context;

        public ProfesionalService(ClinicPassContext context)
        {
            _context = context;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }

        // Crear profesional
        public async Task<ProfesionalDTO?> CrearAsync(ProfesionalCreateDTO dto)
        {
            // Validaciones
            if (await _context.Profesionales.AnyAsync(u => u.Correo == dto.Correo))
                return null;

            var profesional = new Profesional
            {
                NombreCompleto = dto.NombreCompleto,
                Especialidad = dto.Especialidad,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                DNI = dto.DNI,
                Rol = dto.Rol,
                Activo = true,
                PassHash = HashPassword(dto.Password)
            };

            _context.Profesionales.Add(profesional);
            await _context.SaveChangesAsync();

            return new ProfesionalDTO
            {
                IdUsuario = profesional.IdUsuario,
                NombreCompleto = profesional.NombreCompleto,
                Especialidad = profesional.Especialidad,
                Telefono = profesional.Telefono,
                Correo = profesional.Correo,
                DNI = profesional.DNI,
                Activo = profesional.Activo,
                Rol = profesional.Rol
            };
        }

        // Obtener todos
        public async Task<List<ProfesionalDTO>> GetAllAsync()
        {
            return await _context.Profesionales
                .Select(p => new ProfesionalDTO
                {
                    IdUsuario = p.IdUsuario,
                    NombreCompleto = p.NombreCompleto,
                    Especialidad = p.Especialidad,
                    Telefono = p.Telefono,
                    Correo = p.Correo,
                    DNI = p.DNI,
                    Activo = p.Activo,
                    Rol = p.Rol
                })
                .ToListAsync();
        }

        // Obtener por ID
        public async Task<ProfesionalDTO?> GetByIdAsync(int id)
        {
            var p = await _context.Profesionales.FindAsync(id);
            if (p == null) return null;

            return new ProfesionalDTO
            {
                IdUsuario = p.IdUsuario,
                NombreCompleto = p.NombreCompleto,
                Especialidad = p.Especialidad,
                Telefono = p.Telefono,
                Correo = p.Correo,
                DNI = p.DNI,
                Activo = p.Activo,
                Rol = p.Rol
            };
        }

        // Editar
        public async Task<bool> UpdateAsync(int id, ProfesionalUpdateDTO dto)
        {
            var p = await _context.Profesionales.FindAsync(id);
            if (p == null) return false;

            p.NombreCompleto = dto.NombreCompleto;
            p.Especialidad = dto.Especialidad;
            p.Telefono = dto.Telefono;
            p.Correo = dto.Correo;
            p.DNI = dto.DNI;
            p.Activo = dto.Activo;
            p.Rol = dto.Rol;

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar (desactivar)
        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _context.Profesionales.FindAsync(id);
            if (p == null) return false;

            p.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
