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

        // Relación 1 Historia → N Tratamientos
        public List<FichaDeSeguimiento> Fichas { get; set; } = new List<FichaDeSeguimiento>();
        public ICollection<HistorialClinicoTratamiento> Tratamientos { get; set; } = new List<HistorialClinicoTratamiento>();


    }
}

