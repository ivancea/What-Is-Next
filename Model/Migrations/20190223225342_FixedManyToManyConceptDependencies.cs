using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatIsNext.Migrations
{
    public partial class FixedManyToManyConceptDependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Concepts_Concepts_ConceptId",
                table: "Concepts");

            migrationBuilder.DropIndex(
                name: "IX_Concepts_ConceptId",
                table: "Concepts");

            migrationBuilder.DropColumn(
                name: "ConceptId",
                table: "Concepts");

            migrationBuilder.CreateTable(
                name: "ConceptDependency",
                columns: table => new
                {
                    ConceptId = table.Column<int>(nullable: false),
                    DependencyId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptDependency", x => new { x.ConceptId, x.DependencyId });
                    table.ForeignKey(
                        name: "FK_ConceptDependency_Concepts_ConceptId",
                        column: x => x.ConceptId,
                        principalTable: "Concepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConceptDependency_Concepts_DependencyId",
                        column: x => x.DependencyId,
                        principalTable: "Concepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConceptDependency_DependencyId",
                table: "ConceptDependency",
                column: "DependencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceptDependency");

            migrationBuilder.AddColumn<int>(
                name: "ConceptId",
                table: "Concepts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Concepts_ConceptId",
                table: "Concepts",
                column: "ConceptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Concepts_Concepts_ConceptId",
                table: "Concepts",
                column: "ConceptId",
                principalTable: "Concepts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
