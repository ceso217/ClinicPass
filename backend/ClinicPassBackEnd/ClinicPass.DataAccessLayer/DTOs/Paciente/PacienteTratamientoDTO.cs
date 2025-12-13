
namespace ClinicPass.BusinessLayer.DTOs
{
    public class PacienteTratamientoDTO
    {
        public int IdPaciente { get; set; }
        public int IdTratamiento { get; set; }

        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime FechaFin { get; set; }
    }
}
