using ClinicPass.BusinessLayer.DTOs;
using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.DTOs.HistoriaClinica;
using ClinicPass.DataAccessLayer.DTOs.Tratamiento;
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

        public async Task<HistoriaClinicaDTO?> GetByIdAsync(int id)
        {
            var historia = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                
                .Include(h => h.Fichas)
                    .ThenInclude(f => f.Profesional)
                .FirstOrDefaultAsync(h => h.IdHistorialClinico == id);

            return historia == null ? null : MapToDTO(historia);
        }

        public async Task<HistoriaClinicaDTO?> GetHistoriaClinicaAsync(int idPaciente)
        {
            var historia = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                    
                .Include(h => h.Fichas)
                    .ThenInclude(f => f.Profesional)
                .FirstOrDefaultAsync(h => h.IdPaciente == idPaciente);

            return historia == null ? null : MapToDTO(historia);
        }

        public async Task<HistoriaClinicaDTO> CreateAsync(HistoriaClinicaCreateDTO dto)
        {
            var historia = new HistoriaClinica
            {
                IdPaciente = dto.IdPaciente,
                AntecedentesFamiliares = dto.AntecedentesFamiliares,
                AntecedentesPersonales = dto.AntecedentesPersonales,
                Activa = true
            };

            _context.HistoriasClinicas.Add(historia);
            await _context.SaveChangesAsync();

            await _context.Entry(historia).Reference(h => h.Paciente).LoadAsync();
            
                
               

            return MapToDTO(historia);
        }


        private HistoriaClinicaDTO MapToDTO(HistoriaClinica h)
        {
            return new HistoriaClinicaDTO
            {
                IdHistorialClinico = h.IdHistorialClinico,
                IdPaciente = h.IdPaciente,
                AntecedentesFamiliares = h.AntecedentesFamiliares,
                AntecedentesPersonales = h.AntecedentesPersonales,
                Activa = h.Activa,

                

                Fichas = h.Fichas.Select(f => new FichaDeSeguimientoDTO
                {
                    IdFichaSeguimiento = f.IdFichaSeguimiento,
                    IdUsuario = f.IdUsuario,
                    NombreProfesional = f.Profesional.NombreCompleto,
                    IdHistorialClinico = f.IdHistorialClinico,
                    //FechaPase = f.FechaPase,
                    FechaCreacion = f.FechaCreacion,
                    Observaciones = f.Observaciones
                }).ToList()
            };
        }
        public async Task<bool> UpdateAsync(int idHistorial, HistoriaClinicaUpdateDTO dto)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null) return false;

            historia.AntecedentesFamiliares = dto.AntecedentesFamiliares;
            historia.AntecedentesPersonales = dto.AntecedentesPersonales;
            historia.Activa = dto.Activa;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DesactivarAsync(int idHistorial)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null) return false;

            historia.Activa = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivarAsync(int idHistorial)
        {
            var historia = await _context.HistoriasClinicas.FindAsync(idHistorial);
            if (historia == null) return false;

            historia.Activa = true;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<HistoriaClinicaListadoDTO>> GetAllAsync()
        {
            var historias = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                .ToListAsync();

            return historias.Select(h => new HistoriaClinicaListadoDTO
            {
                IdHistorialClinico = h.IdHistorialClinico,
                Activa = h.Activa,
                Paciente = new PacienteBasicoDTO
                {
                    IdPaciente = h.Paciente.IdPaciente,
                    NombreCompleto = h.Paciente.NombreCompleto,
                    Dni = h.Paciente.Dni,
                    Edad = CalcularEdad(h.Paciente.FechaNacimiento)
                }
            }).ToList();
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }
        public async Task<HistoriaClinicaDetalleDTO?> GetDetalleByIdAsync(int id)
        {
            var historia = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                .Include(h => h.Fichas)
                    .ThenInclude(f => f.Profesional)
                .Include(h => h.Tratamientos)
                    .ThenInclude(t => t.Tratamiento)
                .FirstOrDefaultAsync(h => h.IdHistorialClinico == id);

            if (historia == null) return null;

            return new HistoriaClinicaDetalleDTO
            {
                Paciente = new PacienteDTO
                {
                    IdPaciente = historia.Paciente.IdPaciente,
                    NombreCompleto = historia.Paciente.NombreCompleto,
                    Dni = historia.Paciente.Dni,
                    FechaNacimiento = historia.Paciente.FechaNacimiento,
                    Telefono = historia.Paciente.Telefono,
                    Localidad = historia.Paciente.Localidad,
                    Provincia = historia.Paciente.Provincia
                },

                HistoriaClinica = MapToDTO(historia),

                Tratamientos = historia.Tratamientos.Select(t => new TratamientoDetalleDTO
                {
                    IdTratamiento = t.IdTratamiento,
                    Nombre = t.Tratamiento.Nombre,
                    Descripcion = t.Tratamiento.Descripcion,
                    Activo = t.Activo,
                    FechaInicio = t.FechaInicio,
                    FechaFin = t.FechaFin
                }).ToList(),


                Fichas = historia.Fichas.Select(f => new FichaDeSeguimientoDTO
                {
                    IdFichaSeguimiento = f.IdFichaSeguimiento,
                    FechaCreacion = f.FechaCreacion,
                    Observaciones = f.Observaciones,
                    NombreProfesional = f.Profesional.NombreCompleto
                }).ToList()
            };
        }


        public async Task<HistoriaClinicaOrdenadaDTO?> GetOrdenadaByIdAsync(int id)
        {
            var historia = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                .Include(h => h.Fichas)
                    .ThenInclude(f => f.Profesional)
                .Include(h => h.Tratamientos)
                    .ThenInclude(t => t.Tratamiento)
                .FirstOrDefaultAsync(h => h.IdHistorialClinico == id);

            if (historia == null) return null;

            return new HistoriaClinicaOrdenadaDTO
            {
                Paciente = new PacienteDTO
                {
                    IdPaciente = historia.Paciente.IdPaciente,
                    NombreCompleto = historia.Paciente.NombreCompleto,
                    Dni = historia.Paciente.Dni,
                    FechaNacimiento = historia.Paciente.FechaNacimiento,
                    Telefono = historia.Paciente.Telefono,
                    Localidad = historia.Paciente.Localidad,
                    Provincia = historia.Paciente.Provincia
                },

                HistoriaClinica = new HistoriaClinicaOrdenadaDataDTO
                {
                    IdHistorialClinico = historia.IdHistorialClinico,
                    Activa = historia.Activa,
                    AntecedentesFamiliares = historia.AntecedentesFamiliares,
                    AntecedentesPersonales = historia.AntecedentesPersonales,

                    Tratamientos = historia.Tratamientos.Select(t => new TratamientoConFichasDTO
                    {
                        IdTratamiento = t.IdTratamiento,
                        Nombre = t.Tratamiento.Nombre,
                        Descripcion = t.Tratamiento.Descripcion,
                        Activo = t.Activo,
                        FechaInicio = t.FechaInicio,

                        FichasDeSeguimiento = historia.Fichas
                            .Where(f => f.TratamientoId == t.IdTratamiento)
                            .Select(f => new FichaDeSeguimientoDTO
                            {
                                IdFichaSeguimiento = f.IdFichaSeguimiento,
                                FechaCreacion = f.FechaCreacion,
                                Observaciones = f.Observaciones,
                                NombreProfesional = f.Profesional.NombreCompleto
                            }).ToList()
                    }).ToList(),

                    FichasSinTratamiento = historia.Fichas
                        .Where(f => f.TratamientoId == null)
                        .Select(f => new FichaDeSeguimientoDTO
                        {
                            IdFichaSeguimiento = f.IdFichaSeguimiento,
                            FechaCreacion = f.FechaCreacion,
                            Observaciones = f.Observaciones,
                            NombreProfesional = f.Profesional.NombreCompleto
                        }).ToList()
                }
            };
        }



    }
}