using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Audio",
                table: "Tbl_ChurchDetails");

            migrationBuilder.DropColumn(
                name: "AudioFull",
                table: "Tbl_ChurchDetails");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Tbl_ChurchDetails");

            migrationBuilder.DropColumn(
                name: "VideoFull",
                table: "Tbl_ChurchDetails");

            migrationBuilder.DropColumn(
                name: "YoutubeLink",
                table: "Tbl_ChurchDetails");

            migrationBuilder.AddColumn<int>(
                name: "CountViews",
                table: "Tbl_ChurchDetails",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountViews",
                table: "Tbl_ChurchDetails");

            migrationBuilder.AddColumn<string>(
                name: "Audio",
                table: "Tbl_ChurchDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AudioFull",
                table: "Tbl_ChurchDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Tbl_ChurchDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoFull",
                table: "Tbl_ChurchDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeLink",
                table: "Tbl_ChurchDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
