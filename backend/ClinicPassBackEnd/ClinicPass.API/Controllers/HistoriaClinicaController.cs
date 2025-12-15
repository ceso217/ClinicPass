using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoriaClinicaController : ControllerBase
    {
        private readonly IHistoriaClinicaService _service;

        public HistoriaClinicaController(IHistoriaClinicaService service)
        {
            _service = service;
        }

        // =========================
        // GET api/historiaclinica/5
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var historia = await _service.GetByIdAsync(id);
            if (historia == null)
                return NotFound();

            return Ok(historia);
        }

        // =========================
        // GET api/historiaclinica/paciente/3
        // =========================
        [HttpGet("paciente/{idPaciente}")]
        public async Task<IActionResult> GetByPaciente(int idPaciente)
        {
            var historia = await _service.GetHistoriaClinicaAsync(idPaciente);

            if (historia == null)
                return NotFound("El paciente no tiene historia clínica registrada.");

            return Ok(historia);
        }

        // =========================
        // POST api/historiaclinica
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HistoriaClinicaCreateDTO dto)
        {
            try
            {
                var historia = await _service.CreateAsync(dto.IdPaciente, dto.TipoPaciente);
                return Ok(historia);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // =========================
        // PUT api/historiaclinica/5
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HistoriaClinicaUpdateDTO dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok)
                return NotFound("Historia clínica no encontrada.");

            return Ok("Historia clínica actualizada.");
        }

        // =========================
        // PUT api/historiaclinica/5/desactivar
        // =========================
        [HttpPut("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var ok = await _service.DesactivarAsync(id);
            if (!ok)
                return NotFound();

            return Ok("Historia clínica desactivada.");
        }

        // =========================
        // PUT api/historiaclinica/5/activar
        // =========================
        [HttpPut("{id}/activar")]
        public async Task<IActionResult> Activar(int id)
        {
            var ok = await _service.ActivarAsync(id);
            if (!ok)
                return NotFound();

            return Ok("Historia clínica activada.");
        }

        // =========================
        // POST api/historiaclinica/5/tratamientos
        // =========================
        [HttpPost("{id}/tratamientos")]
        public async Task<IActionResult> AgregarTratamiento(
            int id,
            [FromBody] AgregarTratamientoaHistoriaDTO dto)
        {
            var ok = await _service.AgregarTratamientoAsync(id, dto);
            if (!ok)
                return NotFound("Historia clínica o tratamiento no encontrado.");

            return Ok("Tratamiento agregado a la historia clínica.");
        }

        // =========================
        // DELETE api/historiaclinica/5/tratamientos/7
        // =========================
        [HttpDelete("{id}/tratamientos/{idTratamiento}")]
        public async Task<IActionResult> QuitarTratamiento(int id, int idTratamiento)
        {
            var ok = await _service.QuitarTratamientoAsync(id, idTratamiento);
            if (!ok)
                return NotFound();

            return Ok("Tratamiento quitado de la historia clínica.");
        }
    }
}