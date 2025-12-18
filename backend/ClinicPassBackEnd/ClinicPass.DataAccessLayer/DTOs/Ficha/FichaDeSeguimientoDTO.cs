public class FichaDeSeguimientoDTO
{
    public int IdFichaSeguimiento { get; set; }

    public int IdUsuario { get; set; }
    public string? NombreProfesional { get; set; } = null!;

    public int IdHistorialClinico { get; set; }

    public int? TratamientoId { get; set; }
    public string? NombreTratamiento { get; set; } 

    public DateTime FechaCreacion { get; set; }
    public string? Observaciones { get; set; }
}
