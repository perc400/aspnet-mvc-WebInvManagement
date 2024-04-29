using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IMStrategys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMStrategys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ABCGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StrategyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ABCGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ABCGroups_IMStrategys_StrategyId",
                        column: x => x.StrategyId,
                        principalTable: "IMStrategys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "XYZGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StrategyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XYZGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XYZGroups_IMStrategys_StrategyId",
                        column: x => x.StrategyId,
                        principalTable: "IMStrategys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    StockTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_StockTypes_StockTypeId",
                        column: x => x.StockTypeId,
                        principalTable: "StockTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_StockTypes_StockTypeId",
                        column: x => x.StockTypeId,
                        principalTable: "StockTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductionStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    StockTypeId = table.Column<int>(type: "int", nullable: true),
                    ABCId = table.Column<int>(type: "int", nullable: true),
                    XYZId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionStocks_ABCGroups_ABCId",
                        column: x => x.ABCId,
                        principalTable: "ABCGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductionStocks_StockTypes_StockTypeId",
                        column: x => x.StockTypeId,
                        principalTable: "StockTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductionStocks_XYZGroups_XYZId",
                        column: x => x.XYZId,
                        principalTable: "XYZGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockTypeId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_StockTypes_StockTypeId",
                        column: x => x.StockTypeId,
                        principalTable: "StockTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseProductionStocks",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ProductionStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseProductionStocks", x => new { x.WarehouseId, x.ProductionStockId });
                    table.ForeignKey(
                        name: "FK_WarehouseProductionStocks_ProductionStocks_ProductionStockId",
                        column: x => x.ProductionStockId,
                        principalTable: "ProductionStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseProductionStocks_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ABCGroups_StrategyId",
                table: "ABCGroups",
                column: "StrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StockTypeId",
                table: "Orders",
                column: "StockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStocks_ABCId",
                table: "ProductionStocks",
                column: "ABCId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStocks_StockTypeId",
                table: "ProductionStocks",
                column: "StockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStocks_XYZId",
                table: "ProductionStocks",
                column: "XYZId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_OrderId",
                table: "Reports",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_StockTypeId",
                table: "Reports",
                column: "StockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_StockTypeId",
                table: "Suppliers",
                column: "StockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProductionStocks_ProductionStockId",
                table: "WarehouseProductionStocks",
                column: "ProductionStockId");

            migrationBuilder.CreateIndex(
                name: "IX_XYZGroups_StrategyId",
                table: "XYZGroups",
                column: "StrategyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WarehouseProductionStocks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ProductionStocks");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "ABCGroups");

            migrationBuilder.DropTable(
                name: "StockTypes");

            migrationBuilder.DropTable(
                name: "XYZGroups");

            migrationBuilder.DropTable(
                name: "IMStrategys");
        }
    }
}
