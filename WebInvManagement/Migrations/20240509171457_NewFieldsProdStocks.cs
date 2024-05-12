using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class NewFieldsProdStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AnnualDemand",
                table: "ProductionStocks",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CarryingCostPerOrder",
                table: "ProductionStocks",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HoldingCostPerUnitPerYear",
                table: "ProductionStocks",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualDemand",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "CarryingCostPerOrder",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "HoldingCostPerUnitPerYear",
                table: "ProductionStocks");
        }
    }
}
