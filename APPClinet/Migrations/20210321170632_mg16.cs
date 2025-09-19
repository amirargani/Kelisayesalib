using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameFamilyEN",
                table: "Tbl_TeamDetails",
                newName: "LastNameEN");

            migrationBuilder.RenameColumn(
                name: "NameFamily",
                table: "Tbl_TeamDetails",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "NameEN",
                table: "Tbl_TeamDetails",
                newName: "FirstNameEN");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tbl_TeamDetails",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastNameEN",
                table: "Tbl_TeamDetails",
                newName: "NameFamilyEN");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Tbl_TeamDetails",
                newName: "NameFamily");

            migrationBuilder.RenameColumn(
                name: "FirstNameEN",
                table: "Tbl_TeamDetails",
                newName: "NameEN");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Tbl_TeamDetails",
                newName: "Name");
        }
    }
}
