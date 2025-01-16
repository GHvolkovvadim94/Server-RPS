using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_rps.Migrations
{
    /// <inheritdoc />
    public partial class AddInGameToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InGame",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InGame",
                table: "Users");
        }
    }
}
