using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Services
{
    public class ProfesionalService : IProfesionalService
    {
        private readonly ClinicPassContext _db;
        private readonly UserManager<Profesional> _userManager;

        public ProfesionalService(ClinicPassContext db, UserManager<Profesional> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // Get all profesionales
        public async Task<IEnumerable<ProfesionalDTO?>> GetAllAsync()
        {
            var profesionales = await _userManager.Users.Select(p=>MapToDTO(p)).ToListAsync();
            return profesionales;
        }

        // Get profesional by Id
        public async Task<ProfesionalDTO?> GetByIdAsync(int id)
        {
            var profesional = await _userManager.FindByIdAsync(id.ToString());
            if (profesional == null)
            {
                return null;
            }
            return MapToDTO(profesional);
        }

        // Get profesional by Dni
        public async Task<ProfesionalDTO?> GetByDniAsync(string dni)
        {
            var profesional = await _userManager.Users.FirstOrDefaultAsync(p => p.Dni == dni);
            if (profesional == null)
            {
                return null;
            }
            return MapToDTO(profesional);
        }

        // Patch profesional

        public async Task<bool> UpdateAsync(int id, ProfesionalDTO profesionalDTO)
        {
            var profesional = await _userManager.FindByIdAsync(id.ToString());
            if (profesional == null)
            {
                return false;
            }

            var existingDniUser = await _userManager.Users.FirstOrDefaultAsync(p => p.Dni == profesionalDTO.Dni && p.Id != id);
            if (existingDniUser != null)
            {
                // DNI already in use by another user
                return false;
            }

            // esto al final lo voy a poner en authcontroller como un endpoint aparte para cambiar email

            //var existingEmailUser = await _userManager.Users.FirstOrDefaultAsync(p => p.NormalizedEmail == profesionalDTO.NormalizedEmail && p.Id != id);
            //if (existingEmailUser != null)
            //{
            //    // Email already in use by another user
            //    return false;
            //}

            profesional.NombreCompleto = profesionalDTO.NombreCompleto;
            //profesional.Email = profesionalDTO.Email;
            //profesional.UserName = profesionalDTO.Email; // Keep username in sync with email
            profesional.PhoneNumber = profesionalDTO.PhoneNumber;
            profesional.Activo = profesionalDTO.Activo;
            profesional.Dni = profesionalDTO.Dni;
            profesional.Especialidad = profesionalDTO.Especialidad;
            var result = await _userManager.UpdateAsync(profesional);
            return result.Succeeded;
        }

        // Boton dar de baja dar de alta
        public async Task<bool> ToggleActivoAsync(int id)
        {
            var profesional = await _userManager.FindByIdAsync(id.ToString());
            if (profesional == null)
            {
                return false;
            }
            profesional.Activo = !profesional.Activo;
            var result = await _userManager.UpdateAsync(profesional);
            return result.Succeeded;
        }

        // Delete profesional
        public async Task<bool> DeleteAsync(int id)
        {
            var profesional = await _userManager.FindByIdAsync(id.ToString());
            if (profesional == null)
            {
                return false;
            }
            var result = await _userManager.DeleteAsync(profesional);
            return result.Succeeded;
        }

        // Map Profesional to ProfesionalDTO
        private static ProfesionalDTO MapToDTO(Profesional p)
        {
            return new ProfesionalDTO
            {
                UserName = p.UserName,
                Email = p.Email,
                NombreCompleto = p.NombreCompleto,
                PhoneNumber = p.PhoneNumber,
                Activo = p.Activo,
                Dni = p.Dni,
                Especialidad = p.Especialidad,
                Id = p.Id,
            };
        }
    }
}
