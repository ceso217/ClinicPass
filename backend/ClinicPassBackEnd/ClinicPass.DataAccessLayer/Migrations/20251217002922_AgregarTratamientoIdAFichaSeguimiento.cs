using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTratamientoIdAFichaSeguimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaPase",
                table: "FichasDeSeguimiento");

            migrationBuilder.AddColumn<int>(
                name: "TratamientoId",
                table: "FichasDeSeguimiento",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FichasDeSeguimiento_TratamientoId",
                table: "FichasDeSeguimiento",
                column: "TratamientoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FichasDeSeguimiento_Tratamientos_TratamientoId",
                table: "FichasDeSeguimiento",
                column: "TratamientoId",
                principalTable: "Tratamientos",
                principalColumn: "IdTratamiento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichasDeSeguimiento_Tratamientos_TratamientoId",
                table: "FichasDeSeguimiento");

            migrationBuilder.DropIndex(
                name: "IX_FichasDeSeguimiento_TratamientoId",
                table: "FichasDeSeguimiento");

            migrationBuilder.DropColumn(
                name: "TratamientoId",
                table: "FichasDeSeguimiento");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPase",
                table: "FichasDeSeguimiento",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
