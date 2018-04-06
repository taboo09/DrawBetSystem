using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BetSystem.Migrations
{
    public partial class SeedTeamTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Athletic B')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Arsenal')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Bournemouth')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Celta Vigo')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Las Palmas')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Leganes ')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Malaga')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Newcastle')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Sevilla')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Villareal')");
            migrationBuilder.Sql("INSERT INTO Teams (Name) VALUES ('Watford')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Teams");
        }
    }
}
