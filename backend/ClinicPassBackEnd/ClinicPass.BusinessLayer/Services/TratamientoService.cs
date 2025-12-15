using ClinicPass.DataAccessLayer.Data;
using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ClinicPass.BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClinicPass.BusinessLayer.Services
{
    public class TratamientoService : ITratamientoService
    {
        private readonly ClinicPassContext _context;

        public TratamientoService(ClinicPassContext context)
        {
            _context = context;
        }
        // CREAR TRATAMIENTO Y ASOCIARLO AL PACIENTE
        public async Task<TratamientoPacienteDTO?> CrearTratamientoAsync(TratamientoCreateDTO dto)
        {
            // Validar que el paciente exista
            var paciente = await _context.Pacientes.FindAsync(dto.IdPaciente);
            if (paciente == null)
                return null;

            //Crear tratamiento base
            var tratamiento = new Tratamiento
            {
                TipoTratamiento = dto.TipoTratamiento,
                Motivo = dto.Motivo,
                Descripcion = dto.Descripcion
            };

            _context.Tratamientos.Add(tratamiento);
            await _context.SaveChangesAsync();

            //crea la relacion entre paciente y tratamiento
            var rel = new PacienteTratamiento
            {
                IdPaciente = dto.IdPaciente,
                IdTratamiento = tratamiento.IdTratamiento,
                FechaInicio = dto.FechaInicio,
                Estado = "Activo"
            };

            _context.PacienteTratamientos.Add(rel);
            await _context.SaveChangesAsync();

            // devuelve
            return new TratamientoPacienteDTO
            {
                IdTratamiento = tratamiento.IdTratamiento,
                TipoTratamiento = tratamiento.TipoTratamiento,
                Motivo = tratamiento.Motivo,
                Descripcion = tratamiento.Descripcion,
                FechaInicio = rel.FechaInicio,
                Estado = rel.Estado,
                FechaFin = rel.FechaFin
            };
        }

        //traer tratamientos por paciente
        public async Task<List<TratamientoPacienteDTO>> GetByPacienteAsync(int idPaciente)
        {
            return await _context.PacienteTratamientos
                .Where(pt => pt.IdPaciente == idPaciente)
                .Include(pt => pt.Tratamiento)
                .Select(pt => new TratamientoPacienteDTO
                {
                    IdTratamiento = pt.IdTratamiento,
                    TipoTratamiento = pt.Tratamiento.TipoTratamiento,
                    Motivo = pt.Tratamiento.Motivo,
                    Descripcion = pt.Tratamiento.Descripcion,
                    FechaInicio = pt.FechaInicio,
                    Estado = pt.Estado,
                    FechaFin = pt.FechaFin
                })
                .ToListAsync();
        }

        //fin
        public async Task<bool> FinalizarTratamientoAsync(int idPaciente, int idTratamiento)
        {
            var rel = await _context.PacienteTratamientos
                .FirstOrDefaultAsync(pt => pt.IdPaciente == idPaciente && pt.IdTratamiento == idTratamiento);

            if (rel == null)
                return false;

            rel.Estado = "Finalizado";
            rel.FechaFin = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}


