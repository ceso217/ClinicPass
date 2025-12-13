namespace ClinicPass.BusinessLayer.DTOs
{
    public class PacienteDTO
    {
        public int IdPaciente { get; set; }
        public string NombreCompleto { get; set; } = "";
        public string Dni { get; set; } = "";
        public DateTime FechaNacimiento { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Calle { get; set; }
        public string Telefono { get; set; }
    }
}


