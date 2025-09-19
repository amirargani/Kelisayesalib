using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class mg20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_GalleryIamges");

            migrationBuilder.CreateTable(
                name: "Tbl_GalleryImages",
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
                    table.PrimaryKey("PK_Tbl_GalleryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_GalleryImages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_GalleryImages_Tbl_GalleryCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_GalleryCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_GalleryImages_Tbl_GalleryDetails_DetailId",
                        column: x => x.DetailId,
                        principalTable: "Tbl_GalleryDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_GalleryImages_Tbl_GallerySubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Tbl_GallerySubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_GalleryImages_CategoryId",
                table: "Tbl_GalleryImages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_GalleryImages_DetailId",
                table: "Tbl_GalleryImages",
                column: "DetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_GalleryImages_SubCategoryId",
                table: "Tbl_GalleryImages",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_GalleryImages_UserId",
                table: "Tbl_GalleryImages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_GalleryImages");

            migrationBuilder.CreateTable(
                name: "Tbl_GalleryIamges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivePassive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CountViews = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DetailId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_GalleryIamges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_GalleryIamges_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_GalleryIamges_Tbl_GalleryCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_GalleryCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_GalleryIamges_Tbl_GalleryDetails_DetailId",
                        column: x => x.DetailId,
                        principalTable: "Tbl_GalleryDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_GalleryIamges_Tbl_GallerySubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Tbl_GallerySubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_GalleryIamges_CategoryId",
                table: "Tbl_GalleryIamges",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_GalleryIamges_DetailId",
                table: "Tbl_GalleryIamges",
                column: "DetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_GalleryIamges_SubCategoryId",
                table: "Tbl_GalleryIamges",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_GalleryIamges_UserId",
                table: "Tbl_GalleryIamges",
                column: "UserId");
        }
    }
}
