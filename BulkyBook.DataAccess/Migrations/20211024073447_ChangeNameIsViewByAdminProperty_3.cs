using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class ChangeNameIsViewByAdminProperty_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsView",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<bool>(
                name: "IsViewByAdmin",
                table: "OrderHeaders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsViewByAdmin",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<bool>(
                name: "IsView",
                table: "OrderHeaders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
