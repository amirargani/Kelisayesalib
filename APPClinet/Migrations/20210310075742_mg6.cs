using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActiveInactive",
                table: "Tbl_EventDetails",
                newName: "ActivePassive");

            migrationBuilder.RenameColumn(
                name: "ActiveInactive",
                table: "Tbl_ChurchDetails",
                newName: "ActivePassive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivePassive",
                table: "Tbl_EventDetails",
                newName: "ActiveInactive");

            migrationBuilder.RenameColumn(
                name: "ActivePassive",
                table: "Tbl_ChurchDetails",
                newName: "ActiveInactive");
        }
    }
}
