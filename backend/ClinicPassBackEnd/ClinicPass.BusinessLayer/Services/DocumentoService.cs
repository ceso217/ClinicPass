using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
public class DocumentoService : IDocumentoService
{
    private readonly ClinicPassContext _context;

    public DocumentoService(ClinicPassContext context)
    {
        _context = context;
    }

    public async Task<Documento> CrearAsync(Documento documento)
    {
        _context.Documentos.Add(documento);
        await _context.SaveChangesAsync();
        return documento;
    }

    public async Task<List<Documento>> GetByFichaAsync(int idFicha)
        => await _context.Documentos
            .Where(d => d.IdFichaSeguimiento == idFicha)
            .ToListAsync();
}