using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("historia/{idHistoria}")]
        public async Task<IActionResult> GetByHistoria(int idHistoria)
        {
            return Ok(await _service.GetByHistoriaAsync(idHistoria));
        }

        [HttpGet("paciente/{idPaciente}")]
        public async Task<IActionResult> GetByPaciente(int idPaciente)
        {
            return Ok(await _service.GetByPacienteAsync(idPaciente));
        }
    }
}
