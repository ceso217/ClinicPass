using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.DTOs.Turnos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicPass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {

        private readonly ITurnoService _turnoService;

        public TurnosController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        // prueba
        [HttpGet("prueba")]
        public string prueba()
        {
            return "hola-api";
        }
        // GET: api/<TurnosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        

        // GET api/<TurnosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "values";
        }

        //// POST api/<TurnosController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearTurnosDTO dto)
        {
            try
            {
                var turnoCreado = await _turnoService.CrearTurnoAsync(dto);
                return Ok(turnoCreado);  // o Created(), si querés
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message, detalle = ex.InnerException?.Message });
            }
        }

        //// PUT api/<TurnosController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //PUT
        [HttpPut("{idTurno}/estado")]
        public async Task<IActionResult> ActualizarEstado(int idTurno, [FromBody] ActualizarEstadoTurnoDTO dto)
        {
            var turno = await _turnoService.ActualizarEstadoAsync(idTurno, dto.Estado);
            return Ok(turno);
        }

        [HttpPut("{idTurno}/fecha")]
        public async Task<IActionResult> ActualizarFecha(int idTurno, [FromBody] ActualizarFechaTurnoDTO dto)
        {
            var turno = await _turnoService.ActualizarFechaAsync(idTurno, dto.Fecha);
            return Ok(turno);
        }

        [HttpPut("{idTurno}/ficha")]
        public async Task<IActionResult> ActualizarFicha(int idTurno, [FromBody] ActualizarFichaDTO dto)
        {
            var turno = await _turnoService.ActualizarFichaAsync(idTurno, dto.FichaDeSeguimientoID);
            return Ok(turno);
        }

        [HttpPut("{idTurno}")]
        public async Task<IActionResult> ActualizarCompleto(int idTurno, [FromBody] ActualizarTurnoCompletoDTO dto)
        {
            var turno = await _turnoService.ActualizarCompletoAsync(idTurno, dto);
            return Ok(turno);
        }


        // DELETE api/<TurnosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
