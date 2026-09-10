using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoliNewsWeb.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarTextoNoticia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Noticias",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Noticias");
        }
    }
}
