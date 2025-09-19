using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountViews",
                table: "Tbl_TeamSubCategories",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountViews",
                table: "Tbl_TeamSubCategories");
        }
    }
}
