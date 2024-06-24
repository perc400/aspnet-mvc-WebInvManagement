using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class NewStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThresholdLevel",
                table: "ProductionStocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThresholdLevel",
                table: "ProductionStocks",
                type: "int",
                nullable: true);
        }
    }
}
