namespace ClinicPass.BusinessLayer.DTOs
{
    public class PaseDTO
{
    public int IdTratamiento { get; set; }
    public int IdTurno { get; set; }
    public string? FrecuenciaTurno { get; set; }

    public DateTime FechaTurno { get; set; } 
    public string ProfesionalNombre { get; set; } = "";
}
}

