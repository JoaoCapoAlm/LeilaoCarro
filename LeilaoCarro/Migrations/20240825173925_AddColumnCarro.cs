using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeilaoCarro.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnCarro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeletado",
                table: "Carro",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeletado",
                table: "Carro");
        }
    }
}
