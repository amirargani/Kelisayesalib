using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_CourseCategories",
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
                    table.PrimaryKey("PK_Tbl_CourseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_CourseSubCategories",
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
                    Position = table.Column<int>(type: "int", nullable: true),
                    CountViews = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_CourseSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_CourseSubCategories_Tbl_CourseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_CourseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_CourseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telegram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Youtube = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoutubeLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmbedLinkGoogleMap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    ActivePassive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    CountViews = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_CourseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_CourseDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_CourseDetails_Tbl_CourseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_CourseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_CourseDetails_Tbl_CourseSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Tbl_CourseSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_CourseVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    DetailId = table.Column<int>(type: "int", nullable: true),
                    ActivePassive = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: true),
                    CountViews = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_CourseVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_CourseVideos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_CourseVideos_Tbl_CourseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_CourseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_CourseVideos_Tbl_CourseDetails_DetailId",
                        column: x => x.DetailId,
                        principalTable: "Tbl_CourseDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_CourseVideos_Tbl_CourseSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Tbl_CourseSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_CourseDetails_CategoryId",
                table: "Tbl_CourseDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_CourseDetails_SubCategoryId",
                table: "Tbl_CourseDetails",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_CourseDetails_UserId",
                table: "Tbl_CourseDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_CourseSubCategories_CategoryId",
                table: "Tbl_CourseSubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_CourseVideos_CategoryId",
                table: "Tbl_CourseVideos",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_CourseVideos_DetailId",
                table: "Tbl_CourseVideos",
                column: "DetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_CourseVideos_SubCategoryId",
                table: "Tbl_CourseVideos",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_CourseVideos_UserId",
                table: "Tbl_CourseVideos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_CourseVideos");

            migrationBuilder.DropTable(
                name: "Tbl_CourseDetails");

            migrationBuilder.DropTable(
                name: "Tbl_CourseSubCategories");

            migrationBuilder.DropTable(
                name: "Tbl_CourseCategories");
        }
    }
}
