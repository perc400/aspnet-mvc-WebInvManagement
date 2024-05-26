using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebInvManagement.Migrations
{
    public partial class UserWithReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Reports_ReportId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReportId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AppUserId",
                table: "Reports",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_AppUserId",
                table: "Reports",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_AppUserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_AppUserId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReportId",
                table: "AspNetUsers",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Reports_ReportId",
                table: "AspNetUsers",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");
        }
    }
}
