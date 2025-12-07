using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicPass.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ClinicPass.DataAccessLayer.DTOs;

namespace ClinicPass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] //Este controller solo esta disponible para aquellos usuarios con el rol de ADMIN
    public class ProfesionalsController : ControllerBase
    {
        //Contexto y Administrador de usuarios
        private readonly ClinicPassContext _context;
        private readonly UserManager<Profesional> _userManager;

        public ProfesionalsController(ClinicPassContext context, UserManager<Profesional> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Profesionals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesional>>> GetAllProfesionals()
        {
            var profesionales = _userManager.Users.ToList();

            return Ok(profesionales);
        }

        // GET: api/Profesionals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesional>> GetProfesional(int id)
        {
            var profesional = await _userManager.FindByIdAsync(id.ToString());

            if (profesional == null)
            {
                return NotFound();
            }

            return Ok(profesional);// se devuelve un solo profesional
        }

        // PUT: api/Profesionals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesional(int id,[FromBody] Profesional profesionalUpdated)
        {
            if (id != profesionalUpdated.Id)
            {
                return BadRequest();
            }
            var profesional = await _userManager.FindByIdAsync(id.ToString());


            profesional.NombreCompleto = profesionalUpdated.NombreCompleto;
            profesional.Dni = profesionalUpdated.Dni;
            profesional.PhoneNumber = profesionalUpdated?.PhoneNumber;
            profesional.Especialidad = profesionalUpdated.Especialidad;
            profesional.Activo = profesionalUpdated.Activo;

            var result = await _userManager.UpdateAsync(profesional);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok($"Se actualizo el usuario {id} : {profesional}");
        }

        // POST: api/Profesionals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profesional>> PostProfesional([FromBody]RegisterDTO request)
        {
            var profesional = new Profesional
            {
                UserName = request.Email,
                Email = request.Email,
                NombreCompleto = $"{request.Name} {request.LastName}",
                Dni = request.Dni,
                PhoneNumber = request.Telefono,
                Activo = true
            };

            var resultado = await _userManager.CreateAsync(profesional, request.Password);
            if (!resultado.Succeeded)
            {
                return BadRequest(resultado.Errors);

            }

            //se asigna el rol de profesional
            await _userManager.AddToRoleAsync(profesional, "Profesional");

            return Ok("Usuario correctamente creado: " + profesional);
        }

        // DELETE: api/Profesionals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesional(int id)
        {
            var profesional = await _userManager.FindByIdAsync(id.ToString());
            if (profesional == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(profesional);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok($"Profesional {id} correctamente eliminado.");
        }

        // PUT api/Profesionals/5/ResetPassword
        [HttpPut("{id}")]

        //TODO


        private bool ProfesionalExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
