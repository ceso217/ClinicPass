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
        //segun a paciente create dto voy a crear un paciente, validar dni unico
        public async Task<PacienteDTO> Create(PacienteCreateDTO dto)
        {
            // Validar DNI único
           bool existe = await _context.Pacientes.AnyAsync(p => p.Dni == dto.Dni);
            if (existe)
                throw new Exception("Ya existe un paciente con ese DNI.");


            var nuevo = new Paciente
            {
                NombreCompleto = dto.NombreCompleto,
                Dni = dto.Dni
                // No enviar más nada
            };

            _context.Pacientes.Add(nuevo);
            await _context.SaveChangesAsync();

            return new PacienteDTO
            {
                IdPaciente = nuevo.IdPaciente,
                NombreCompleto = nuevo.NombreCompleto,
                Dni = nuevo.Dni
            };
        }


        // UPDATE
        public async Task<PacienteDTO?> Update(int id, PacienteUpdateDTO dto)
        {
            var p = await _context.Pacientes.FindAsync(id);

            // Validar DNI único al actualizar
            bool existeOtro = await _context.Pacientes
                .AnyAsync(p => p.Dni == dto.Dni && p.IdPaciente != id);

            if (existeOtro){
                throw new Exception("El DNI pertenece a otro paciente.");
            }


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

        public async Task<PacienteDTO?> GetByDni(string dni) //quiero buscar al paciente por dni
        {
            var p = await _context.Pacientes
                .FirstOrDefaultAsync(x => x.Dni == dni);

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


    }
}
