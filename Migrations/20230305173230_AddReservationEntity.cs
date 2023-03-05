using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskBookingAPI.Migrations
{
    public partial class AddReservationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desks_Employees_EmployeeId",
                table: "Desks");

            migrationBuilder.DropIndex(
                name: "IX_Desks_EmployeeId",
                table: "Desks");

            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Desks");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Desks");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Desks");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeskId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Desks_DeskId",
                        column: x => x.DeskId,
                        principalTable: "Desks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DeskId",
                table: "Reservations",
                column: "DeskId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EmployeeId",
                table: "Reservations",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Desks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Desks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Desks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Desks_EmployeeId",
                table: "Desks",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Desks_Employees_EmployeeId",
                table: "Desks",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
