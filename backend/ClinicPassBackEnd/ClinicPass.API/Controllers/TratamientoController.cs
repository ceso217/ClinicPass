using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.BusinessLayer.Services;
using ClinicPass.DataAccessLayer.DTOs.Tratamiento;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPass.API.Controllers
{
    [ApiController]
    [Route("api/tratamientos")]
    public class TratamientosController : ControllerBase
    {
        private readonly ITratamientoService _service;

        public TratamientosController(ITratamientoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TratamientoCreateDTO dto)
        {
            return Ok(await _service.CrearAsync(dto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool incluirInactivos = false)
        {
            return Ok(await _service.GetAllAsync(incluirInactivos));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TratamientoUpdateDTO dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok)
                return NotFound();

            return Ok("Tratamiento actualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var ok = await _service.DesactivarAsync(id);
            if (!ok)
                return NotFound();

            return Ok("Tratamiento desactivado");
        }
    }

}

