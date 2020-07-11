using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshingStore.Repo.Migrations
{
    public partial class fixProductColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Color_FK_ProductColor_Color",
                schema: "product",
                table: "ProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductColor_FK_ProductColor_Color",
                schema: "product",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "FK_ProductColor_Color",
                schema: "product",
                table: "ProductColor");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ColorId",
                schema: "product",
                table: "ProductColor",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Color",
                schema: "product",
                table: "ProductColor",
                column: "ColorId",
                principalSchema: "product",
                principalTable: "Color",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Color",
                schema: "product",
                table: "ProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductColor_ColorId",
                schema: "product",
                table: "ProductColor");

            migrationBuilder.AddColumn<int>(
                name: "FK_ProductColor_Color",
                schema: "product",
                table: "ProductColor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_FK_ProductColor_Color",
                schema: "product",
                table: "ProductColor",
                column: "FK_ProductColor_Color");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Color_FK_ProductColor_Color",
                schema: "product",
                table: "ProductColor",
                column: "FK_ProductColor_Color",
                principalSchema: "product",
                principalTable: "Color",
                principalColumn: "Id");
        }
    }
}
