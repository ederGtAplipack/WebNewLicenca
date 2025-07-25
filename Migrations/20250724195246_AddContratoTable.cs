using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicencaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddContratoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_licencas",
                table: "licenca");

            migrationBuilder.RenameTable(
                name: "licencas",
                newName: "licenca");

            migrationBuilder.AddColumn<string>(
                name: "id_contrato",
                table: "licenca",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_licenca",
                table: "licenca",
                column: "numLic");

            migrationBuilder.CreateTable(
                name: "contrato",
                columns: table => new
                {
                    id_contrato = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    plano = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    qtd_licencas = table.Column<int>(type: "int", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_fim = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contrato", x => x.id_contrato);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contrato");

            migrationBuilder.DropPrimaryKey(
                name: "PK_licenca",
                table: "licenca");

            migrationBuilder.DropColumn(
                name: "id_contrato",
                table: "licenca");

            migrationBuilder.RenameTable(
                name: "licenca",
                newName: "licencas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_licencas",
                table: "licenca",
                column: "numLic");
        }
    }
}
