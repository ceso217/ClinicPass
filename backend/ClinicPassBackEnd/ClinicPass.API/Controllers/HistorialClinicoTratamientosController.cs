using ClinicPass.BusinessLayer.Services;
using ClinicPass.DataAccessLayer.DTOs.HistorialClinicoTratamiento;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicPass.BusinessLayer.Interfaces;
using System.Threading.Tasks;


namespace ClinicPass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialClinicoTratamientosController : ControllerBase
    {
        private readonly IHistorialClinicoTratamientoService _service;

        public HistorialClinicoTratamientosController(IHistorialClinicoTratamientoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] HistorialClinicoTratamientoCreateDTO dto)
        {
            await _service.CrearAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("historia/{idHistorialClinico}")]
        public async Task<IActionResult> GetByHistoriaClinica(int idHistorialClinico)
        {
            return Ok(await _service.GetByHistoriaClinicaAsync(idHistorialClinico));
        }


        [HttpGet("tratamiento/{idTratamiento}/estadisticas")]
        public async Task<IActionResult> GetEstadisticasPorTratamiento(int idTratamiento)
        {
            var data = await _service.GetEstadisticasPorTratamientoAsync(idTratamiento);
            if (data == null) return NotFound();

            return Ok(data);
        }


        [HttpPut("{idTratamiento}/{idHistorialClinico}")]
        public async Task<IActionResult> Update(
            int idTratamiento,
            int idHistorialClinico,
            [FromBody] HistorialClinicoTratamientoUpdateDTO dto)
        {
            var ok = await _service.UpdateAsync(idTratamiento, idHistorialClinico, dto);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpPut("{idTratamiento}/{idHistorialClinico}/finalizar")]
        public async Task<IActionResult> Finalizar(int idTratamiento, int idHistorialClinico)
        {
            var ok = await _service.FinalizarTratamientoAsync(idTratamiento, idHistorialClinico);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{idTratamiento}/{idHistorialClinico}")]
        public async Task<IActionResult> Delete(int idTratamiento, int idHistorialClinico)
        {
            var ok = await _service.DeleteAsync(idTratamiento, idHistorialClinico);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}
