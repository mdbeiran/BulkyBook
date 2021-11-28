using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class AddSiteSettingToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    SettingId = table.Column<int>(nullable: false),
                    SiteName = table.Column<string>(maxLength: 100, nullable: false),
                    SiteDescription = table.Column<string>(nullable: false),
                    SiteEmail = table.Column<string>(maxLength: 256, nullable: false),
                    SiteTell = table.Column<string>(maxLength: 250, nullable: false),
                    SiteMobile = table.Column<string>(maxLength: 200, nullable: false),
                    SiteFax = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    SiteRules = table.Column<string>(nullable: true),
                    CopyRight = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.SettingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSettings");
        }
    }
}
