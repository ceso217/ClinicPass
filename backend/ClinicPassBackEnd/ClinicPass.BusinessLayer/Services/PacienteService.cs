using ClinicPass.BusinessLayer.DTOs;
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

        // GET ALL
        public async Task<List<PacienteDTO>> GetAll()
        {
            return await _context.Pacientes
                .Select(p => new PacienteDTO
                {
                    IdPaciente = p.IdPaciente,
                    NombreCompleto = p.NombreCompleto,
                    Dni = p.Dni,
                    FechaNacimiento = p.FechaNacimiento,
                    Localidad = p.Localidad,
                    Provincia = p.Provincia,
                    Calle = p.Calle,
                    Telefono = p.Telefono
                })
                .ToListAsync();
        }

        // GET BY ID
        public async Task<PacienteDTO?> GetById(int id)
        {
            var p = await _context.Pacientes.FindAsync(id);

            if (p == null)
                return null;

            return new PacienteDTO
            {
                IdPaciente = p.IdPaciente,
                NombreCompleto = p.NombreCompleto,
                Dni = p.Dni,
                FechaNacimiento = p.FechaNacimiento,
                Localidad = p.Localidad,
                Provincia = p.Provincia,
                Calle = p.Calle,
                Telefono = p.Telefono
            };
        }

        // CREATE
        public async Task<PacienteDTO> Create(PacienteCreateDTO dto)
        {
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

            return new PacienteDTO
            {
                IdPaciente = nuevo.IdPaciente,
                NombreCompleto = nuevo.NombreCompleto,
                Dni = nuevo.Dni,
                FechaNacimiento = nuevo.FechaNacimiento,
                Localidad = nuevo.Localidad,
                Provincia = nuevo.Provincia,
                Calle = nuevo.Calle,
                Telefono = nuevo.Telefono
            };
        }

        // UPDATE
        public async Task<PacienteDTO?> Update(int id, PacienteUpdateDTO dto)
        {
            var p = await _context.Pacientes.FindAsync(id);

            if (p == null)
                return null;

            p.NombreCompleto = dto.NombreCompleto;
            p.Dni = dto.Dni;
            p.FechaNacimiento = dto.FechaNacimiento;
            p.Localidad = dto.Localidad;
            p.Provincia = dto.Provincia;
            p.Calle = dto.Calle;
            p.Telefono = dto.Telefono;

            await _context.SaveChangesAsync();

            return new PacienteDTO
            {
                IdPaciente = p.IdPaciente,
                NombreCompleto = p.NombreCompleto,
                Dni = p.Dni,
                FechaNacimiento = p.FechaNacimiento,
                Localidad = p.Localidad,
                Provincia = p.Provincia,
                Calle = p.Calle,
                Telefono = p.Telefono
            };
        }

        // DELETE
        public async Task<bool> Delete(int id)
        {
            var p = await _context.Pacientes.FindAsync(id);

            if (p == null)
                return false;

            _context.Pacientes.Remove(p);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
