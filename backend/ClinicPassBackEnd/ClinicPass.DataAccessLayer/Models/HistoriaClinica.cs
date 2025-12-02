using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class HistoriaClinica
    {
        [Key]
        public int IdHistorialClinico { get; set; }

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public int TipoPaciente { get; set; }
        public string? AntecedentesFamiliares { get; set; }
        public string? AntecedentesPersonales { get; set; }
        public bool Activa { get; set; } = true;

        public ICollection<HCTratamiento> HCTratamientos { get; set; } = new List<HCTratamiento>();
        public ICollection<FichaDeSeguimiento> Fichas { get; set; } = new List<FichaDeSeguimiento>();
    }
}
