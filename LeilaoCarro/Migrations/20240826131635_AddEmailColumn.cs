using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeilaoCarro.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuario");
        }
    }
}
