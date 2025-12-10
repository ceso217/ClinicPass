//using ClinicPass.BusinessLayer.DTOs;
//using ClinicPass.BusinessLayer.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace ClinicPass.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class PasesController : ControllerBase
//    {
//        private readonly PaseService _service;

//        public PasesController(PaseService service)
//        {
//            _service = service;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Crear([FromBody] PaseCreateDTO dto)
//        {
//            return Ok(await _service.CrearAsync(dto));
//        }

//        [HttpGet("tratamiento/{idTratamiento}")]
//        public async Task<IActionResult> GetByTratamiento(int idTratamiento)
//        {
//            return Ok(await _service.GetByTratamiento(idTratamiento));
//        }
//    }
//}

