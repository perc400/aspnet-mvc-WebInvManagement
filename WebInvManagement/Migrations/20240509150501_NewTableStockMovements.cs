using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class NewTableStockMovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockMovementProductionStocks",
                columns: table => new
                {
                    StockMovementId = table.Column<int>(type: "int", nullable: false),
                    ProductionStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovementProductionStocks", x => new { x.StockMovementId, x.ProductionStockId });
                    table.ForeignKey(
                        name: "FK_StockMovementProductionStocks_ProductionStocks_ProductionStockId",
                        column: x => x.ProductionStockId,
                        principalTable: "ProductionStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovementProductionStocks_StockMovements_StockMovementId",
                        column: x => x.StockMovementId,
                        principalTable: "StockMovements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementProductionStocks_ProductionStockId",
                table: "StockMovementProductionStocks",
                column: "ProductionStockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockMovementProductionStocks");

            migrationBuilder.DropTable(
                name: "StockMovements");
        }
    }
}
