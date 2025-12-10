using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.BusinessLayer.Services
{
    public class HistoriaClinicaService : IHistoriaClinicaService
    {
        private readonly ClinicPassContext _context;

        public HistoriaClinicaService(ClinicPassContext context)
        {
            _context = context;
        }

        // ============================================================
        // Obtener historia por ID
        // ============================================================
        public async Task<HistoriaClinicaDTO?> GetByIdAsync(int id)
        {
            var historia = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                    .ThenInclude(p => p.PacienteTratamientos)
                        .ThenInclude(pt => pt.Tratamiento)
                .Include(h => h.Fichas)
                    .ThenInclude(f => f.Profesional)
                .FirstOrDefaultAsync(h => h.IdHistorialClinico == id);

            if (historia == null)
                return null;

            return MapToDTO(historia);
        }

        public async Task<HistoriaClinicaDTO?> GetHistoriaClinicaAsync(int idPaciente)
        {
            var historia = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                    .ThenInclude(p => p.PacienteTratamientos)
                        .ThenInclude(pt => pt.Tratamiento)
                .Include(h => h.Fichas)
                    .ThenInclude(f => f.Profesional)
                .FirstOrDefaultAsync(h => h.IdPaciente == idPaciente);

            if (historia == null)
                return null;

            return MapToDTO(historia);
        }


        // ============================================================
        // Crear historia clínica
        // ============================================================
        public async Task<HistoriaClinicaDTO> CreateAsync(int idPaciente, int tipoPaciente)
        {
            var historia = new HistoriaClinica
            {
                IdPaciente = idPaciente,
                TipoPaciente = tipoPaciente,
                Activa = true
            };

            _context.HistoriasClinicas.Add(historia);
            await _context.SaveChangesAsync();

            // Cargar relaciones necesarias para MapToDTO
            await _context.Entry(historia)
                .Reference(h => h.Paciente)
                .LoadAsync();

            await _context.Entry(historia.Paciente)
                .Collection(p => p.PacienteTratamientos)
                .LoadAsync();

            return MapToDTO(historia);
        }


        public async Task<bool> UpdateAsync(int idHistorial, HistoriaClinicaUpdateDTO dto) 
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null) return false;

            historia.TipoPaciente = dto.TipoPaciente;
            historia.AntecedentesFamiliares = dto.AntecedentesFamiliares;
            historia.AntecedentesPersonales = dto.AntecedentesPersonales;
            historia.Activa = dto.Activa;

            await _context.SaveChangesAsync();
            return true;
        }


        // ============================================================
        // Desactivar historia clínica
        // ============================================================
        public async Task<bool> DesactivarAsync(int idHistorial)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null)
                return false;

            historia.Activa = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // ============================================================
        // Activar historia clínica
        // ============================================================
        public async Task<bool> ActivarAsync(int idHistorial)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null)
                return false;

            historia.Activa = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AgregarTratamientoAsync(int idHistorial, AgregarTratamientoaHistoriaDTO dto)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null) return false;

            var pacienteId = historia.IdPaciente;

            var nuevo = new PacienteTratamiento
            {
                IdPaciente = pacienteId,
                IdTratamiento = dto.IdTratamiento,
                FechaInicio = dto.FechaInicio,
                Estado = dto.Estado,
                FechaFin = dto.FechaFin
            };

            _context.PacienteTratamientos.Add(nuevo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> QuitarTratamientoAsync(int idHistorial, int idTratamiento)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null) return false;

            var registro = await _context.PacienteTratamientos
                .FirstOrDefaultAsync(pt => pt.IdPaciente == historia.IdPaciente &&
                                           pt.IdTratamiento == idTratamiento);

            if (registro == null) return false;

            _context.PacienteTratamientos.Remove(registro);
            await _context.SaveChangesAsync();
            return true;
        }


        // ============================================================
        // MAPEADOR A DTO
        // ============================================================
        private HistoriaClinicaDTO MapToDTO(HistoriaClinica h)
        {
            return new HistoriaClinicaDTO
            {
                IdHistorialClinico = h.IdHistorialClinico,
                IdPaciente = h.IdPaciente,
                TipoPaciente = h.TipoPaciente,
                AntecedentesFamiliares = h.AntecedentesFamiliares,
                AntecedentesPersonales = h.AntecedentesPersonales,
                Activa = h.Activa,

                // =======================================
                // TRATAMIENTOS: vienen desde PacienteTratamientos
                // =======================================
                Tratamientos = h.Paciente.PacienteTratamientos
                    .Select(pt => new TratamientoPacienteDTO
                    {
                        IdTratamiento = pt.IdTratamiento,
                        TipoTratamiento = pt.Tratamiento.TipoTratamiento,
                        Motivo = pt.Tratamiento.Motivo,
                        Descripcion = pt.Tratamiento.Descripcion,
                        FechaInicio = pt.FechaInicio,
                        Estado = pt.Estado,
                        FechaFin = pt.FechaFin
                    })
                    .ToList(),

                // =======================================
                // FICHAS CLÍNICAS
                // =======================================
                Fichas = h.Fichas.Select(f => new FichaDeSeguimientoDTO
                {
                    IdFichaSeguimiento = f.IdFichaSeguimiento,
                    IdUsuario = f.IdUsuario,
                    NombreProfesional = f.Profesional.NombreCompleto,
                    IdHistorialClinico = f.IdHistorialClinico,
                    FechaPase = f.FechaPase,
                    FechaCreacion = f.FechaCreacion,
                    Observaciones = f.Observaciones
                }).ToList()
            };
        }

    }
}


