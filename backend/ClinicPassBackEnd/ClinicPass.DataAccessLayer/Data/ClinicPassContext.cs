using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicPass.DataAccessLayer.Data
{
    public class ClinicPassContext
        : IdentityDbContext<Profesional, IdentityRole<int>, int>
    {
        public ClinicPassContext(DbContextOptions<ClinicPassContext> options)
            : base(options)
        {
        }

        // =========================
        // TABLAS PRINCIPALES
        // =========================
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<HistoriaClinica> HistoriasClinicas { get; set; }
        public DbSet<FichaDeSeguimiento> FichasDeSeguimiento { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<CoberturaMedica> CoberturasMedicas { get; set; }
        public DbSet<Tutor> Tutores { get; set; }

        // =========================
        // TABLAS INTERMEDIAS (N–N)
        // =========================
        public DbSet<PacienteCobertura> PacienteCoberturas { get; set; }
        public DbSet<PacienteTratamiento> PacienteTratamientos { get; set; }
        public DbSet<ProfesionalTurno> ProfesionalTurnos { get; set; }
        public DbSet<ProfesionalPaciente> ProfesionalPacientes { get; set; }
        public DbSet<TutorResponsablePaciente> TutorResponsables { get; set; }
        public DbSet<PaseDiario> Pases { get; set; }
        public DbSet<HistorialClinicoTratamiento> HistorialClinicoTratamientos { get; set; }

        // =========================
        // CONFIGURACIÓN DE MODELO
        // =========================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // PACIENTE
            // =========================
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.HistoriaClinica)
                .WithOne(h => h.Paciente)
                .HasForeignKey<HistoriaClinica>(h => h.IdPaciente);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Turnos)
                .WithOne(t => t.Paciente)
                .HasForeignKey(t => t.IdPaciente);

            // =========================
            // HISTORIA CLÍNICA
            // =========================
            modelBuilder.Entity<HistoriaClinica>()
                .HasMany(h => h.Fichas)
                .WithOne(f => f.HistoriaClinica)
                .HasForeignKey(f => f.IdHistorialClinico);

            modelBuilder.Entity<HistoriaClinica>()
                .HasMany(h => h.Tratamientos)
                .WithOne(hct => hct.HistoriaClinica)
                .HasForeignKey(hct => hct.IdHistorialClinico);

            // =========================
            // FICHA DE SEGUIMIENTO
            // =========================
            modelBuilder.Entity<FichaDeSeguimiento>()
                .HasOne(f => f.Profesional)
                .WithMany()
                .HasForeignKey(f => f.IdUsuario);

            modelBuilder.Entity<FichaDeSeguimiento>()
                .HasMany(f => f.Turnos)
                .WithOne(t => t.FichaDeSeguimiento)
                .HasForeignKey(t => t.IdFichaSeguimiento)
                .IsRequired(false);

            modelBuilder.Entity<FichaDeSeguimiento>()
                .HasMany(f => f.Documentos)
                .WithOne(d => d.FichaSeguimiento)
                .HasForeignKey(d => d.IdFichaSeguimiento);

            // =========================
            // TURNO
            // =========================
            modelBuilder.Entity<Turno>()
                .HasMany(t => t.ProfesionalTurnos)
                .WithOne(pt => pt.Turno)
                .HasForeignKey(pt => pt.IdTurno);

            modelBuilder.Entity<Turno>()
                .HasMany(t => t.Pases)
                .WithOne(p => p.Turno)
                .HasForeignKey(p => p.IdTurno);

            // =========================
            // TRATAMIENTO
            // =========================
            modelBuilder.Entity<Tratamiento>()
                .HasMany(t => t.Pacientes)
                .WithOne(pt => pt.Tratamiento)
                .HasForeignKey(pt => pt.IdTratamiento);

            modelBuilder.Entity<Tratamiento>()
                .HasMany(t => t.Pases)
                .WithOne(p => p.Tratamiento)
                .HasForeignKey(p => p.IdTratamiento);

            modelBuilder.Entity<Tratamiento>()
                .HasMany(t => t.HistoriasClinicas)
                .WithOne(hct => hct.Tratamiento)
                .HasForeignKey(hct => hct.IdTratamiento);

            // =========================
            // TABLAS INTERMEDIAS (PK COMPUESTAS)
            // =========================
            modelBuilder.Entity<PacienteCobertura>()
                .HasKey(pc => new { pc.IdPaciente, pc.IdCobertura });

            modelBuilder.Entity<PacienteTratamiento>()
                .HasKey(pt => new { pt.IdPaciente, pt.IdTratamiento });

            modelBuilder.Entity<ProfesionalTurno>()
                .HasKey(pt => new { pt.IdUsuario, pt.IdTurno });

            modelBuilder.Entity<ProfesionalPaciente>()
                .HasKey(pp => new { pp.IdUsuario, pp.IdPaciente });

            modelBuilder.Entity<TutorResponsablePaciente>()
                .HasKey(tp => new { tp.DNITutor, tp.IdPaciente });

            modelBuilder.Entity<PaseDiario>()
                .HasKey(p => new { p.IdTratamiento, p.IdTurno });

            modelBuilder.Entity<HistorialClinicoTratamiento>()
                .HasKey(hct => new { hct.IdTratamiento, hct.IdHistorialClinico });
        }
    }
}