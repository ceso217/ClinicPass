using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TutorController : ControllerBase
{
    private readonly ITutorService _service;

    public TutorController(ITutorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Tutor dto)
        => Ok(await _service.CrearAsync(dto));

    [HttpGet("{dni}")]
    public async Task<IActionResult> Get(int dni)
        => Ok(await _service.GetByDniAsync(dni));
}