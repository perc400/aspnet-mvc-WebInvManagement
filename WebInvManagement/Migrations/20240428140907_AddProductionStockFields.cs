using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class AddProductionStockFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
