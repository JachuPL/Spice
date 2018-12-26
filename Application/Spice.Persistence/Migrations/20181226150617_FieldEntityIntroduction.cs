using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Spice.Persistence.Migrations
{
    public partial class FieldEntityIntroduction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "Plants");

            migrationBuilder.AddColumn<Guid>(
                name: "FieldId",
                table: "Plants",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longtitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_FieldId",
                table: "Plants",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_Id",
                table: "Fields",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Fields",
                columns: new[] { "Id", "Name", "Description", "Latitude", "Longtitude" },
                values: new object[]
                {
                    new Guid("10000000-0000-0000-0000-000000000001"),
                    "Unknown Field",
                    "This field was automatically created while applying migration to database. Since it ignores domain restrictions, please move all plants from this field to another one.",
                    0.0,
                    0.0
                });

            migrationBuilder.Sql("UPDATE [Plants] SET [FieldId] = N'10000000-0000-0000-0000-000000000001'");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Fields_FieldId",
                table: "Plants",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Fields_FieldId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Plants_FieldId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Plants");

            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "Plants",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}