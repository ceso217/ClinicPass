using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class EliminaciontablaintermediaProfesionalTurnos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfesionalTurnos");

            migrationBuilder.AddColumn<int>(
                name: "ProfesionalId",
                table: "Turnos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_ProfesionalId",
                table: "Turnos",
                column: "ProfesionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_AspNetUsers_ProfesionalId",
                table: "Turnos",
                column: "ProfesionalId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_AspNetUsers_ProfesionalId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_ProfesionalId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "ProfesionalId",
                table: "Turnos");

            migrationBuilder.CreateTable(
                name: "ProfesionalTurnos",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdTurno = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesionalTurnos", x => new { x.IdUsuario, x.IdTurno });
                    table.ForeignKey(
                        name: "FK_ProfesionalTurnos_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesionalTurnos_Turnos_IdTurno",
                        column: x => x.IdTurno,
                        principalTable: "Turnos",
                        principalColumn: "IdTurno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionalTurnos_IdTurno",
                table: "ProfesionalTurnos",
                column: "IdTurno");
        }
    }
}
