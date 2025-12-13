using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CoberturasController : ControllerBase
{
    private readonly ICoberturaService _service;

    public CoberturasController(ICoberturaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CoberturaMedica dto)
        => Ok(await _service.CrearAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());
}
