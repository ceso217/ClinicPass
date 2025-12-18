using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.DTOs.HistoriaClinica;

namespace ClinicPass.BusinessLayer.Services
{
    public interface IHistoriaClinicaService
    {
        Task<HistoriaClinicaDTO?> GetByIdAsync(int id);
        Task<HistoriaClinicaDTO?> GetHistoriaClinicaAsync(int idPaciente);
        Task<HistoriaClinicaDTO> CreateAsync(HistoriaClinicaCreateDTO dto);
        Task<bool> UpdateAsync(int idHistorial, HistoriaClinicaUpdateDTO dto);
        Task<bool> DesactivarAsync(int idHistorial);
        Task<bool> ActivarAsync(int idHistorial);

        Task<List<HistoriaClinicaListadoDTO>> GetAllAsync();
        Task<HistoriaClinicaDetalleDTO?> GetDetalleByIdAsync(int id);
        Task<HistoriaClinicaOrdenadaDTO?> GetOrdenadaByIdAsync(int id);

    }

}

