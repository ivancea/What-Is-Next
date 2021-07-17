using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatIsNext.Migrations
{
    public partial class Addedconceptlevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Concepts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Concepts");
        }
    }
}
