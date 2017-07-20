using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Skeleton.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) Values ('Audi')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) Values ('BMW')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) Values ('Mercedes')");
            
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('A3', (SELECT ID FROM Makes WHERE Name = 'Audi'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('A5', (SELECT ID FROM Makes WHERE Name = 'Audi'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('A6', (SELECT ID FROM Makes WHERE Name = 'Audi'))");
            
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('1-Series', (SELECT ID FROM Makes WHERE Name = 'BMW'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('3-Series', (SELECT ID FROM Makes WHERE Name = 'BMW'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('5-Series', (SELECT ID FROM Makes WHERE Name = 'BMW'))");
            
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('A-Class', (SELECT ID FROM Makes WHERE Name = 'Mercedes'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('C-Class', (SELECT ID FROM Makes WHERE Name = 'Mercedes'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('E-Class', (SELECT ID FROM Makes WHERE Name = 'Mercedes'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Audi', 'BMW', 'Mercedes')");
        }
    }
}
