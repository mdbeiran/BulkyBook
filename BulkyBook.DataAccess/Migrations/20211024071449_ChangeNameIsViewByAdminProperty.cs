using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class ChangeNameIsViewByAdminProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsViewByAdmin",
                table: "OrderHeaders",
                newName: "IsViewbyadmin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsViewbyadmin",
                table: "OrderHeaders",
                newName: "IsViewByAdmin");
        }
    }
}
