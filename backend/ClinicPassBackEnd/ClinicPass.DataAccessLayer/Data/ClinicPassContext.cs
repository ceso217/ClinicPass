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
        
        //public DbSet<ProfesionalTurno> ProfesionalTurnos { get; set; }
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
            // PROFESIONAL
            // =========================

            modelBuilder.Entity<Profesional>()
                .ToTable(t => t.HasCheckConstraint("CHK_Profesional_Dni_NumericAndLength", "LENGTH(\"Dni\") BETWEEN 7 AND 8 AND \"Dni\" ~ '^[0-9]+$'"));

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

            modelBuilder.Entity<Paciente>()
                .ToTable(t => t.HasCheckConstraint("CHK_Paciente_Dni_NumericAndLength","LENGTH(\"Dni\") BETWEEN 7 AND 8 AND \"Dni\" ~ '^[0-9]+$'"));

            modelBuilder.Entity<Paciente>()
                .ToTable(t => t.HasCheckConstraint("CHK_Paciente_FechaNacimiento_PastDate","\"FechaNacimiento\"<NOW()"));

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
            //modelBuilder.Entity<Turno>()
            //    .HasMany(t => t.ProfesionalTurnos)
            //    .WithOne(pt => pt.Turno)
            //    .HasForeignKey(pt => pt.IdTurno);

            modelBuilder.Entity<Turno>()
                .HasMany(t => t.Pases)
                .WithOne(p => p.Turno)
                .HasForeignKey(p => p.IdTurno);

            // =========================
            // TRATAMIENTO
            // =========================
         

            //modelBuilder.Entity<Tratamiento>()
            //    .HasMany(t => t.Pases)
            //    .WithOne(p => p.Tratamiento)
            //    .HasForeignKey(p => p.IdTratamiento);

            //modelBuilder.Entity<Tratamiento>()
            //    .HasMany(t => t.HistoriasClinicas)
            //    .WithOne(hct => hct.Tratamiento)
            //    .HasForeignKey(hct => hct.IdTratamiento);
            // =========================
            // TABLAS INTERMEDIAS (PK COMPUESTAS + RELACIONES)
            // =========================

            // -------- PacienteCobertura --------
            modelBuilder.Entity<PacienteCobertura>()
                .HasKey(pc => new { pc.IdPaciente, pc.IdCobertura });

            modelBuilder.Entity<PacienteCobertura>()
                .HasOne(pc => pc.Paciente)
                .WithMany(p => p.PacienteCoberturas)
                .HasForeignKey(pc => pc.IdPaciente);

            modelBuilder.Entity<PacienteCobertura>()
                .HasOne(pc => pc.Cobertura)
                .WithMany(c => c.PacienteCoberturas)
                .HasForeignKey(pc => pc.IdCobertura);

            // -------- PacienteTratamiento --------
            //modelBuilder.Entity<PacienteTratamiento>()
            //    .HasKey(pt => new { pt.IdPaciente, pt.IdTratamiento });

            //modelBuilder.Entity<PacienteTratamiento>()
            //    .HasOne(pt => pt.Paciente)
            //    .WithMany(p => p.PacienteTratamientos)
            //    .HasForeignKey(pt => pt.IdPaciente);

            //modelBuilder.Entity<PacienteTratamiento>()
            //    .HasOne(pt => pt.Tratamiento)
            //    .WithMany(t => t.Pacientes)
            //    .HasForeignKey(pt => pt.IdTratamiento);

            // -------- ProfesionalTurno --------
            //modelBuilder.Entity<ProfesionalTurno>()
            //    .HasKey(pt => new { pt.IdUsuario, pt.IdTurno });

            //modelBuilder.Entity<ProfesionalTurno>()
            //    .HasOne(pt => pt.Profesional)
            //    .WithMany(p => p.ProfesionalTurnos)
            //    .HasForeignKey(pt => pt.IdUsuario);

            //modelBuilder.Entity<ProfesionalTurno>()
            //    .HasOne(pt => pt.Turno)
            //    .WithMany(t => t.ProfesionalTurnos)
            //    .HasForeignKey(pt => pt.IdTurno);

            // -------- ProfesionalPaciente --------
            modelBuilder.Entity<ProfesionalPaciente>()
                .HasKey(pp => new { pp.IdUsuario, pp.IdPaciente });

            modelBuilder.Entity<ProfesionalPaciente>()
                .HasOne(pp => pp.Profesional)
                .WithMany(p => p.ProfesionalPacientes)
                .HasForeignKey(pp => pp.IdUsuario);

            modelBuilder.Entity<ProfesionalPaciente>()
                .HasOne(pp => pp.Paciente)
                .WithMany(p => p.ProfesionalesVinculados)
                .HasForeignKey(pp => pp.IdPaciente);

            // -------- TutorResponsablePaciente --------
            modelBuilder.Entity<TutorResponsablePaciente>()
                .HasKey(tp => new { tp.DNITutor, tp.IdPaciente });

            modelBuilder.Entity<TutorResponsablePaciente>()
                .HasOne(tp => tp.Tutor)
                .WithMany(t => t.Pacientes)
                .HasForeignKey(tp => tp.DNITutor);

            modelBuilder.Entity<TutorResponsablePaciente>()
                .HasOne(tp => tp.Paciente)
                .WithMany(p => p.TutoresResponsables)
                .HasForeignKey(tp => tp.IdPaciente);

            // -------- PaseDiario --------
            modelBuilder.Entity<PaseDiario>()
                .HasKey(p => new { p.IdTratamiento, p.IdTurno });

            //modelBuilder.Entity<PaseDiario>()
            //    .HasOne(p => p.Tratamiento)
            //    .WithMany(t => t.Pases)
            //    .HasForeignKey(p => p.IdTratamiento);

            modelBuilder.Entity<PaseDiario>()
                .HasOne(p => p.Turno)
                .WithMany(t => t.Pases)
                .HasForeignKey(p => p.IdTurno);

            // -------- HistorialClinicoTratamiento --------
            modelBuilder.Entity<HistorialClinicoTratamiento>()
                .HasKey(hct => new { hct.IdTratamiento, hct.IdHistorialClinico });

            //modelBuilder.Entity<HistorialClinicoTratamiento>()
            //    .HasOne(hct => hct.Tratamiento)
            //    .WithMany(t => t.HistoriasClinicas)
            //    .HasForeignKey(hct => hct.IdTratamiento);

            modelBuilder.Entity<HistorialClinicoTratamiento>()
                .HasOne(hct => hct.HistoriaClinica)
                .WithMany(h => h.Tratamientos)
                .HasForeignKey(hct => hct.IdHistorialClinico);
        }
    }
}