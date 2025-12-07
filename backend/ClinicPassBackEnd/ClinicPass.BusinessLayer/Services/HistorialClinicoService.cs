using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.BusinessLayer.Services
{
    public class HistoriaClinicaService
    {
        private readonly ClinicPassContext _context;

        public HistoriaClinicaService(ClinicPassContext context)
        {
            _context = context;
        }

        // Obtener la historia clínica completa de un paciente
        public async Task<HistoriaClinicaDTO?> GetHistoriaClinicaAsync(int idPaciente)
        {
            var historia = await _context.HistoriasClinicas
                .FirstOrDefaultAsync(h => h.IdPaciente == idPaciente);

            if (historia == null)
                return null;

            //Cargar tratamientos del paciente
            var tratamientos = await _context.PacienteTratamientos
                .Where(pt => pt.IdPaciente == idPaciente)
                .Include(pt => pt.Tratamiento)
                .Select(pt => new TratamientoDTO
                {
                    IdTratamiento = pt.IdTratamiento,
                    TipoTratamiento = pt.Tratamiento.TipoTratamiento,
                    Motivo = pt.Tratamiento.Motivo,
                    Descripcion = pt.Tratamiento.Descripcion,
                    FechaInicio = pt.FechaInicio,
                    Estado = pt.Estado,
                    FechaFin = pt.FechaFin
                })
                .ToListAsync();

            //Cargar fichas de seguimiento
            var fichas = await _context.FichasDeSeguimiento
                .Where(f => f.IdHistorialClinico == historia.IdHistorialClinico)
                .Include(f => f.Profesional)
                .Select(f => new FichaDeSeguimientoDTO
                {
                    IdFichaSeguimiento = f.IdFichaSeguimiento,
                    IdUsuario = f.IdUsuario,
                    NombreProfesional = f.Profesional.NombreCompleto,
                    IdHistorialClinico = f.IdHistorialClinico,
                    FechaPase = f.FechaPase,
                    FechaCreacion = f.FechaCreacion,
                    FechaObservaciones = f.FechaObservaciones
                })
                .ToListAsync();

            // Map
            return new HistoriaClinicaDTO
            {
                IdHistorialClinico = historia.IdHistorialClinico,
                IdPaciente = historia.IdPaciente,
                TipoPaciente = historia.TipoPaciente,
                AntecedentesFamiliares = historia.AntecedentesFamiliares,
                AntecedentesPersonales = historia.AntecedentesPersonales,
                Activa = historia.Activa,

                Tratamientos = tratamientos,
                Fichas = fichas
            };
        }

        // Crear historia clínica automáticamente cuando se crea un paciente
        public async Task CrearHistoriaClinicaAsync(int idPaciente, int tipoPaciente = 1)
        {
            var historia = new HistoriaClinica
            {
                IdPaciente = idPaciente,
                TipoPaciente = tipoPaciente,
                Activa = true
            };

            _context.HistoriasClinicas.Add(historia);
            await _context.SaveChangesAsync();
        }
    }
}
