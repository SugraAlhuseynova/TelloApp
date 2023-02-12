using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tello.Data.Migrations
{
    public partial class VariationsConfilict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItemVariations_VariationOptions_VariationOptionId1",
                table: "ProductItemVariations");

            migrationBuilder.DropForeignKey(
                name: "FK_VariationOptions_Variations_VariationId",
                table: "VariationOptions");

            migrationBuilder.DropIndex(
                name: "IX_VariationOptions_VariationId",
                table: "VariationOptions");

            migrationBuilder.DropIndex(
                name: "IX_ProductItemVariations_VariationOptionId1",
                table: "ProductItemVariations");

            migrationBuilder.DropColumn(
                name: "VariationId",
                table: "VariationOptions");

            migrationBuilder.DropColumn(
                name: "VariationOptionId1",
                table: "ProductItemVariations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VariationId",
                table: "VariationOptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VariationOptionId1",
                table: "ProductItemVariations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VariationOptions_VariationId",
                table: "VariationOptions",
                column: "VariationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemVariations_VariationOptionId1",
                table: "ProductItemVariations",
                column: "VariationOptionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItemVariations_VariationOptions_VariationOptionId1",
                table: "ProductItemVariations",
                column: "VariationOptionId1",
                principalTable: "VariationOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariationOptions_Variations_VariationId",
                table: "VariationOptions",
                column: "VariationId",
                principalTable: "Variations",
                principalColumn: "Id");
        }
    }
}
