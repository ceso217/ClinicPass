using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.BusinessLayer.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly ClinicPassContext _context;

        public PacienteService(ClinicPassContext context)
        {
            _context = context;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<List<PacienteDTO>> GetAll()
        {
            return await _context.Pacientes
                .Select(p => MapToDTO(p))
                .ToListAsync();
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<PacienteDTO?> GetById(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return null;

            return MapToDTO(paciente);
        }

        // =========================
        // GET BY DNI
        // =========================
        public async Task<PacienteDTO?> GetByDni(string dni)
        {
            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.Dni == dni);

            if (paciente == null)
                return null;

            return MapToDTO(paciente);
        }

        // =========================
        // CREATE
        // =========================
        public async Task<PacienteDTO> Create(PacienteCreateDTO dto)
        {
            // Validar DNI único
            bool existe = await _context.Pacientes.AnyAsync(p => p.Dni == dto.Dni);
            if (existe)
                throw new Exception("Ya existe un paciente con ese DNI.");

            var nuevo = new Paciente
            {
                NombreCompleto = dto.NombreCompleto,
                Dni = dto.Dni,
                FechaNacimiento = dto.FechaNacimiento,
                Localidad = dto.Localidad,
                Provincia = dto.Provincia,
                Calle = dto.Calle,
                Telefono = dto.Telefono
            };

            _context.Pacientes.Add(nuevo);
            await _context.SaveChangesAsync();

            return MapToDTO(nuevo);
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<PacienteDTO?> Update(int id, PacienteUpdateDTO dto)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return null;

            // Validar DNI único (excluyendo al mismo paciente)
            bool existeOtro = await _context.Pacientes
                .AnyAsync(p => p.Dni == dto.Dni && p.IdPaciente != id);

            if (existeOtro)
                throw new Exception("El DNI pertenece a otro paciente.");

            paciente.NombreCompleto = dto.NombreCompleto;
            paciente.Dni = dto.Dni;
            paciente.FechaNacimiento = dto.FechaNacimiento;
            paciente.Localidad = dto.Localidad;
            paciente.Provincia = dto.Provincia;
            paciente.Calle = dto.Calle;
            paciente.Telefono = dto.Telefono;

            await _context.SaveChangesAsync();

            return MapToDTO(paciente);
        }

        // =========================
        // DELETE
        // =========================
        public async Task<bool> Delete(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return false;

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // Mapper privado
        // =========================
        private static PacienteDTO MapToDTO(Paciente paciente)
        {
            return new PacienteDTO
            {
                IdPaciente = paciente.IdPaciente,
                NombreCompleto = paciente.NombreCompleto,
                Dni = paciente.Dni,
                FechaNacimiento = paciente.FechaNacimiento,
                Localidad = paciente.Localidad,
                Provincia = paciente.Provincia,
                Calle = paciente.Calle,
                Telefono = paciente.Telefono
            };
        }
    }
}
