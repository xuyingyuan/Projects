using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshingStore.Repo.Migrations
{
    public partial class createFreshDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    ColorCode = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageType",
                schema: "product",
                columns: table => new
                {
                    TypeCode = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    TypeName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TypeDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FileExtension = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    isDefaultImageType = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageType", x => x.TypeCode);
                });

            migrationBuilder.CreateTable(
                name: "SizeCode",
                schema: "product",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeCode", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "SizeScale",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    SizeRange = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SizeCodes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeScale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category",
                        column: x => x.CategoryId,
                        principalSchema: "product",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SizeScaleId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_SizeScale",
                        column: x => x.SizeScaleId,
                        principalSchema: "product",
                        principalTable: "SizeScale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryItem",
                schema: "product",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItem", x => new { x.SubCategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CategoryItem_Product",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryItem_SubCategory",
                        column: x => x.SubCategoryId,
                        principalSchema: "product",
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductColor",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ColorDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ColorPriceOverride = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    IsDefaultColor = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    FK_ProductColor_Color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductColor_Color_FK_ProductColor_Color",
                        column: x => x.FK_ProductColor_Color,
                        principalSchema: "product",
                        principalTable: "Color",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductColor_Product",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ImageTypeCode = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    ProductImageUrl = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_ImageType",
                        column: x => x.ImageTypeCode,
                        principalSchema: "product",
                        principalTable: "ImageType",
                        principalColumn: "TypeCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sku",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    Upc = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    FitId = table.Column<int>(type: "int", nullable: true),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    SizeCode = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    SizeIndex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sku_Color",
                        column: x => x.ColorId,
                        principalSchema: "product",
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sku_Product",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sku_SizeCode",
                        column: x => x.SizeCode,
                        principalSchema: "product",
                        principalTable: "SizeCode",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItem_ProductId",
                schema: "product",
                table: "CategoryItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SizeScaleId",
                schema: "product",
                table: "Product",
                column: "SizeScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_FK_ProductColor_Color",
                schema: "product",
                table: "ProductColor",
                column: "FK_ProductColor_Color");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ProductId",
                schema: "product",
                table: "ProductColor",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ImageTypeCode",
                schema: "product",
                table: "ProductImage",
                column: "ImageTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                schema: "product",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sku_ColorId",
                schema: "product",
                table: "Sku",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sku_ProductId",
                schema: "product",
                table: "Sku",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sku_SizeCode",
                schema: "product",
                table: "Sku",
                column: "SizeCode");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryId",
                schema: "product",
                table: "SubCategory",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryItem",
                schema: "product");

            migrationBuilder.DropTable(
                name: "ProductColor",
                schema: "product");

            migrationBuilder.DropTable(
                name: "ProductImage",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Sku",
                schema: "product");

            migrationBuilder.DropTable(
                name: "SubCategory",
                schema: "product");

            migrationBuilder.DropTable(
                name: "ImageType",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Color",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "product");

            migrationBuilder.DropTable(
                name: "SizeCode",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "product");

            migrationBuilder.DropTable(
                name: "SizeScale",
                schema: "product");
        }
    }
}
