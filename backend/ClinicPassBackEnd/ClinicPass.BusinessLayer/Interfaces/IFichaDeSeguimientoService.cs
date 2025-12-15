using ClinicPass.BusinessLayer.DTOs;
namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface IFichaDeSeguimientoService
    {
        Task<FichaDeSeguimientoDTO> CrearFichaAsync(FichaDeSeguimientoCreateDTO dto);
        Task<List<FichaDeSeguimientoDTO>> GetByHistoriaAsync(int idHistoria);
        Task<List<FichaDeSeguimientoDTO>> GetByPacienteAsync(int idPaciente);
    }
}
