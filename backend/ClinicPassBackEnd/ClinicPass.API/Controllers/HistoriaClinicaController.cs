using ClinicPass.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoriaClinicaController : ControllerBase
    {
        private readonly HistoriaClinicaService _service;

        public HistoriaClinicaController(HistoriaClinicaService service)
        {
            _service = service;
        }

        // GET api/historiaclinic
        [HttpGet("paciente/{idPaciente}")]
        public async Task<IActionResult> GetByPaciente(int idPaciente)
        {
            var historia = await _service.GetHistoriaClinicaAsync(idPaciente);

            if (historia == null)
                return NotFound("El paciente no tiene historia clínica registrada.");

            return Ok(historia);
        }
    }
}
