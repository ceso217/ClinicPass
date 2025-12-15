using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _service;

        public PacienteController(IPacienteService service)
        {
            _service = service;
        }

        // GET api/paciente
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        // GET api/paciente/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await _service.GetById(id);
            if (paciente == null) return NotFound();

            return Ok(paciente);
        }

        // GET api/paciente/dni/123
        [HttpGet("dni/{dni}")]
        public async Task<IActionResult> GetByDni(string dni)
        {
            var paciente = await _service.GetByDni(dni);
            if (paciente == null)
                return NotFound(new { error = "Paciente no encontrado por DNI" });

            return Ok(paciente);
        }

        // POST api/paciente
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PacienteCreateDTO dto)
        {
            try
            {
                var paciente = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = paciente.IdPaciente }, paciente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

            // PUT api/paciente/5
            [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PacienteUpdateDTO dto)
        {
            try
            {
                var actualizado = await _service.Update(id, dto);
                if (actualizado == null)
                    return NotFound(new { error = "Paciente no encontrado" });

                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE api/paciente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.Delete(id);
            if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
