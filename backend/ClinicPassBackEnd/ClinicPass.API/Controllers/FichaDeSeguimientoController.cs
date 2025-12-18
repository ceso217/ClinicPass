using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.DTOs.Ficha;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FichasDeSeguimientoController : ControllerBase
    {
        private readonly IFichaDeSeguimientoService _service;

        public FichasDeSeguimientoController(IFichaDeSeguimientoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearFicha([FromBody] FichaDeSeguimientoCreateDTO dto)
        {
            return Ok(await _service.CrearFichaAsync(dto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("tratamiento/{tratamientoId}")]
        public async Task<IActionResult> GetByTratamiento(int tratamientoId)
        {
            return Ok(await _service.GetByTratamientoAsync(tratamientoId));
        }


        [HttpGet("historia/{idHistoria}")]
        public async Task<IActionResult> GetByHistoria(int idHistoria)
        {
            return Ok(await _service.GetByHistoriaAsync(idHistoria));
        }


        [HttpGet("historia/{idHistoria}/tratamiento/{tratamientoId}")]
        public async Task<IActionResult> GetByTratamientoAndHistoria(int idHistoria,int tratamientoId)
        {
            return Ok(
                await _service.GetByTratamientoAndHistoriaAsync(tratamientoId, idHistoria)
            );
        }


        [HttpGet("paciente/{idPaciente}")]
        public async Task<IActionResult> GetByPaciente(int idPaciente)
        {
            return Ok(await _service.GetByPacienteAsync(idPaciente));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FichaDeSeguimientoUpdateDTO dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();

            return NoContent();
        }

    }
}
