using Microsoft.EntityFrameworkCore.Migrations;

namespace Spice.Persistence.Migrations
{
    public partial class FieldForAutomaticallyCreatedEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CreatedAutomatically",
                table: "Events",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAutomatically",
                table: "Events");
        }
    }
}
