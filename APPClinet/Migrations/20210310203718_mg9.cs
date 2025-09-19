using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Countdown",
                table: "Tbl_EventDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartAt",
                table: "Tbl_EventDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StopAt",
                table: "Tbl_EventDetails",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Countdown",
                table: "Tbl_EventDetails");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Tbl_EventDetails");

            migrationBuilder.DropColumn(
                name: "StopAt",
                table: "Tbl_EventDetails");
        }
    }
}
