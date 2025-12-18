using ClinicPass.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface IProfesionalService
    {
        Task<IEnumerable<ProfesionalDTO?>> GetAllAsync();
        Task<ProfesionalDTO?> GetByIdAsync(int id);
        Task<ProfesionalDTO?> GetByDniAsync(string dni);
        Task<bool> UpdateAsync(int id, ProfesionalDTO profesionalDTO);
        Task<bool> ToggleActivoAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
