using ClinicPass.BusinessLayer.DTOs;

namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface ITratamientoService
    {
        Task<TratamientoPacienteDTO?> CrearTratamientoAsync(TratamientoCreateDTO dto);
        Task<List<TratamientoPacienteDTO>> GetByPacienteAsync(int idPaciente);
        Task<bool> FinalizarTratamientoAsync(int idPaciente, int idTratamiento);
    }
}
