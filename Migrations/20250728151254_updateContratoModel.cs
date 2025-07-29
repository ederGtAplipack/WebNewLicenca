using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicencaApi.Migrations
{
    /// <inheritdoc />
    public partial class updateContratoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataProximoPagamento",
                table: "contrato",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimoPagamento",
                table: "contrato",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "contrato",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "StatusDescricao",
                table: "contrato",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataProximoPagamento",
                table: "contrato");

            migrationBuilder.DropColumn(
                name: "DataUltimoPagamento",
                table: "contrato");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "contrato");

            migrationBuilder.DropColumn(
                name: "StatusDescricao",
                table: "contrato");
        }
    }
}
