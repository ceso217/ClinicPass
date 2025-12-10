namespace ClinicPass.BusinessLayer.DTOs
{
    public class TratamientoCreateDTO
    {
        public int IdPaciente { get; set; }
        public string TipoTratamiento { get; set; } = null!;
        public string Motivo { get; set; } = null!;
        public string? Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }
    }
}

