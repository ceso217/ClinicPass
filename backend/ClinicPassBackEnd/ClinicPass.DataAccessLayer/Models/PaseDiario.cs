
namespace ClinicPass.DataAccessLayer.Models
{
    public class PaseDiario
    {
        // PK compuesta
        public int IdTratamiento { get; set; }
        public Tratamiento Tratamiento { get; set; } = null!;

        public int IdTurno { get; set; }
        public Turno Turno { get; set; } = null!;

        public string? FrecuenciaTurno { get; set; }
    }
}



