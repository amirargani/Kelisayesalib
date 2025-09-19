using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoFull",
                table: "Tbl_EventDetails",
                newName: "Youtube");

            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Tbl_EventDetails",
                newName: "Webseite");

            migrationBuilder.RenameColumn(
                name: "AudioFull",
                table: "Tbl_EventDetails",
                newName: "Twitter");

            migrationBuilder.RenameColumn(
                name: "Audio",
                table: "Tbl_EventDetails",
                newName: "Telegram");

            migrationBuilder.AddColumn<int>(
                name: "CountViews",
                table: "Tbl_EventDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tbl_EventDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Tbl_EventDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Tbl_EventDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Tbl_EventDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateNumber",
                table: "Tbl_EventDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountViews",
                table: "Tbl_EventDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tbl_EventDetails");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Tbl_EventDetails");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Tbl_EventDetails");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Tbl_EventDetails");

            migrationBuilder.DropColumn(
                name: "PrivateNumber",
                table: "Tbl_EventDetails");

            migrationBuilder.RenameColumn(
                name: "Youtube",
                table: "Tbl_EventDetails",
                newName: "VideoFull");

            migrationBuilder.RenameColumn(
                name: "Webseite",
                table: "Tbl_EventDetails",
                newName: "Video");

            migrationBuilder.RenameColumn(
                name: "Twitter",
                table: "Tbl_EventDetails",
                newName: "AudioFull");

            migrationBuilder.RenameColumn(
                name: "Telegram",
                table: "Tbl_EventDetails",
                newName: "Audio");
        }
    }
}
