using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class restriccionesaDniyFechaNacimientoagregadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CHK_Paciente_Dni_NumericAndLength",
                table: "Pacientes",
                sql: "LENGTH(\"Dni\") BETWEEN 7 AND 8 AND \"Dni\" ~ '^[0-9]+$'");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Paciente_FechaNacimiento_PastDate",
                table: "Pacientes",
                sql: "\"FechaNacimiento\"<NOW()");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Profesional_Dni_NumericAndLength",
                table: "AspNetUsers",
                sql: "LENGTH(\"Dni\") BETWEEN 7 AND 8 AND \"Dni\" ~ '^[0-9]+$'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Paciente_Dni_NumericAndLength",
                table: "Pacientes");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Paciente_FechaNacimiento_PastDate",
                table: "Pacientes");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Profesional_Dni_NumericAndLength",
                table: "AspNetUsers");
        }
    }
}
