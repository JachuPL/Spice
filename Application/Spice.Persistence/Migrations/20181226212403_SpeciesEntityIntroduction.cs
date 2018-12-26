using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Spice.Persistence.Migrations
{
    public partial class SpeciesEntityIntroduction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specimen",
                table: "Plants");

            migrationBuilder.AddColumn<Guid>(
                name: "SpeciesId",
                table: "Plants",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Fields",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    LatinName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_SpeciesId",
                table: "Plants",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Species_Id",
                table: "Species",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "Name", "LatinName", "Description" },
                values: new object[]
                {
                    new Guid("10000000-0000-0000-0000-000000000001"),
                    "Unknown Species",
                    "Anonymous",
                    "This species was automatically created while applying migration to database. Since it ignores domain restrictions, please move all plants from this species to another one."
                });

            migrationBuilder.Sql("UPDATE [Plants] SET [SpeciesId] = N'10000000-0000-0000-0000-000000000001'");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Species_SpeciesId",
                table: "Plants",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Species_SpeciesId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Plants_SpeciesId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "SpeciesId",
                table: "Plants");

            migrationBuilder.AddColumn<string>(
                name: "Specimen",
                table: "Plants",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Fields",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}