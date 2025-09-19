using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageProfile",
                table: "Tbl_TeamDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tbl_TeamDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                table: "Tbl_TeamDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameFamily",
                table: "Tbl_TeamDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameFamilyEN",
                table: "Tbl_TeamDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service",
                table: "Tbl_TeamDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "Tbl_TeamDetails");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tbl_TeamDetails");

            migrationBuilder.DropColumn(
                name: "NameEN",
                table: "Tbl_TeamDetails");

            migrationBuilder.DropColumn(
                name: "NameFamily",
                table: "Tbl_TeamDetails");

            migrationBuilder.DropColumn(
                name: "NameFamilyEN",
                table: "Tbl_TeamDetails");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "Tbl_TeamDetails");
        }
    }
}
