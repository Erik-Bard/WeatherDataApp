using Microsoft.EntityFrameworkCore.Migrations;

namespace Väderdata.Web.Migrations
{
    public partial class initData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CsvModel",
                table: "CsvModel");

            migrationBuilder.RenameTable(
                name: "CsvModel",
                newName: "CsvModelClasses");

            migrationBuilder.AlterColumn<double>(
                name: "AverageTemperature",
                table: "AvgTemp",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CsvModelClasses",
                table: "CsvModelClasses",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CsvModelClasses",
                table: "CsvModelClasses");

            migrationBuilder.RenameTable(
                name: "CsvModelClasses",
                newName: "CsvModel");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageTemperature",
                table: "AvgTemp",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CsvModel",
                table: "CsvModel",
                column: "Id");
        }
    }
}
