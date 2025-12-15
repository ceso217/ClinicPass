using ClinicPass.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Interfaces
{
    public interface ICoberturaService
    {
        Task<CoberturaMedica> CrearAsync(CoberturaMedica cobertura);
        Task<List<CoberturaMedica>> GetAllAsync();
    }
}
