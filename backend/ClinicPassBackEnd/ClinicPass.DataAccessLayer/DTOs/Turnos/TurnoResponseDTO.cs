namespace ClinicPass.DataAccessLayer.DTOs.Turnos
{
    public class TurnoResponseDTO
    {
        public int IdTurno { get; set; }
        public DateTime Fecha { get; set; }
        public string? Estado { get; set; }

        public int IdPaciente { get; set; }
        public string NombrePaciente { get; set; } = null!;

        public int? IdFichaSeguimiento { get; set; }
    }
}
