using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTratamientoSimple : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialClinicoTratamientos_Tratamientos_IdTratamiento",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pases_Tratamientos_IdTratamiento",
                table: "Pases");

            migrationBuilder.DropTable(
                name: "PacienteTratamientos");

            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "Tratamientos");

            migrationBuilder.RenameColumn(
                name: "TipoTratamiento",
                table: "Tratamientos",
                newName: "Nombre");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Tratamientos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TratamientoIdTratamiento",
                table: "Pases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TratamientoIdTratamiento",
                table: "HistorialClinicoTratamientos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pases_TratamientoIdTratamiento",
                table: "Pases",
                column: "TratamientoIdTratamiento");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialClinicoTratamientos_TratamientoIdTratamiento",
                table: "HistorialClinicoTratamientos",
                column: "TratamientoIdTratamiento");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialClinicoTratamientos_Tratamientos_TratamientoIdTrat~",
                table: "HistorialClinicoTratamientos",
                column: "TratamientoIdTratamiento",
                principalTable: "Tratamientos",
                principalColumn: "IdTratamiento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pases_Tratamientos_TratamientoIdTratamiento",
                table: "Pases",
                column: "TratamientoIdTratamiento",
                principalTable: "Tratamientos",
                principalColumn: "IdTratamiento",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialClinicoTratamientos_Tratamientos_TratamientoIdTrat~",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pases_Tratamientos_TratamientoIdTratamiento",
                table: "Pases");

            migrationBuilder.DropIndex(
                name: "IX_Pases_TratamientoIdTratamiento",
                table: "Pases");

            migrationBuilder.DropIndex(
                name: "IX_HistorialClinicoTratamientos_TratamientoIdTratamiento",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Tratamientos");

            migrationBuilder.DropColumn(
                name: "TratamientoIdTratamiento",
                table: "Pases");

            migrationBuilder.DropColumn(
                name: "TratamientoIdTratamiento",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Tratamientos",
                newName: "TipoTratamiento");

            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "Tratamientos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PacienteTratamientos",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "integer", nullable: false),
                    IdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteTratamientos", x => new { x.IdPaciente, x.IdTratamiento });
                    table.ForeignKey(
                        name: "FK_PacienteTratamientos_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacienteTratamientos_Tratamientos_IdTratamiento",
                        column: x => x.IdTratamiento,
                        principalTable: "Tratamientos",
                        principalColumn: "IdTratamiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacienteTratamientos_IdTratamiento",
                table: "PacienteTratamientos",
                column: "IdTratamiento");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialClinicoTratamientos_Tratamientos_IdTratamiento",
                table: "HistorialClinicoTratamientos",
                column: "IdTratamiento",
                principalTable: "Tratamientos",
                principalColumn: "IdTratamiento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pases_Tratamientos_IdTratamiento",
                table: "Pases",
                column: "IdTratamiento",
                principalTable: "Tratamientos",
                principalColumn: "IdTratamiento",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
