using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeEvent",
                table: "Tbl_EventDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeEvent",
                table: "Tbl_EventDetails");
        }
    }
}
