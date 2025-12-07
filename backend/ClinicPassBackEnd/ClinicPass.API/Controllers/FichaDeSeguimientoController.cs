using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FichasDeSeguimientoController : ControllerBase
    {
        private readonly FichaDeSeguimientoService _service;

        public FichasDeSeguimientoController(FichaDeSeguimientoService service)
        {
            _service = service;
        }

        // POST api/fichas
        [HttpPost]
        public async Task<IActionResult> CrearFicha([FromBody] FichaDeSeguimientoCreateDTO dto)
        {
            return Ok(await _service.CrearFichaAsync(dto));
        }

        // GET api/fichas/historia/3
        [HttpGet("historia/{idHistoria}")]
        public async Task<IActionResult> GetByHistoria(int idHistoria)
        {
            return Ok(await _service.GetByHistoriaAsync(idHistoria));
        }

        // GET api/fichas/paciente/7
        [HttpGet("paciente/{idPaciente}")]
        public async Task<IActionResult> GetByPaciente(int idPaciente)
        {
            return Ok(await _service.GetByPacienteAsync(idPaciente));
        }
    }
}

