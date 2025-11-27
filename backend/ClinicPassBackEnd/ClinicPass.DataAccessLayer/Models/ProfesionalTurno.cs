namespace ClinicPass.Models
{
    public class ProfesionalTurno
    {
        public int IdUsuario { get; set; }
        public Profesional Profesional { get; set; } = null!;

        public int IdTurno { get; set; }
        public Turno Turno { get; set; } = null!;
    }
}
