using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class AddCreateDateToTicketAndTicketMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "TicketMessages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "TicketMessages");
        }
    }
}
