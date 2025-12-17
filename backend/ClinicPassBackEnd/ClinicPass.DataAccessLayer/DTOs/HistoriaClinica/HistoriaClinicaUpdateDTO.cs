using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.DTOs
{
    public class HistoriaClinicaUpdateDTO
    {
        public string? AntecedentesFamiliares { get; set; }
        public string? AntecedentesPersonales { get; set; }
        public bool Activa { get; set; }
    }
}

