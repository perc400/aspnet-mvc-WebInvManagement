using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class TotalUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionStocks_ABCGroups_ABCId",
                table: "ProductionStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionStocks_XYZGroups_XYZId",
                table: "ProductionStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProductionStocks_ABCId",
                table: "ProductionStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProductionStocks_XYZId",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "StockTypes");

            migrationBuilder.DropColumn(
                name: "ABCId",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "LastOrderDate",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "LeadTime",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "MaxOrderQuantity",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "MinOrderQuantity",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "ServiceLevel",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "XYZId",
                table: "ProductionStocks");

            migrationBuilder.CreateTable(
                name: "ABCProductionStocks",
                columns: table => new
                {
                    ABCId = table.Column<int>(type: "int", nullable: false),
                    ProductionStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ABCProductionStocks", x => new { x.ABCId, x.ProductionStockId });
                    table.ForeignKey(
                        name: "FK_ABCProductionStocks_ABCGroups_ABCId",
                        column: x => x.ABCId,
                        principalTable: "ABCGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ABCProductionStocks_ProductionStocks_ProductionStockId",
                        column: x => x.ProductionStockId,
                        principalTable: "ProductionStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XYZProductionStocks",
                columns: table => new
                {
                    XYZId = table.Column<int>(type: "int", nullable: false),
                    ProductionStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XYZProductionStocks", x => new { x.XYZId, x.ProductionStockId });
                    table.ForeignKey(
                        name: "FK_XYZProductionStocks_ProductionStocks_ProductionStockId",
                        column: x => x.ProductionStockId,
                        principalTable: "ProductionStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_XYZProductionStocks_XYZGroups_XYZId",
                        column: x => x.XYZId,
                        principalTable: "XYZGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ABCProductionStocks_ProductionStockId",
                table: "ABCProductionStocks",
                column: "ProductionStockId");

            migrationBuilder.CreateIndex(
                name: "IX_XYZProductionStocks_ProductionStockId",
                table: "XYZProductionStocks",
                column: "ProductionStockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ABCProductionStocks");

            migrationBuilder.DropTable(
                name: "XYZProductionStocks");

            migrationBuilder.AddColumn<int>(
                name: "UnitPrice",
                table: "StockTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ABCId",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOrderDate",
                table: "ProductionStocks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "ProductionStocks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadTime",
                table: "ProductionStocks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxOrderQuantity",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinOrderQuantity",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ProductionStocks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceLevel",
                table: "ProductionStocks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ProductionStocks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "XYZId",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStocks_ABCId",
                table: "ProductionStocks",
                column: "ABCId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStocks_XYZId",
                table: "ProductionStocks",
                column: "XYZId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionStocks_ABCGroups_ABCId",
                table: "ProductionStocks",
                column: "ABCId",
                principalTable: "ABCGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionStocks_XYZGroups_XYZId",
                table: "ProductionStocks",
                column: "XYZId",
                principalTable: "XYZGroups",
                principalColumn: "Id");
        }
    }
}
