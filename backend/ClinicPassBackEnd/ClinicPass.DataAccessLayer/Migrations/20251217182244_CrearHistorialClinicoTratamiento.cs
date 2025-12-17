using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CrearHistorialClinicoTratamiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialClinicoTratamientos_Tratamientos_TratamientoIdTrat~",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.DropIndex(
                name: "IX_HistorialClinicoTratamientos_TratamientoIdTratamiento",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.DropColumn(
                name: "TratamientoIdTratamiento",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "HistorialClinicoTratamientos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "HistorialClinicoTratamientos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "HistorialClinicoTratamientos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "HistorialClinicoTratamientos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialClinicoTratamientos_Tratamientos_IdTratamiento",
                table: "HistorialClinicoTratamientos",
                column: "IdTratamiento",
                principalTable: "Tratamientos",
                principalColumn: "IdTratamiento",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialClinicoTratamientos_Tratamientos_IdTratamiento",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "HistorialClinicoTratamientos");

            migrationBuilder.AddColumn<int>(
                name: "TratamientoIdTratamiento",
                table: "HistorialClinicoTratamientos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
