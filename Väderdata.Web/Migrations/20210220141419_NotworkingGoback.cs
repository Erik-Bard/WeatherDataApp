using Microsoft.EntityFrameworkCore.Migrations;

namespace Väderdata.Web.Migrations
{
    public partial class NotworkingGoback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_avgTempInit_ReadOnlyEnv_ReadOnlyId",
                table: "avgTempInit");

            migrationBuilder.DropIndex(
                name: "IX_avgTempInit_ReadOnlyId",
                table: "avgTempInit");

            migrationBuilder.DropColumn(
                name: "ReadOnlyId",
                table: "avgTempInit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadOnlyId",
                table: "avgTempInit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_avgTempInit_ReadOnlyId",
                table: "avgTempInit",
                column: "ReadOnlyId");

            migrationBuilder.AddForeignKey(
                name: "FK_avgTempInit_ReadOnlyEnv_ReadOnlyId",
                table: "avgTempInit",
                column: "ReadOnlyId",
                principalTable: "ReadOnlyEnv",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
