using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.Data
{
    public class ClinicPassContext : IdentityDbContext<Profesional, IdentityRole<int>, int>
	{
        public ClinicPassContext(DbContextOptions<ClinicPassContext> options)
            : base(options)
        {
        }

        // Tablas principales
        public DbSet<Paciente> Pacientes { get; set; }
        //public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<HistoriaClinica> HistoriasClinicas { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }
        public DbSet<FichaDeSeguimiento> FichasDeSeguimiento { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<CoberturaMedica> CoberturasMedicas { get; set; }

        // Tablas intermedias
        public DbSet<PacienteCobertura> PacienteCoberturas { get; set; }
        public DbSet<PacienteTratamiento> PacienteTratamientos { get; set; }
        public DbSet<HCTratamiento> HCTratamientos { get; set; }
        public DbSet<ProfesionalTurno> ProfesionalTurnos { get; set; }
        public DbSet<ProfesionalPaciente> ProfesionalPacientes { get; set; }
        public DbSet<TutorResponsablePaciente> TutorResponsables { get; set; }
        public DbSet<PaseDiario> PasesDiarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PACIENTE - COBERTURA (PK compuesta)
            modelBuilder.Entity<PacienteCobertura>()
                .HasKey(pc => new { pc.IdPaciente, pc.IdCobertura });

            // PROFESIONAL - TURNOS
            modelBuilder.Entity<ProfesionalTurno>()
                .HasKey(pt => new { pt.IdUsuario, pt.IdTurno });

            // PROFESIONAL - PACIENTE
            modelBuilder.Entity<ProfesionalPaciente>()
                .HasKey(pp => new { pp.IdUsuario, pp.IdPaciente });

            // Relación 1:1 entre Paciente e HistoriaClinica
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.HistoriaClinica)
                .WithOne(h => h.Paciente)
                .HasForeignKey<HistoriaClinica>(h => h.IdPaciente);

            // HISTORIAL - TRATAMIENTO
            modelBuilder.Entity<HCTratamiento>()
                .HasKey(hc => new { hc.IdTratamiento, hc.IdHistorialClinico });

            // PACIENTE - TRATAMIENTO
            modelBuilder.Entity<PacienteTratamiento>()
                .HasKey(pt => new { pt.IdPaciente, pt.IdTratamiento });

            // TUTOR - PACIENTE
            modelBuilder.Entity<TutorResponsablePaciente>()
                .HasKey(tp => new { tp.DNITutor, tp.DNIPaciente });

            // PASE DIARIO (PK triple)
            modelBuilder.Entity<PaseDiario>()
                .HasKey(p => new { p.IdTratamiento, p.IdTurno, p.IdFichaSeguimiento });


        }
    }
}
