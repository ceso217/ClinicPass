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
            return await _context.HistoriasClinicas
                .Where(h => h.IdHistorialClinico == id)
                .Select(h => new HistoriaClinicaDetalleDTO
                {
                    Paciente = new PacienteDTO
                    {
                        IdPaciente = h.Paciente.IdPaciente,
                        NombreCompleto = h.Paciente.NombreCompleto,
                        Dni = h.Paciente.Dni,
                        FechaNacimiento = h.Paciente.FechaNacimiento,
                        Telefono = h.Paciente.Telefono,
                        Localidad = h.Paciente.Localidad,
                        Provincia = h.Paciente.Provincia
                    },

                    HistoriaClinica = new HistoriaClinicaDTO
                    {
                        IdHistorialClinico = h.IdHistorialClinico,
                        IdPaciente = h.IdPaciente,
                        AntecedentesFamiliares = h.AntecedentesFamiliares,
                        AntecedentesPersonales = h.AntecedentesPersonales,
                        Activa = h.Activa,
                        Fichas = new List<FichaDeSeguimientoDTO>() // 👈 vacío a propósito
                    },

                    Tratamientos = h.Tratamientos.Select(t => new TratamientoDetalleDTO
                    {
                        IdTratamiento = t.IdTratamiento,
                        Nombre = t.Tratamiento.Nombre,
                        Descripcion = t.Tratamiento.Descripcion,
                        Activo = t.Activo,
                        FechaInicio = t.FechaInicio,
                        FechaFin = t.FechaFin
                    }).ToList(),

                    Fichas = h.Fichas.Select(f => new FichaDeSeguimientoHistorialDTO
                    {
                        IdFichaSeguimiento = f.IdFichaSeguimiento,
                        FechaCreacion = f.FechaCreacion,
                        Observaciones = f.Observaciones,
                        TratamientoId = f.TratamientoId,
                        IdUsuario = f.IdUsuario,

                        NombreProfesional = _context.Users
                            .Where(u => u.Id == f.IdUsuario)
                            .Select(u => u.NombreCompleto)
                            .FirstOrDefault()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }




        public async Task<HistoriaClinicaOrdenadaDTO?> GetOrdenadaByIdAsync(int id)
        {
            return await _context.HistoriasClinicas
                .Where(h => h.IdHistorialClinico == id)
                .Select(h => new HistoriaClinicaOrdenadaDTO
                {
                    Paciente = new PacienteDTO
                    {
                        IdPaciente = h.Paciente.IdPaciente,
                        NombreCompleto = h.Paciente.NombreCompleto,
                        Dni = h.Paciente.Dni,
                        FechaNacimiento = h.Paciente.FechaNacimiento,
                        Telefono = h.Paciente.Telefono,
                        Localidad = h.Paciente.Localidad,
                        Provincia = h.Paciente.Provincia
                    },

                    HistoriaClinica = new HistoriaClinicaOrdenadaDataDTO
                    {
                        IdHistorialClinico = h.IdHistorialClinico,
                        Activa = h.Activa,
                        AntecedentesFamiliares = h.AntecedentesFamiliares,
                        AntecedentesPersonales = h.AntecedentesPersonales,

                        Tratamientos = h.Tratamientos.Select(t => new TratamientoConFichasDTO
                        {
                            IdTratamiento = t.IdTratamiento,
                            Nombre = t.Tratamiento.Nombre,
                            Descripcion = t.Tratamiento.Descripcion,
                            Activo = t.Activo,
                            FechaInicio = t.FechaInicio,

                            FichasDeSeguimiento = h.Fichas
                                .Where(f => f.TratamientoId == t.IdTratamiento)
                                .Select(f => new FichaDeSeguimientoHistorialDTO
                                {
                                    IdFichaSeguimiento = f.IdFichaSeguimiento,
                                    FechaCreacion = f.FechaCreacion,
                                    Observaciones = f.Observaciones,
                                    TratamientoId = f.TratamientoId
                                }).ToList()
                        }).ToList(),

                        FichasSinTratamiento = h.Fichas
                            .Where(f => f.TratamientoId == null)
                            .Select(f => new FichaDeSeguimientoHistorialDTO
                            {
                                IdFichaSeguimiento = f.IdFichaSeguimiento,
                                FechaCreacion = f.FechaCreacion,
                                Observaciones = f.Observaciones,
                                TratamientoId = null,
                                IdUsuario = f.IdUsuario,

                                NombreProfesional = _context.Users
                                .Where(u => u.Id == f.IdUsuario)
                                .Select(u => u.NombreCompleto)
                                .FirstOrDefault()
                            }).ToList()
                    }
                })
                .FirstOrDefaultAsync();
        }





    }
}