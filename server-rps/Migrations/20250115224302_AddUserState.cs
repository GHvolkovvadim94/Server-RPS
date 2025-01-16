using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_rps.Migrations
{
    /// <inheritdoc />
    public partial class AddUserState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InGame",
                table: "Users",
                newName: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Users",
                newName: "InGame");
        }
    }
}
