using ClinicPass.DataAccessLayer.Models;

public interface IDocumentoService
{
    Task<Documento> CrearAsync(Documento documento);
    Task<List<Documento>> GetByFichaAsync(int idFicha);
}
