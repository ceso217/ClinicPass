using ClinicPass.DataAccessLayer.DTOs.Turnos;
using ClinicPass.DataAccessLayer.Models;

namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface ITurnoService
    {
        Task<IEnumerable<TurnoResponseDTO>> ObtenerTodosAsync();
        Task<TurnoResponseDTO> ObtenerPorIdAsync(int idTurno);
        Task<TurnoResponseDTO> CrearTurnoAsync(TurnoDTO dto);
        Task<IEnumerable<TurnoResponseDTO>> ObtenerPorPacienteAsync(int idPaciente);
        Task<Turno> ActualizarEstadoAsync(int idTurno, string estado);
        Task<Turno> ActualizarFechaAsync(int idTurno, ActualizarTurnoDTO dto);
        Task<Turno> ActualizarFichaAsync(int idTurno, int? fichaId);
        Task<TurnoResponseDTO> ActualizarCompletoAsync(int idTurno, ActualizarTurnoDTO dto);

        Task EliminarAsync(int idTurno);
    }
}