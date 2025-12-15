using ClinicPass.DataAccessLayer.Models;

public interface ITutorService
{
    Task<Tutor> CrearAsync(Tutor tutor);
    Task<Tutor?> GetByDniAsync(int dni);
}