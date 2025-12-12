namespace ClinicPass.DataAccessLayer.Models
{
    public class ProfesionalTurno
    {
        // PK compuesta
        public int IdUsuario { get; set; }
        public Profesional Profesional { get; set; } = null!;

        public int IdTurno { get; set; }
        public Turno Turno { get; set; } = null!;
    }
}

