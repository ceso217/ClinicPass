using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.DTOs.Ficha;
namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface IFichaDeSeguimientoService
    {
        Task<FichaDeSeguimientoDTO> CrearFichaAsync(FichaDeSeguimientoCreateDTO dto);
        Task<List<FichaDeSeguimientoDTO>> GetByHistoriaAsync(int idHistoria);
        Task<List<FichaDeSeguimientoDTO>> GetByPacienteAsync(int idPaciente);
        Task<bool> UpdateAsync(int idFicha, FichaDeSeguimientoUpdateDTO dto);
        Task<List<FichaDeSeguimientoDTO>> GetAllAsync();


    }
}
