using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BetSystem.Migrations
{
    public partial class SeedCurrencyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(("INSERT INTO Currency (IsSelected, Name, Symbol) VALUES (0, 'USD', '$')"));
            migrationBuilder.Sql(("INSERT INTO Currency (IsSelected, Name, Symbol) VALUES (0, 'EUR', '€')"));
            migrationBuilder.Sql(("INSERT INTO Currency (IsSelected, Name, Symbol) VALUES (1, 'GBP', '£')"));
            migrationBuilder.Sql(("INSERT INTO Currency (IsSelected, Name, Symbol) VALUES (0, 'NONE', ' ')"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
