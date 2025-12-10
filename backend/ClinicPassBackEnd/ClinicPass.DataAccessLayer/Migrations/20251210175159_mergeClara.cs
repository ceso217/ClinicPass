using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mergeClara : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasesDiarios_FichasDeSeguimiento_FichaDeSeguimientoIdFichaS~",
                table: "PasesDiarios");

            migrationBuilder.DropForeignKey(
                name: "FK_PasesDiarios_Tratamientos_TratamientoIdTratamiento",
                table: "PasesDiarios");

            migrationBuilder.DropForeignKey(
                name: "FK_PasesDiarios_Turnos_TurnoIdTurno",
                table: "PasesDiarios");

            migrationBuilder.DropTable(
                name: "HCTratamientos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PasesDiarios",
                table: "PasesDiarios");

            migrationBuilder.RenameTable(
                name: "PasesDiarios",
                newName: "Pases");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "FichasDeSeguimiento",
                newName: "FechaPase");

            migrationBuilder.RenameIndex(
                name: "IX_PasesDiarios_TurnoIdTurno",
                table: "Pases",
                newName: "IX_Pases_TurnoIdTurno");

            migrationBuilder.RenameIndex(
                name: "IX_PasesDiarios_TratamientoIdTratamiento",
                table: "Pases",
                newName: "IX_Pases_TratamientoIdTratamiento");

            migrationBuilder.RenameIndex(
                name: "IX_PasesDiarios_FichaDeSeguimientoIdFichaSeguimiento",
                table: "Pases",
                newName: "IX_Pases_FichaDeSeguimientoIdFichaSeguimiento");

            migrationBuilder.AlterColumn<string>(
                name: "TipoTratamiento",
                table: "Tratamientos",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "Tratamientos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "PacienteTratamientos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "PacienteTratamientos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "FichasDeSeguimiento",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HistorialClinicoId",
                table: "FichasDeSeguimiento",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pases",
                table: "Pases",
                columns: new[] { "IdTratamiento", "IdTurno", "IdFichaSeguimiento" });

            migrationBuilder.CreateTable(
                name: "HistorialClinicoTratamiento",
                columns: table => new
                {
                    IdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    IdHistorialClinico = table.Column<int>(type: "integer", nullable: false),
                    TratamientoIdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    HistoriaClinicaIdHistorialClinico = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialClinicoTratamiento", x => new { x.IdTratamiento, x.IdHistorialClinico });
                    table.ForeignKey(
                        name: "FK_HistorialClinicoTratamiento_HistoriasClinicas_HistoriaClini~",
                        column: x => x.HistoriaClinicaIdHistorialClinico,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "IdHistorialClinico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialClinicoTratamiento_Tratamientos_TratamientoIdTrata~",
                        column: x => x.TratamientoIdTratamiento,
                        principalTable: "Tratamientos",
                        principalColumn: "IdTratamiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialClinicoTratamiento_HistoriaClinicaIdHistorialClini~",
                table: "HistorialClinicoTratamiento",
                column: "HistoriaClinicaIdHistorialClinico");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialClinicoTratamiento_TratamientoIdTratamiento",
                table: "HistorialClinicoTratamiento",
                column: "TratamientoIdTratamiento");

            migrationBuilder.AddForeignKey(
                name: "FK_Pases_FichasDeSeguimiento_FichaDeSeguimientoIdFichaSeguimie~",
                table: "Pases",
                column: "FichaDeSeguimientoIdFichaSeguimiento",
                principalTable: "FichasDeSeguimiento",
                principalColumn: "IdFichaSeguimiento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pases_Tratamientos_TratamientoIdTratamiento",
                table: "Pases",
                column: "TratamientoIdTratamiento",
                principalTable: "Tratamientos",
                principalColumn: "IdTratamiento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pases_Turnos_TurnoIdTurno",
                table: "Pases",
                column: "TurnoIdTurno",
                principalTable: "Turnos",
                principalColumn: "IdTurno",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pases_FichasDeSeguimiento_FichaDeSeguimientoIdFichaSeguimie~",
                table: "Pases");

            migrationBuilder.DropForeignKey(
                name: "FK_Pases_Tratamientos_TratamientoIdTratamiento",
                table: "Pases");

            migrationBuilder.DropForeignKey(
                name: "FK_Pases_Turnos_TurnoIdTurno",
                table: "Pases");

            migrationBuilder.DropTable(
                name: "HistorialClinicoTratamiento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pases",
                table: "Pases");

            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "Tratamientos");

            migrationBuilder.RenameTable(
                name: "Pases",
                newName: "PasesDiarios");

            migrationBuilder.RenameColumn(
                name: "FechaPase",
                table: "FichasDeSeguimiento",
                newName: "Fecha");

            migrationBuilder.RenameIndex(
                name: "IX_Pases_TurnoIdTurno",
                table: "PasesDiarios",
                newName: "IX_PasesDiarios_TurnoIdTurno");

            migrationBuilder.RenameIndex(
                name: "IX_Pases_TratamientoIdTratamiento",
                table: "PasesDiarios",
                newName: "IX_PasesDiarios_TratamientoIdTratamiento");

            migrationBuilder.RenameIndex(
                name: "IX_Pases_FichaDeSeguimientoIdFichaSeguimiento",
                table: "PasesDiarios",
                newName: "IX_PasesDiarios_FichaDeSeguimientoIdFichaSeguimiento");

            migrationBuilder.AlterColumn<int>(
                name: "TipoTratamiento",
                table: "Tratamientos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "PacienteTratamientos",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "PacienteTratamientos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "FichasDeSeguimiento",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "HistorialClinicoId",
                table: "FichasDeSeguimiento",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasesDiarios",
                table: "PasesDiarios",
                columns: new[] { "IdTratamiento", "IdTurno", "IdFichaSeguimiento" });

            migrationBuilder.CreateTable(
                name: "HCTratamientos",
                columns: table => new
                {
                    IdTratamiento = table.Column<int>(type: "integer", nullable: false),
                    IdHistorialClinico = table.Column<int>(type: "integer", nullable: false),
                    HistoriaClinicaIdHistorialClinico = table.Column<int>(type: "integer", nullable: false),
                    TratamientoIdTratamiento = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_HCTratamientos_HistoriaClinicaIdHistorialClinico",
                table: "HCTratamientos",
                column: "HistoriaClinicaIdHistorialClinico");

            migrationBuilder.CreateIndex(
                name: "IX_HCTratamientos_TratamientoIdTratamiento",
                table: "HCTratamientos",
                column: "TratamientoIdTratamiento");

            migrationBuilder.AddForeignKey(
                name: "FK_PasesDiarios_FichasDeSeguimiento_FichaDeSeguimientoIdFichaS~",
                table: "PasesDiarios",
                column: "FichaDeSeguimientoIdFichaSeguimiento",
                principalTable: "FichasDeSeguimiento",
                principalColumn: "IdFichaSeguimiento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PasesDiarios_Tratamientos_TratamientoIdTratamiento",
                table: "PasesDiarios",
                column: "TratamientoIdTratamiento",
                principalTable: "Tratamientos",
                principalColumn: "IdTratamiento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PasesDiarios_Turnos_TurnoIdTurno",
                table: "PasesDiarios",
                column: "TurnoIdTurno",
                principalTable: "Turnos",
                principalColumn: "IdTurno",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
