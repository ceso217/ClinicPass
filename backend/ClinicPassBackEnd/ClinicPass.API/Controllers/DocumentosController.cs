using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DocumentosController : ControllerBase
{
    private readonly IDocumentoService _service;

    public DocumentosController(IDocumentoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Documento dto)
        => Ok(await _service.CrearAsync(dto));

    [HttpGet("ficha/{idFicha}")]
    public async Task<IActionResult> GetByFicha(int idFicha)
        => Ok(await _service.GetByFichaAsync(idFicha));
}
