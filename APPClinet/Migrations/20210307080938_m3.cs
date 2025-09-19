using Microsoft.EntityFrameworkCore.Migrations;

namespace APPClinet.Migrations
{
    public partial class m3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_ChurchDetails_Tbl_ChurchSubcategories_SubCategoryId",
                table: "Tbl_ChurchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_ChurchSubcategories_Tbl_ChurchCategories_CategoryId",
                table: "Tbl_ChurchSubcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbl_ChurchSubcategories",
                table: "Tbl_ChurchSubcategories");

            migrationBuilder.RenameTable(
                name: "Tbl_ChurchSubcategories",
                newName: "Tbl_ChurchSubCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Tbl_ChurchSubcategories_CategoryId",
                table: "Tbl_ChurchSubCategories",
                newName: "IX_Tbl_ChurchSubCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbl_ChurchSubCategories",
                table: "Tbl_ChurchSubCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_ChurchDetails_Tbl_ChurchSubCategories_SubCategoryId",
                table: "Tbl_ChurchDetails",
                column: "SubCategoryId",
                principalTable: "Tbl_ChurchSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_ChurchSubCategories_Tbl_ChurchCategories_CategoryId",
                table: "Tbl_ChurchSubCategories",
                column: "CategoryId",
                principalTable: "Tbl_ChurchCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_ChurchDetails_Tbl_ChurchSubCategories_SubCategoryId",
                table: "Tbl_ChurchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_ChurchSubCategories_Tbl_ChurchCategories_CategoryId",
                table: "Tbl_ChurchSubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbl_ChurchSubCategories",
                table: "Tbl_ChurchSubCategories");

            migrationBuilder.RenameTable(
                name: "Tbl_ChurchSubCategories",
                newName: "Tbl_ChurchSubcategories");

            migrationBuilder.RenameIndex(
                name: "IX_Tbl_ChurchSubCategories_CategoryId",
                table: "Tbl_ChurchSubcategories",
                newName: "IX_Tbl_ChurchSubcategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbl_ChurchSubcategories",
                table: "Tbl_ChurchSubcategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_ChurchDetails_Tbl_ChurchSubcategories_SubCategoryId",
                table: "Tbl_ChurchDetails",
                column: "SubCategoryId",
                principalTable: "Tbl_ChurchSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_ChurchSubcategories_Tbl_ChurchCategories_CategoryId",
                table: "Tbl_ChurchSubcategories",
                column: "CategoryId",
                principalTable: "Tbl_ChurchCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
