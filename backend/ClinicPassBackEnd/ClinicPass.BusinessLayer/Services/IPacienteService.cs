using ClinicPass.BusinessLayer.DTOs;

namespace ClinicPass.BusinessLayer.Services
{
    public interface IPacienteService
    {
        Task<List<PacienteDTO>> GetAll();
        Task<PacienteDTO?> GetById(int id);
        Task<PacienteDTO> Create(PacienteCreateDTO dto);
        Task<PacienteDTO?> Update(int id, PacienteUpdateDTO dto);
        Task<bool> Delete(int id);
        Task<PacienteDTO?> GetByDni(string dni);
    }
}

