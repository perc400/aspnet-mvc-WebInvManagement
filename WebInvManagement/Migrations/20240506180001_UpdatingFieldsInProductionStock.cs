using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class UpdatingFieldsInProductionStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailyConsumption",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpectedConsumptionDuringLeadTime",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaximumDesirableStockLevel",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReorderPoint",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SafetyStock",
                table: "ProductionStocks",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyConsumption",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "ExpectedConsumptionDuringLeadTime",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "MaximumDesirableStockLevel",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "ReorderPoint",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "SafetyStock",
                table: "ProductionStocks");
        }
    }
}
