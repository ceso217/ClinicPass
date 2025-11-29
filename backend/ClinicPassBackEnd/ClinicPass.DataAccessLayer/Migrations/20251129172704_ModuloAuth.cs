using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPass.Migrations
{
    /// <inheritdoc />
    public partial class ModuloAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Profesionales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Profesionales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Profesionales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Profesionales",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Profesionales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Profesionales",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Profesionales",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Profesionales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Profesionales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Profesionales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Profesionales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Profesionales",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Profesionales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Profesionales",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Profesionales",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Profesionales");
        }
    }
}
