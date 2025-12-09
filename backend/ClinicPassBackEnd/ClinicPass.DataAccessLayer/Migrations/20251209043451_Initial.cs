using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClinicPass.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCompleto = table.Column<string>(type: "text", nullable: false),
                    Dni = table.Column<string>(type: "text", nullable: false),
                    Especialidad = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoberturasMedicas",
                columns: table => new
                {
                    IdCobertura = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCobertura = table.Column<string>(type: "text", nullable: true),
                    TipoCobertura = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoberturasMedicas", x => x.IdCobertura);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCompleto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Dni = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Localidad = table.Column<string>(type: "text", nullable: true),
                    Provincia = table.Column<string>(type: "text", nullable: true),
                    Calle = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.IdPaciente);
                });

            migrationBuilder.CreateTable(
                name: "Tratamientos",
                columns: table => new
                {
                    IdTratamiento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoTratamiento = table.Column<int>(type: "integer", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamientos", x => x.IdTratamiento);
                });

            migrationBuilder.CreateTable(
                name: "Tutor",
                columns: table => new
                {
                    DNI = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Parentesco = table.Column<string>(type: "text", nullable: true),
                    NumeroCompleto = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    Ocupacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.DNI);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriasClinicas",
                columns: table => new
                {
                    IdHistorialClinico = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPaciente = table.Column<int>(type: "integer", nullable: false),
                    TipoPaciente = table.Column<int>(type: "integer", nullable: false),
                    AntecedentesFamiliares = table.Column<string>(type: "text", nullable: true),
                    AntecedentesPersonales = table.Column<string>(type: "text", nullable: true),
                    Activa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriasClinicas", x => x.IdHistorialClinico);
                    table.ForeignKey(
                        name: "FK_HistoriasClinicas_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PacienteCoberturas",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "integer", nullable: false),
                    IdCobertura = table.Column<int>(type: "integer", nullable: false),
                    PacienteIdPaciente = table.Column<int>(type: "integer", nullable: false),
                    CoberturaIdCobertura = table.Column<int>(type: "integer", nullable: false),
                    NumeroAfiliado = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteCoberturas", x => new { x.IdPaciente, x.IdCobertura });
                    table.ForeignKey(
                        name: "FK_PacienteCoberturas_CoberturasMedicas_CoberturaIdCobertura",
                        column: x => x.CoberturaIdCobertura,
                        principalTable: "CoberturasMedicas",
                        principalColumn: "IdCobertura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacienteCoberturas_Pacientes_PacienteIdPaciente",
                        column: x => x.PacienteIdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfesionalPacientes",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdPaciente = table.Column<int>(type: "integer", nullable: false),
                    ProfesionalId = table.Column<int>(type: "integer", nullable: false),
                    PacienteIdPaciente = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesionalPacientes", x => new { x.IdUsuario, x.IdPaciente });
                    table.ForeignKey(
                        name: "FK_ProfesionalPacientes_AspNetUsers_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesionalPacientes_Pacientes_PacienteIdPaciente",
                        column: x => x.PacienteIdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PacienteTratamientos",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "integer", nullable: false),
                    IdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    PacienteIdPaciente = table.Column<int>(type: "integer", nullable: false),
                    TratamientoIdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteTratamientos", x => new { x.IdPaciente, x.IdTratamiento });
                    table.ForeignKey(
                        name: "FK_PacienteTratamientos_Pacientes_PacienteIdPaciente",
                        column: x => x.PacienteIdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacienteTratamientos_Tratamientos_TratamientoIdTratamiento",
                        column: x => x.TratamientoIdTratamiento,
                        principalTable: "Tratamientos",
                        principalColumn: "IdTratamiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorResponsables",
                columns: table => new
                {
                    DNITutor = table.Column<int>(type: "integer", nullable: false),
                    DNIPaciente = table.Column<int>(type: "integer", nullable: false),
                    TutorDNI = table.Column<int>(type: "integer", nullable: false),
                    PacienteIdPaciente = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorResponsables", x => new { x.DNITutor, x.DNIPaciente });
                    table.ForeignKey(
                        name: "FK_TutorResponsables_Pacientes_PacienteIdPaciente",
                        column: x => x.PacienteIdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorResponsables_Tutor_TutorDNI",
                        column: x => x.TutorDNI,
                        principalTable: "Tutor",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichasDeSeguimiento",
                columns: table => new
                {
                    IdFichaSeguimiento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true),
                    ProfesionalId = table.Column<int>(type: "integer", nullable: true),
                    HistorialClinicoId = table.Column<int>(type: "integer", nullable: true),
                    HistoriaClinicaIdHistorialClinico = table.Column<int>(type: "integer", nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichasDeSeguimiento", x => x.IdFichaSeguimiento);
                    table.ForeignKey(
                        name: "FK_FichasDeSeguimiento_AspNetUsers_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FichasDeSeguimiento_HistoriasClinicas_HistoriaClinicaIdHist~",
                        column: x => x.HistoriaClinicaIdHistorialClinico,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "IdHistorialClinico");
                });

            migrationBuilder.CreateTable(
                name: "HCTratamientos",
                columns: table => new
                {
                    IdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    IdHistorialClinico = table.Column<int>(type: "integer", nullable: false),
                    TratamientoIdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    HistoriaClinicaIdHistorialClinico = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCTratamientos", x => new { x.IdTratamiento, x.IdHistorialClinico });
                    table.ForeignKey(
                        name: "FK_HCTratamientos_HistoriasClinicas_HistoriaClinicaIdHistorial~",
                        column: x => x.HistoriaClinicaIdHistorialClinico,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "IdHistorialClinico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HCTratamientos_Tratamientos_TratamientoIdTratamiento",
                        column: x => x.TratamientoIdTratamiento,
                        principalTable: "Tratamientos",
                        principalColumn: "IdTratamiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    IdDocumento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdFichaSeguimiento = table.Column<int>(type: "integer", nullable: false),
                    FichaSeguimientoIdFichaSeguimiento = table.Column<int>(type: "integer", nullable: false),
                    TipoDocumento = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Ruta = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.IdDocumento);
                    table.ForeignKey(
                        name: "FK_Documentos_FichasDeSeguimiento_FichaSeguimientoIdFichaSegui~",
                        column: x => x.FichaSeguimientoIdFichaSeguimiento,
                        principalTable: "FichasDeSeguimiento",
                        principalColumn: "IdFichaSeguimiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    IdTurno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    FichaDeSeguimientoID = table.Column<int>(type: "integer", nullable: true),
                    PacienteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.IdTurno);
                    table.ForeignKey(
                        name: "FK_Turnos_FichasDeSeguimiento_FichaDeSeguimientoID",
                        column: x => x.FichaDeSeguimientoID,
                        principalTable: "FichasDeSeguimiento",
                        principalColumn: "IdFichaSeguimiento");
                    table.ForeignKey(
                        name: "FK_Turnos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasesDiarios",
                columns: table => new
                {
                    IdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    IdTurno = table.Column<int>(type: "integer", nullable: false),
                    IdFichaSeguimiento = table.Column<int>(type: "integer", nullable: false),
                    TratamientoIdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    TurnoIdTurno = table.Column<int>(type: "integer", nullable: false),
                    FichaDeSeguimientoIdFichaSeguimiento = table.Column<int>(type: "integer", nullable: false),
                    FrecuenciaTurno = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasesDiarios", x => new { x.IdTratamiento, x.IdTurno, x.IdFichaSeguimiento });
                    table.ForeignKey(
                        name: "FK_PasesDiarios_FichasDeSeguimiento_FichaDeSeguimientoIdFichaS~",
                        column: x => x.FichaDeSeguimientoIdFichaSeguimiento,
                        principalTable: "FichasDeSeguimiento",
                        principalColumn: "IdFichaSeguimiento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PasesDiarios_Tratamientos_TratamientoIdTratamiento",
                        column: x => x.TratamientoIdTratamiento,
                        principalTable: "Tratamientos",
                        principalColumn: "IdTratamiento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PasesDiarios_Turnos_TurnoIdTurno",
                        column: x => x.TurnoIdTurno,
                        principalTable: "Turnos",
                        principalColumn: "IdTurno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfesionalTurnos",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdTurno = table.Column<int>(type: "integer", nullable: false),
                    ProfesionalId = table.Column<int>(type: "integer", nullable: false),
                    TurnoIdTurno = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesionalTurnos", x => new { x.IdUsuario, x.IdTurno });
                    table.ForeignKey(
                        name: "FK_ProfesionalTurnos_AspNetUsers_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesionalTurnos_Turnos_TurnoIdTurno",
                        column: x => x.TurnoIdTurno,
                        principalTable: "Turnos",
                        principalColumn: "IdTurno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_FichaSeguimientoIdFichaSeguimiento",
                table: "Documentos",
                column: "FichaSeguimientoIdFichaSeguimiento");

            migrationBuilder.CreateIndex(
                name: "IX_FichasDeSeguimiento_HistoriaClinicaIdHistorialClinico",
                table: "FichasDeSeguimiento",
                column: "HistoriaClinicaIdHistorialClinico");

            migrationBuilder.CreateIndex(
                name: "IX_FichasDeSeguimiento_ProfesionalId",
                table: "FichasDeSeguimiento",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_HCTratamientos_HistoriaClinicaIdHistorialClinico",
                table: "HCTratamientos",
                column: "HistoriaClinicaIdHistorialClinico");

            migrationBuilder.CreateIndex(
                name: "IX_HCTratamientos_TratamientoIdTratamiento",
                table: "HCTratamientos",
                column: "TratamientoIdTratamiento");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasClinicas_IdPaciente",
                table: "HistoriasClinicas",
                column: "IdPaciente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PacienteCoberturas_CoberturaIdCobertura",
                table: "PacienteCoberturas",
                column: "CoberturaIdCobertura");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteCoberturas_PacienteIdPaciente",
                table: "PacienteCoberturas",
                column: "PacienteIdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteTratamientos_PacienteIdPaciente",
                table: "PacienteTratamientos",
                column: "PacienteIdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteTratamientos_TratamientoIdTratamiento",
                table: "PacienteTratamientos",
                column: "TratamientoIdTratamiento");

            migrationBuilder.CreateIndex(
                name: "IX_PasesDiarios_FichaDeSeguimientoIdFichaSeguimiento",
                table: "PasesDiarios",
                column: "FichaDeSeguimientoIdFichaSeguimiento");

            migrationBuilder.CreateIndex(
                name: "IX_PasesDiarios_TratamientoIdTratamiento",
                table: "PasesDiarios",
                column: "TratamientoIdTratamiento");

            migrationBuilder.CreateIndex(
                name: "IX_PasesDiarios_TurnoIdTurno",
                table: "PasesDiarios",
                column: "TurnoIdTurno");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionalPacientes_PacienteIdPaciente",
                table: "ProfesionalPacientes",
                column: "PacienteIdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionalPacientes_ProfesionalId",
                table: "ProfesionalPacientes",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionalTurnos_ProfesionalId",
                table: "ProfesionalTurnos",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionalTurnos_TurnoIdTurno",
                table: "ProfesionalTurnos",
                column: "TurnoIdTurno");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_FichaDeSeguimientoID",
                table: "Turnos",
                column: "FichaDeSeguimientoID");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_PacienteId",
                table: "Turnos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorResponsables_PacienteIdPaciente",
                table: "TutorResponsables",
                column: "PacienteIdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_TutorResponsables_TutorDNI",
                table: "TutorResponsables",
                column: "TutorDNI");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "HCTratamientos");

            migrationBuilder.DropTable(
                name: "PacienteCoberturas");

            migrationBuilder.DropTable(
                name: "PacienteTratamientos");

            migrationBuilder.DropTable(
                name: "PasesDiarios");

            migrationBuilder.DropTable(
                name: "ProfesionalPacientes");

            migrationBuilder.DropTable(
                name: "ProfesionalTurnos");

            migrationBuilder.DropTable(
                name: "TutorResponsables");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CoberturasMedicas");

            migrationBuilder.DropTable(
                name: "Tratamientos");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Tutor");

            migrationBuilder.DropTable(
                name: "FichasDeSeguimiento");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "HistoriasClinicas");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
