using ClinicPass.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarioController : ControllerBase
    {
        private readonly ICalendarioService _calendarioService;

        public CalendarioController(ICalendarioService calendarioService)
        {
            _calendarioService = calendarioService;
        }

        // 1️⃣ Todos los turnos de todos los profesionales
        [HttpGet("todos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var turnos = await _calendarioService.ObtenerTodos();
            return Ok(turnos);
        }

        // 2️⃣ Todos los profesionales por rango de fecha
        [HttpGet("todos/rango")]
        public async Task<IActionResult> ObtenerTodosPorRango(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            var turnos = await _calendarioService
                .ObtenerPorRangoFecha(fechaInicio, fechaFin);

            return Ok(turnos);
        }

        // 3️⃣ Turnos de un profesional
        [HttpGet("profesional/{idProfesional}")]
        public async Task<IActionResult> ObtenerPorProfesional(int idProfesional)
        {
            var turnos = await _calendarioService
                .ObtenerPorProfesional(idProfesional);

            return Ok(turnos);
        }

        // 4️⃣ Turnos de un profesional por rango
        [HttpGet("profesional/{idProfesional}/rango")]
        public async Task<IActionResult> ObtenerPorProfesionalYRango(
            int idProfesional,
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            var turnos = await _calendarioService
                .ObtenerPorProfesionalYRangoFecha(
                    idProfesional,
                    fechaInicio,
                    fechaFin);

            return Ok(turnos);
        }
    }
}
