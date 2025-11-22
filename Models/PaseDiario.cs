namespace ClinicPass.Models
{
    public class PaseDiario
    {
        public int IdTratamiento { get; set; }
        public Tratamiento Tratamiento { get; set; } = null!;

        public int IdTurno { get; set; }
        public Turno Turno { get; set; } = null!;

        public int IdFichaSeguimiento { get; set; }
        public FichaDeSeguimiento FichaDeSeguimiento { get; set; } = null!;

        public string? FrecuenciaTurno { get; set; }
    }
}
