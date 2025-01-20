using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_rps.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Player1Id = table.Column<string>(type: "TEXT", nullable: false),
                    Player2Id = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Player1Health = table.Column<int>(type: "INTEGER", nullable: false),
                    Player2Health = table.Column<int>(type: "INTEGER", nullable: false),
                    Player1Ready = table.Column<bool>(type: "INTEGER", nullable: false),
                    Player2Ready = table.Column<bool>(type: "INTEGER", nullable: false),
                    Player1Choice = table.Column<string>(type: "TEXT", nullable: false),
                    Player2Choice = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentRound = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Player1Id = table.Column<string>(type: "TEXT", nullable: false),
                    Player2Id = table.Column<string>(type: "TEXT", nullable: false),
                    Rounds = table.Column<int>(type: "INTEGER", nullable: false),
                    WinnerId = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayedMatches = table.Column<int>(type: "INTEGER", nullable: false),
                    Losses = table.Column<int>(type: "INTEGER", nullable: false),
                    Wins = table.Column<int>(type: "INTEGER", nullable: false),
                    Draws = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "MatchHistories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
