using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClinicPass.Migrations
{
    /// <inheritdoc />
    public partial class ProfesionalFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichasDeSeguimiento_Profesionales_ProfesionalIdUsuario",
                table: "FichasDeSeguimiento");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesionalPacientes_Profesionales_ProfesionalIdUsuario",
                table: "ProfesionalPacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesionalTurnos_Profesionales_ProfesionalIdUsuario",
                table: "ProfesionalTurnos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profesionales",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Profesionales");

            migrationBuilder.RenameColumn(
                name: "ProfesionalIdUsuario",
                table: "ProfesionalTurnos",
                newName: "ProfesionalId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfesionalTurnos_ProfesionalIdUsuario",
                table: "ProfesionalTurnos",
                newName: "IX_ProfesionalTurnos_ProfesionalId");

            migrationBuilder.RenameColumn(
                name: "ProfesionalIdUsuario",
                table: "ProfesionalPacientes",
                newName: "ProfesionalId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfesionalPacientes_ProfesionalIdUsuario",
                table: "ProfesionalPacientes",
                newName: "IX_ProfesionalPacientes_ProfesionalId");

            migrationBuilder.RenameColumn(
                name: "ProfesionalIdUsuario",
                table: "FichasDeSeguimiento",
                newName: "ProfesionalId");

            migrationBuilder.RenameIndex(
                name: "IX_FichasDeSeguimiento_ProfesionalIdUsuario",
                table: "FichasDeSeguimiento",
                newName: "IX_FichasDeSeguimiento_ProfesionalId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Profesionales",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profesionales",
                table: "Profesionales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FichasDeSeguimiento_Profesionales_ProfesionalId",
                table: "FichasDeSeguimiento",
                column: "ProfesionalId",
                principalTable: "Profesionales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesionalPacientes_Profesionales_ProfesionalId",
                table: "ProfesionalPacientes",
                column: "ProfesionalId",
                principalTable: "Profesionales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesionalTurnos_Profesionales_ProfesionalId",
                table: "ProfesionalTurnos",
                column: "ProfesionalId",
                principalTable: "Profesionales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichasDeSeguimiento_Profesionales_ProfesionalId",
                table: "FichasDeSeguimiento");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesionalPacientes_Profesionales_ProfesionalId",
                table: "ProfesionalPacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesionalTurnos_Profesionales_ProfesionalId",
                table: "ProfesionalTurnos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profesionales",
                table: "Profesionales");

            migrationBuilder.RenameColumn(
                name: "ProfesionalId",
                table: "ProfesionalTurnos",
                newName: "ProfesionalIdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_ProfesionalTurnos_ProfesionalId",
                table: "ProfesionalTurnos",
                newName: "IX_ProfesionalTurnos_ProfesionalIdUsuario");

            migrationBuilder.RenameColumn(
                name: "ProfesionalId",
                table: "ProfesionalPacientes",
                newName: "ProfesionalIdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_ProfesionalPacientes_ProfesionalId",
                table: "ProfesionalPacientes",
                newName: "IX_ProfesionalPacientes_ProfesionalIdUsuario");

            migrationBuilder.RenameColumn(
                name: "ProfesionalId",
                table: "FichasDeSeguimiento",
                newName: "ProfesionalIdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_FichasDeSeguimiento_ProfesionalId",
                table: "FichasDeSeguimiento",
                newName: "IX_FichasDeSeguimiento_ProfesionalIdUsuario");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Profesionales",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Profesionales",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profesionales",
                table: "Profesionales",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_FichasDeSeguimiento_Profesionales_ProfesionalIdUsuario",
                table: "FichasDeSeguimiento",
                column: "ProfesionalIdUsuario",
                principalTable: "Profesionales",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesionalPacientes_Profesionales_ProfesionalIdUsuario",
                table: "ProfesionalPacientes",
                column: "ProfesionalIdUsuario",
                principalTable: "Profesionales",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesionalTurnos_Profesionales_ProfesionalIdUsuario",
                table: "ProfesionalTurnos",
                column: "ProfesionalIdUsuario",
                principalTable: "Profesionales",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
