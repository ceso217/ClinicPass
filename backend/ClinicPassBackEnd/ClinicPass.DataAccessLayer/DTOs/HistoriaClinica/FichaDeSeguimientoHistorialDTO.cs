using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.DataAccessLayer.DTOs.HistoriaClinica
{
    public class FichaDeSeguimientoHistorialDTO
    {
        public int IdFichaSeguimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Observaciones { get; set; }

        public int? TratamientoId { get; set; }

        // 👇 NUEVO
        public int IdUsuario { get; set; }
        public string? NombreProfesional { get; set; }
    }

}
