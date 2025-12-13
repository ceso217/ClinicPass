using ClinicPass.BusinessLayer.DTOs;

namespace ClinicPass.BusinessLayer.Services
{
    public interface IHistoriaClinicaService
    {
        Task<HistoriaClinicaDTO?> GetByIdAsync(int id);
        Task<HistoriaClinicaDTO?> GetHistoriaClinicaAsync(int idPaciente);
        Task<HistoriaClinicaDTO> CreateAsync(int idPaciente, int tipoPaciente);
        Task<bool> UpdateAsync(int idHistorial, HistoriaClinicaUpdateDTO dto);
        Task<bool> DesactivarAsync(int idHistorial);
        Task<bool> ActivarAsync(int idHistorial);

        Task<bool> AgregarTratamientoAsync(int idHistorial, AgregarTratamientoaHistoriaDTO dto);
        Task<bool> QuitarTratamientoAsync(int idHistorial, int idTratamiento);
    }

}

