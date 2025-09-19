using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_ChurchSubcategories_Tbl_ChurchCategories_ChurchCategoryId",
                table: "Tbl_ChurchSubcategories");

            migrationBuilder.RenameColumn(
                name: "TitleChurchSubCategory",
                table: "Tbl_ChurchSubcategories",
                newName: "TitleSubCategory");

            migrationBuilder.RenameColumn(
                name: "ChurchCategoryId",
                table: "Tbl_ChurchSubcategories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tbl_ChurchSubcategories_ChurchCategoryId",
                table: "Tbl_ChurchSubcategories",
                newName: "IX_Tbl_ChurchSubcategories_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_ChurchSubcategories_Tbl_ChurchCategories_CategoryId",
                table: "Tbl_ChurchSubcategories",
                column: "CategoryId",
                principalTable: "Tbl_ChurchCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_ChurchSubcategories_Tbl_ChurchCategories_CategoryId",
                table: "Tbl_ChurchSubcategories");

            migrationBuilder.RenameColumn(
                name: "TitleSubCategory",
                table: "Tbl_ChurchSubcategories",
                newName: "TitleChurchSubCategory");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Tbl_ChurchSubcategories",
                newName: "ChurchCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tbl_ChurchSubcategories_CategoryId",
                table: "Tbl_ChurchSubcategories",
                newName: "IX_Tbl_ChurchSubcategories_ChurchCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_ChurchSubcategories_Tbl_ChurchCategories_ChurchCategoryId",
                table: "Tbl_ChurchSubcategories",
                column: "ChurchCategoryId",
                principalTable: "Tbl_ChurchCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
