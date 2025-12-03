using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionIdPacienteIdFichaDeSeguimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_FichasDeSeguimiento_FichaDeSeguimientoIdFichaSeguimi~",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Pacientes_PacienteIdPaciente",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_FichaDeSeguimientoIdFichaSeguimiento",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "FichaDeSeguimientoIdFichaSeguimiento",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "IdPaciente",
                table: "Turnos");

            migrationBuilder.RenameColumn(
                name: "PacienteIdPaciente",
                table: "Turnos",
                newName: "PacienteId");

            migrationBuilder.RenameColumn(
                name: "IdFichaDeSeguimiento",
                table: "Turnos",
                newName: "FichaDeSeguimientoID");

            migrationBuilder.RenameIndex(
                name: "IX_Turnos_PacienteIdPaciente",
                table: "Turnos",
                newName: "IX_Turnos_PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_FichaDeSeguimientoID",
                table: "Turnos",
                column: "FichaDeSeguimientoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_FichasDeSeguimiento_FichaDeSeguimientoID",
                table: "Turnos",
                column: "FichaDeSeguimientoID",
                principalTable: "FichasDeSeguimiento",
                principalColumn: "IdFichaSeguimiento");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Pacientes_PacienteId",
                table: "Turnos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "IdPaciente",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_FichasDeSeguimiento_FichaDeSeguimientoID",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Pacientes_PacienteId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_FichaDeSeguimientoID",
                table: "Turnos");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "Turnos",
                newName: "PacienteIdPaciente");

            migrationBuilder.RenameColumn(
                name: "FichaDeSeguimientoID",
                table: "Turnos",
                newName: "IdFichaDeSeguimiento");

            migrationBuilder.RenameIndex(
                name: "IX_Turnos_PacienteId",
                table: "Turnos",
                newName: "IX_Turnos_PacienteIdPaciente");

            migrationBuilder.AddColumn<int>(
                name: "FichaDeSeguimientoIdFichaSeguimiento",
                table: "Turnos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPaciente",
                table: "Turnos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_FichaDeSeguimientoIdFichaSeguimiento",
                table: "Turnos",
                column: "FichaDeSeguimientoIdFichaSeguimiento");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_FichasDeSeguimiento_FichaDeSeguimientoIdFichaSeguimi~",
                table: "Turnos",
                column: "FichaDeSeguimientoIdFichaSeguimiento",
                principalTable: "FichasDeSeguimiento",
                principalColumn: "IdFichaSeguimiento");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Pacientes_PacienteIdPaciente",
                table: "Turnos",
                column: "PacienteIdPaciente",
                principalTable: "Pacientes",
                principalColumn: "IdPaciente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
