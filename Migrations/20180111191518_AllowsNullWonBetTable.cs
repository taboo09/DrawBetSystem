using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BetSystem.Migrations
{
    public partial class AllowsNullWonBetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Won",
                table: "Bets",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Won",
                table: "Bets",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
