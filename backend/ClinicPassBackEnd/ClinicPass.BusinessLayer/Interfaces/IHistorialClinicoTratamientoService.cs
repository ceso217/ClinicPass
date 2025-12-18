using ClinicPass.DataAccessLayer.DTOs.HistorialClinicoTratamiento;
using ClinicPass.DataAccessLayer.DTOs.Tratamiento;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface IHistorialClinicoTratamientoService
    {
        Task CrearAsync(HistorialClinicoTratamientoCreateDTO dto);

        Task<IEnumerable<HistorialClinicoTratamientoDTO>> GetAllAsync();

        Task<IEnumerable<HistorialClinicoTratamientoDTO>> GetByHistoriaClinicaAsync(int idHistorialClinico);

        Task<bool> UpdateAsync(
            int idTratamiento,
            int idHistorialClinico,
            HistorialClinicoTratamientoUpdateDTO dto
        );

        Task<bool> FinalizarTratamientoAsync(int idTratamiento, int idHistorialClinico);

        Task<bool> DeleteAsync(int idTratamiento, int idHistorialClinico);

        Task<TratamientoEstadisticaDTO?> GetEstadisticasPorTratamientoAsync(int idTratamiento);

    }
}
