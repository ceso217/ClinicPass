using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.Tratamiento;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Services
{
    public class TratamientoService : ITratamientoService
    {
        private readonly ClinicPassContext _context;

        public TratamientoService(ClinicPassContext context)
        {
            _context = context;
        }

        public async Task<TratamientoDTO> CrearAsync(TratamientoCreateDTO dto)
        {
            var tratamiento = new Tratamiento
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Activo = true
            };

            _context.Tratamientos.Add(tratamiento);
            await _context.SaveChangesAsync();

            return new TratamientoDTO
            {
                IdTratamiento = tratamiento.IdTratamiento,
                Nombre = tratamiento.Nombre,
                Descripcion = tratamiento.Descripcion,
                Activo = tratamiento.Activo
            };
        }

        public async Task<List<TratamientoDTO>> GetAllAsync(bool incluirInactivos = false)
        {
            var query = _context.Tratamientos.AsQueryable();

            if (!incluirInactivos)
                query = query.Where(t => t.Activo);

            return await query
                .Select(t => new TratamientoDTO
                {
                    IdTratamiento = t.IdTratamiento,
                    Nombre = t.Nombre,
                    Descripcion = t.Descripcion,
                    Activo = t.Activo
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(int id, TratamientoUpdateDTO dto)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
                return false;

            if (!string.IsNullOrWhiteSpace(dto.Nombre))
                tratamiento.Nombre = dto.Nombre;

            if (dto.Descripcion != null)
                tratamiento.Descripcion = dto.Descripcion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DesactivarAsync(int id)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
                return false;

            tratamiento.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TratamientoDTO?> GetByIdAsync(int id)
        {
            return await _context.Tratamientos
                .Where(t => t.IdTratamiento == id)
                .Select(t => new TratamientoDTO
                {
                    IdTratamiento = t.IdTratamiento,
                    Nombre = t.Nombre,
                    Descripcion = t.Descripcion,
                    Activo = t.Activo
                })
                .FirstOrDefaultAsync();
        }

    }

}



