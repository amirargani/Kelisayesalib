using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Church_Details");

            migrationBuilder.DropTable(
                name: "Tbl_Church_Subcategories");

            migrationBuilder.DropTable(
                name: "Tbl_Church_Categories");

            migrationBuilder.CreateTable(
                name: "Tbl_ChurchCategories",
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
                    table.PrimaryKey("PK_Tbl_ChurchCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ChurchSubcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TitleChurchSubCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ActivePassive = table.Column<bool>(type: "bit", nullable: false),
                    FontName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ChurchCategoryId = table.Column<int>(type: "int", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ChurchSubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_ChurchSubcategories_Tbl_ChurchCategories_ChurchCategoryId",
                        column: x => x.ChurchCategoryId,
                        principalTable: "Tbl_ChurchCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ChurchDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    ActiveInactive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ChurchDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_ChurchDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_ChurchDetails_Tbl_ChurchCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_ChurchCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_ChurchDetails_Tbl_ChurchSubcategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Tbl_ChurchSubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ChurchDetails_CategoryId",
                table: "Tbl_ChurchDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ChurchDetails_SubCategoryId",
                table: "Tbl_ChurchDetails",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ChurchDetails_UserId",
                table: "Tbl_ChurchDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ChurchSubcategories_ChurchCategoryId",
                table: "Tbl_ChurchSubcategories",
                column: "ChurchCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_ChurchDetails");

            migrationBuilder.DropTable(
                name: "Tbl_ChurchSubcategories");

            migrationBuilder.DropTable(
                name: "Tbl_ChurchCategories");

            migrationBuilder.CreateTable(
                name: "Tbl_Church_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivePassive = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FontName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    TitleCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Church_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Church_Subcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivePassive = table.Column<bool>(type: "bit", nullable: false),
                    ChurchCategoryId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FontName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    TitleChurchSubCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Church_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Church_Subcategories_Tbl_Church_Categories_ChurchCategoryId",
                        column: x => x.ChurchCategoryId,
                        principalTable: "Tbl_Church_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Church_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveInactive = table.Column<bool>(type: "bit", nullable: false),
                    Audio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Church_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Church_Details_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Church_Details_Tbl_Church_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_Church_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Church_Details_Tbl_Church_Subcategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Tbl_Church_Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Church_Details_CategoryId",
                table: "Tbl_Church_Details",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Church_Details_SubCategoryId",
                table: "Tbl_Church_Details",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Church_Details_UserId",
                table: "Tbl_Church_Details",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Church_Subcategories_ChurchCategoryId",
                table: "Tbl_Church_Subcategories",
                column: "ChurchCategoryId");
        }
    }
}
