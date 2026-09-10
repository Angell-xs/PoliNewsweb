using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoliNewsWeb.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCurtidasPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Curtidas",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Curtidas",
                table: "Posts");
        }
    }
}
