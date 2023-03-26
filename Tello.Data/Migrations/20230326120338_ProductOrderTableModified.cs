using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tello.Data.Migrations
{
    public partial class ProductOrderTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_Carts_CartId",
                table: "ProductsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_ProductItems_ProductItemId",
                table: "ProductsOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders");

            migrationBuilder.RenameTable(
                name: "ProductsOrders",
                newName: "ProductsOrder");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsOrders_ProductItemId",
                table: "ProductsOrder",
                newName: "IX_ProductsOrder_ProductItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsOrders_CartId",
                table: "ProductsOrder",
                newName: "IX_ProductsOrder_CartId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ProductsOrder",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProductsOrder",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductsOrder",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsOrder",
                table: "ProductsOrder",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductItemId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrder_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_CartId",
                table: "ProductOrder",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ProductItemId",
                table: "ProductOrder",
                column: "ProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrder_Carts_CartId",
                table: "ProductsOrder",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrder_ProductItems_ProductItemId",
                table: "ProductsOrder",
                column: "ProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrder_Carts_CartId",
                table: "ProductsOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrder_ProductItems_ProductItemId",
                table: "ProductsOrder");

            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsOrder",
                table: "ProductsOrder");

            migrationBuilder.RenameTable(
                name: "ProductsOrder",
                newName: "ProductsOrders");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsOrder_ProductItemId",
                table: "ProductsOrders",
                newName: "IX_ProductsOrders_ProductItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsOrder_CartId",
                table: "ProductsOrders",
                newName: "IX_ProductsOrders_CartId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ProductsOrders",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProductsOrders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductsOrders",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsOrders",
                table: "ProductsOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_Carts_CartId",
                table: "ProductsOrders",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_ProductItems_ProductItemId",
                table: "ProductsOrders",
                column: "ProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
