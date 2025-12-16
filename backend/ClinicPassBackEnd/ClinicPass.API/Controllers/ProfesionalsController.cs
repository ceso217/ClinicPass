using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ClinicPass.DataAccessLayer.DTOs;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.BusinessLayer.Interfaces;
using NuGet.Protocol;

namespace ClinicPass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] //Este controller solo esta disponible para aquellos usuarios con el rol de ADMIN
    public class ProfesionalsController : ControllerBase
    {
        //Contexto y Administrador de usuarios
        private readonly IProfesionalService _profesionalService;

        public ProfesionalsController(IProfesionalService profesionalService)
        {
            _profesionalService = profesionalService;
        }

        // GET: api/Profesionals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesionalDTO>>> GetAllProfesionals()
        {
            var profesionales = await _profesionalService.GetAllAsync();

            return Ok(profesionales);
        }

        // GET: api/Profesionals/id/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<ProfesionalDTO>> GetProfesionalId(int id)
        {
            var profesional = await _profesionalService.GetByIdAsync(id);

            if (profesional == null)
            {
                return NotFound();
            }

            return Ok(profesional);
        }

        // GET: api/Profesionals/dni/41765123
        [HttpGet("dni/{dni}")]
        public async Task<ActionResult<ProfesionalDTO>> GetProfesionalDni(string dni)
        {
            var profesional = await _profesionalService.GetByDniAsync(dni);
            if (profesional == null)
            {
                return NotFound();
            }
            return Ok(profesional);
        }

        // PUT: api/Profesionals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesional(int id,[FromBody] ProfesionalDTO profesionalUpdated)
        {
            var updated = await _profesionalService.UpdateAsync(id, profesionalUpdated);

            if (!updated)
            {
                return BadRequest("No se pudo actualizar el profesional.");
            }
			var successResponse = new SuccessMessageDTO
			{
				Message = $"Se actualizó el profesional {id}: {profesionalUpdated.NombreCompleto}."
			};
			return Ok(successResponse);
        }

        // POST: api/Profesionals
        // Ya existe endpoint para crear profesional en AuthController/Register
        //[HttpPost]
        //public async Task<ActionResult<Profesional>> PostProfesional([FromBody]RegisterDTO request)
        //{
        //    var profesional = new Profesional
        //    {
        //        UserName = request.Email,
        //        Email = request.Email,
        //        NombreCompleto = $"{request.Name} {request.LastName}",
        //        Dni = request.Dni,
        //        PhoneNumber = request.PhoneNumber,
        //        Activo = true
        //    };

        //    var resultado = await _userManager.CreateAsync(profesional, request.Password);
        //    if (!resultado.Succeeded)
        //    {
        //        return BadRequest(resultado.Errors);

        //    }

        //    //se asigna el rol de profesional
        //    await _userManager.AddToRoleAsync(profesional, "Profesional");

        //    return Ok("Usuario correctamente creado: " + profesional);
        //}

        // DELETE: api/Profesionals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesional(int id)
        {
            var result = await _profesionalService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok($"Profesional {id} correctamente eliminado.");
        }

        // PUT api/Profesionals/5/ResetPassword
        //[HttpPut("{id}")]
    }
}
