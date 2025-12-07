namespace ClinicPass.BusinessLayer.DTOs
{
    public class TratamientoDTO
    {
        public int IdTratamiento { get; set; }
        public string TipoTratamiento { get; set; } = null!;
        public string Motivo { get; set; } = null!;
        public string? Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime? FechaFin { get; set; }
    }
}



