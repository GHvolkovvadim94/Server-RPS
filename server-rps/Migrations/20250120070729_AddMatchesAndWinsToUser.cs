using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_rps.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchesAndWinsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayedMatches",
                table: "Users",
                newName: "Matches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Matches",
                table: "Users",
                newName: "PlayedMatches");
        }
    }
}
