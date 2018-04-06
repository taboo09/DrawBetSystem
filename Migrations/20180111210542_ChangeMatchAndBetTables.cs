using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BetSystem.Migrations
{
    public partial class ChangeMatchAndBetTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashReturn",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "Stake",
                table: "Bets");

            migrationBuilder.AddColumn<double>(
                name: "CashReturn",
                table: "Matches",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Stake",
                table: "Matches",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashReturn",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Stake",
                table: "Matches");

            migrationBuilder.AddColumn<double>(
                name: "CashReturn",
                table: "Bets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Stake",
                table: "Bets",
                nullable: false,
                defaultValue: 0);
        }
    }
}
