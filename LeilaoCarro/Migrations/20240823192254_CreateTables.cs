using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeilaoCarro.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Marca = table.Column<string>(type: "TEXT", nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", nullable: false),
                    Placa = table.Column<string>(type: "TEXT", nullable: true),
                    Ano = table.Column<short>(type: "INTEGER", nullable: true),
                    DataHoraLeiloado = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LanceInicial = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdLance = table.Column<int>(type: "INTEGER", nullable: true),
                    DataHoraCadastrado = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCarro = table.Column<int>(type: "INTEGER", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataHoraLance = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lance_Carro_IdCarro",
                        column: x => x.IdCarro,
                        principalTable: "Carro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lance_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carro_IdLance",
                table: "Carro",
                column: "IdLance");

            migrationBuilder.CreateIndex(
                name: "IX_Lance_IdCarro",
                table: "Lance",
                column: "IdCarro");

            migrationBuilder.CreateIndex(
                name: "IX_Lance_IdUsuario",
                table: "Lance",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Carro_Lance_IdLance",
                table: "Carro",
                column: "IdLance",
                principalTable: "Lance",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carro_Lance_IdLance",
                table: "Carro");

            migrationBuilder.DropTable(
                name: "Lance");

            migrationBuilder.DropTable(
                name: "Carro");
        }
    }
}
