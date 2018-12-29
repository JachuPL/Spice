using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spice.Persistence.Migrations
{
    public partial class TrackNutrientsAdministeredToPlants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdministeredNutrients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlantId = table.Column<Guid>(nullable: false),
                    NutrientId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministeredNutrients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministeredNutrients_Nutrients_NutrientId",
                        column: x => x.NutrientId,
                        principalTable: "Nutrients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdministeredNutrients_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdministeredNutrients_Id",
                table: "AdministeredNutrients",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AdministeredNutrients_NutrientId",
                table: "AdministeredNutrients",
                column: "NutrientId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministeredNutrients_PlantId",
                table: "AdministeredNutrients",
                column: "PlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdministeredNutrients");
        }
    }
}
