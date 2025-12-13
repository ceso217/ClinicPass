using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

public class CoberturaService : ICoberturaService
{
    private readonly ClinicPassContext _context;

    public CoberturaService(ClinicPassContext context)
    {
        _context = context;
    }

    public async Task<CoberturaMedica> CrearAsync(CoberturaMedica cobertura)
    {
        _context.CoberturasMedicas.Add(cobertura);
        await _context.SaveChangesAsync();
        return cobertura;
    }

    public async Task<List<CoberturaMedica>> GetAllAsync()
        => await _context.CoberturasMedicas.ToListAsync();
}