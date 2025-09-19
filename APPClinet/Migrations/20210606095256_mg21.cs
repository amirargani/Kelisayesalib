using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_NewCategories",
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
                    table.PrimaryKey("PK_Tbl_NewCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_NewSubCategories",
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
                    table.PrimaryKey("PK_Tbl_NewSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_NewSubCategories_Tbl_NewCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_NewCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_NewDetails",
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
                    table.PrimaryKey("PK_Tbl_NewDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_NewDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_NewDetails_Tbl_NewCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_NewCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_NewDetails_Tbl_NewSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Tbl_NewSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_NewImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Tbl_NewImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_NewImages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_NewImages_Tbl_NewCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_NewCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_NewImages_Tbl_NewDetails_DetailId",
                        column: x => x.DetailId,
                        principalTable: "Tbl_NewDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_NewImages_Tbl_NewSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Tbl_NewSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NewDetails_CategoryId",
                table: "Tbl_NewDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NewDetails_SubCategoryId",
                table: "Tbl_NewDetails",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NewDetails_UserId",
                table: "Tbl_NewDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NewImages_CategoryId",
                table: "Tbl_NewImages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NewImages_DetailId",
                table: "Tbl_NewImages",
                column: "DetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NewImages_SubCategoryId",
                table: "Tbl_NewImages",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NewImages_UserId",
                table: "Tbl_NewImages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NewSubCategories_CategoryId",
                table: "Tbl_NewSubCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_NewImages");

            migrationBuilder.DropTable(
                name: "Tbl_NewDetails");

            migrationBuilder.DropTable(
                name: "Tbl_NewSubCategories");

            migrationBuilder.DropTable(
                name: "Tbl_NewCategories");
        }
    }
}
