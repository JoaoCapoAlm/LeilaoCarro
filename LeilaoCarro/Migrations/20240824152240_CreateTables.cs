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
                    DataHoraCadastrado = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Sigla = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Documento = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "UsuarioEndereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    Cep = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    IdEstado = table.Column<byte>(type: "INTEGER", nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", nullable: false),
                    Logradouro = table.Column<string>(type: "TEXT", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Complemento = table.Column<string>(type: "TEXT", nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioEndereco_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioEndereco_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lance_IdCarro",
                table: "Lance",
                column: "IdCarro");

            migrationBuilder.CreateIndex(
                name: "IX_Lance_IdUsuario",
                table: "Lance",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEndereco_IdEstado",
                table: "UsuarioEndereco",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEndereco_IdUsuario",
                table: "UsuarioEndereco",
                column: "IdUsuario");

            migrationBuilder.InsertData(
                "Estado",
                ["Id", "Nome", "Sigla"],
                new object[,]
                {
                    { 1, "Acre", "AC" },
                    { 2, "Alagoas", "AL" },
                    { 3, "Amapá", "AP" },
                    { 4, "Amazonas", "AM" },
                    { 5, "Bahia", "BA" },
                    { 6, "Ceará", "CE" },
                    { 7, "Distrito Federal", "DF" },
                    { 8, "Espírito Santo", "ES" },
                    { 9, "Goiás", "GO" },
                    { 10, "Maranhão", "MA" },
                    { 11, "Mato Grosso", "MT" },
                    { 12, "Mato Grosso do Sul", "MS" },
                    { 13, "Minas Gerais", "MG" },
                    { 14, "Pará", "PA" },
                    { 15, "Paraíba", "PB" },
                    { 16, "Paraná", "PR" },
                    { 17, "Pernambuco", "PE" },
                    { 18, "Piauí", "PI" },
                    { 19, "Rio de Janeiro", "RJ" },
                    { 20, "Rio Grande do Norte", "RN" },
                    { 21, "Rio Grande do Sul", "RS" },
                    { 22, "Rondônia", "RO" },
                    { 23, "Roraima", "RR" },
                    { 24, "Santa Catarina", "SC" },
                    { 25, "São Paulo", "SP" },
                    { 26, "Sergipe", "SE" },
                    { 27, "Tocantins", "TO" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lance");

            migrationBuilder.DropTable(
                name: "UsuarioEndereco");

            migrationBuilder.DropTable(
                name: "Carro");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
