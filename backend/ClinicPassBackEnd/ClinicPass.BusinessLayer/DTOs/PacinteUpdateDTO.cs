namespace ClinicPass.BusinessLayer.DTOs
{
    public class PacienteUpdateDTO
    {
        public string NombreCompleto { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Localidad { get; set; }
        public string? Provincia { get; set; }
        public string? Calle { get; set; }
        public string? Telefono { get; set; }
    }
}


