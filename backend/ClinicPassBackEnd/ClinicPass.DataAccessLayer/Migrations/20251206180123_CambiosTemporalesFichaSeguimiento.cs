using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.Migrations
{
    /// <inheritdoc />
    public partial class CambiosTemporalesFichaSeguimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichasDeSeguimiento_HistoriasClinicas_HistoriaClinicaIdHist~",
                table: "FichasDeSeguimiento");

            migrationBuilder.DropForeignKey(
                name: "FK_FichasDeSeguimiento_Profesionales_ProfesionalIdUsuario",
                table: "FichasDeSeguimiento");

            migrationBuilder.AlterColumn<int>(
                name: "ProfesionalIdUsuario",
                table: "FichasDeSeguimiento",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "HistoriaClinicaIdHistorialClinico",
                table: "FichasDeSeguimiento",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_FichasDeSeguimiento_HistoriasClinicas_HistoriaClinicaIdHist~",
                table: "FichasDeSeguimiento",
                column: "HistoriaClinicaIdHistorialClinico",
                principalTable: "HistoriasClinicas",
                principalColumn: "IdHistorialClinico");

            migrationBuilder.AddForeignKey(
                name: "FK_FichasDeSeguimiento_Profesionales_ProfesionalIdUsuario",
                table: "FichasDeSeguimiento",
                column: "ProfesionalIdUsuario",
                principalTable: "Profesionales",
                principalColumn: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichasDeSeguimiento_HistoriasClinicas_HistoriaClinicaIdHist~",
                table: "FichasDeSeguimiento");

            migrationBuilder.DropForeignKey(
                name: "FK_FichasDeSeguimiento_Profesionales_ProfesionalIdUsuario",
                table: "FichasDeSeguimiento");

            migrationBuilder.AlterColumn<int>(
                name: "ProfesionalIdUsuario",
                table: "FichasDeSeguimiento",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HistoriaClinicaIdHistorialClinico",
                table: "FichasDeSeguimiento",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FichasDeSeguimiento_HistoriasClinicas_HistoriaClinicaIdHist~",
                table: "FichasDeSeguimiento",
                column: "HistoriaClinicaIdHistorialClinico",
                principalTable: "HistoriasClinicas",
                principalColumn: "IdHistorialClinico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FichasDeSeguimiento_Profesionales_ProfesionalIdUsuario",
                table: "FichasDeSeguimiento",
                column: "ProfesionalIdUsuario",
                principalTable: "Profesionales",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
