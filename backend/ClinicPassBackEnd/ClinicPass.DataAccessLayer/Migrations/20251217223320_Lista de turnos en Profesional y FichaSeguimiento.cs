using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ListadeturnosenProfesionalyFichaSeguimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Turnos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfesionalId",
                table: "FichasDeSeguimiento",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FichasDeSeguimiento_ProfesionalId",
                table: "FichasDeSeguimiento",
                column: "ProfesionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_FichasDeSeguimiento_AspNetUsers_ProfesionalId",
                table: "FichasDeSeguimiento",
                column: "ProfesionalId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichasDeSeguimiento_AspNetUsers_ProfesionalId",
                table: "FichasDeSeguimiento");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Turno_Fecha_FutureDate",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_FichasDeSeguimiento_ProfesionalId",
                table: "FichasDeSeguimiento");

            migrationBuilder.DropColumn(
                name: "ProfesionalId",
                table: "FichasDeSeguimiento");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Turnos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
