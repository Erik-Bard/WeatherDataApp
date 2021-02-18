using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Väderdata.Web.Migrations
{
    public partial class initMar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvgTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AverageTemperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvgTemp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CsvModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Plats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temp = table.Column<double>(type: "float", nullable: false),
                    Luftfuktighet = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsvModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvgTemp");

            migrationBuilder.DropTable(
                name: "CsvModel");
        }
    }
}
