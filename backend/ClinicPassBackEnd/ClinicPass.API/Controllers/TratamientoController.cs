using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TratamientosController : ControllerBase
    {
        private readonly TratamientoService _service;

        public TratamientosController(TratamientoService service)
        {
            _service = service;
        }

        // POST api
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TratamientoCreateDTO dto)
        {
            var result = await _service.CrearTratamientoAsync(dto);

            if (result == null)
                return NotFound("El paciente no existe.");

            return Ok(result);
        }


        // GET api
        [HttpGet("paciente/{idPaciente}")]
        public async Task<IActionResult> GetByPaciente(int idPaciente)
        {
            return Ok(await _service.GetByPacienteAsync(idPaciente));
        }

        // PUT api
        [HttpPut("finalizar/{idPaciente}/{idTratamiento}")]
        public async Task<IActionResult> Finalizar(int idPaciente, int idTratamiento)
        {
            var ok = await _service.FinalizarTratamientoAsync(idPaciente, idTratamiento);

            if (!ok) return NotFound("Tratamiento no encontrado para este paciente.");

            return Ok("Tratamiento finalizado.");
        }
    }
}

