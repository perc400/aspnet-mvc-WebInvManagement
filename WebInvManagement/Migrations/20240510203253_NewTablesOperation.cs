using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class NewTablesOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationProductionStock",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    ProductionStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationProductionStock", x => new { x.OperationId, x.ProductionStockId });
                    table.ForeignKey(
                        name: "FK_OperationProductionStock_Operation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationProductionStock_ProductionStocks_ProductionStockId",
                        column: x => x.ProductionStockId,
                        principalTable: "ProductionStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationProductionStock_ProductionStockId",
                table: "OperationProductionStock",
                column: "ProductionStockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationProductionStock");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ProductionStocks");
        }
    }
}
