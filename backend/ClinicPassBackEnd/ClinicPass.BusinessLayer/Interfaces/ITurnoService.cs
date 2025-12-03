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
        Task<Turno> CrearTurnoAsync(CrearTurnosDTO dto);
    }
}
