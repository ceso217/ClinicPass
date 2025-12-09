using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicPass.DataAccessLayer.DTOs.Turnos;
using ClinicPass.DataAccessLayer.Models;

namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface ITurnoService
    {
        //GET
        Task<IEnumerable<TurnoResponseDTO>> ObtenerTodosAsync();
        Task<TurnoResponseDTO> ObtenerPorIdAsync(int idTurno);


        //POST
        Task<Turno> CrearTurnoAsync(CrearTurnosDTO dto);


        //PUT
        Task<Turno> ActualizarEstadoAsync(int idTurno, string estado);
        Task<Turno> ActualizarFechaAsync(int idTurno, DateTime nuevaFecha);
        Task<Turno> ActualizarFichaAsync(int idTurno, int? fichaId);
        Task<Turno> ActualizarCompletoAsync(int idTurno, ActualizarTurnoCompletoDTO dto);

        //DELETE
        Task EliminarAsync(int idTurno);

    }
}
