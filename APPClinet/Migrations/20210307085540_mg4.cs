using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioFull",
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

            migrationBuilder.CreateTable(
                name: "Tbl_EventCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TitleCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ActivePassive = table.Column<bool>(type: "bit", nullable: false),
                    FontName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_EventCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_EventSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TitleSubCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ActivePassive = table.Column<bool>(type: "bit", nullable: false),
                    FontName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_EventSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_EventSubCategories_Tbl_EventCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_EventCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_EventDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoFull = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AudioFull = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoutubeLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    ActiveInactive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_EventDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_EventDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_EventDetails_Tbl_EventCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_EventCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_EventDetails_Tbl_EventSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Tbl_EventSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_EventDetails_CategoryId",
                table: "Tbl_EventDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_EventDetails_SubCategoryId",
                table: "Tbl_EventDetails",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_EventDetails_UserId",
                table: "Tbl_EventDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_EventSubCategories_CategoryId",
                table: "Tbl_EventSubCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_EventDetails");

            migrationBuilder.DropTable(
                name: "Tbl_EventSubCategories");

            migrationBuilder.DropTable(
                name: "Tbl_EventCategories");

            migrationBuilder.DropColumn(
                name: "AudioFull",
                table: "Tbl_ChurchDetails");

            migrationBuilder.DropColumn(
                name: "VideoFull",
                table: "Tbl_ChurchDetails");

            migrationBuilder.DropColumn(
                name: "YoutubeLink",
                table: "Tbl_ChurchDetails");
        }
    }
}
