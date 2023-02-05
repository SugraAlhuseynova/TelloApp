using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tello.Data.Migrations
{
    public partial class ProductItemVariationTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductItemVariations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductItemId = table.Column<int>(type: "int", nullable: false),
                    VariationOptionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItemVariations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductItemVariations_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductItemVariations_VariationOptions_VariationOptionId",
                        column: x => x.VariationOptionId,
                        principalTable: "VariationOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemVariations_ProductItemId",
                table: "ProductItemVariations",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemVariations_VariationOptionId",
                table: "ProductItemVariations",
                column: "VariationOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductItemVariations");
        }
    }
}
