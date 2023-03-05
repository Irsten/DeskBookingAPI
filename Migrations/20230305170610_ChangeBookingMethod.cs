using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskBookingAPI.Migrations
{
    public partial class ChangeBookingMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "Desks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAvailable",
                table: "Desks",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }
    }
}
