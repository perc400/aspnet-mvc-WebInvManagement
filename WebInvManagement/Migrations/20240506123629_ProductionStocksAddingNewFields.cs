using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class ProductionStocksAddingNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxDesiredLevel",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptimalOrderSize",
                table: "ProductionStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThresholdLevel",
                table: "ProductionStocks",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDesiredLevel",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "OptimalOrderSize",
                table: "ProductionStocks");

            migrationBuilder.DropColumn(
                name: "ThresholdLevel",
                table: "ProductionStocks");
        }
    }
}
