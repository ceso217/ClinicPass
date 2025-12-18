using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.DTOs.Tratamiento;

namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface ITratamientoService
    {
        Task<TratamientoDTO> CrearAsync(TratamientoCreateDTO dto);
        Task<List<TratamientoDTO>> GetAllAsync(bool incluirInactivos = false);
        Task<bool> UpdateAsync(int id, TratamientoUpdateDTO dto);
        Task<bool> DesactivarAsync(int id);
        Task<TratamientoDTO?> GetByIdAsync(int id);

    }
}
