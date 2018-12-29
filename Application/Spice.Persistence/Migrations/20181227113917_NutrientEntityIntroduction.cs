using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spice.Persistence.Migrations
{
    public partial class NutrientEntityIntroduction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nutrients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    DosageUnits = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nutrients_Id",
                table: "Nutrients",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nutrients");
        }
    }
}
