using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Väderdata.Web.Migrations
{
    public partial class newTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvgHumidities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AverageHumidity = table.Column<double>(type: "float", nullable: false),
                    Plats = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvgHumidities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MögelRisk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgTemperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvgFuktighet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RiskFörMögel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MögelRisk", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvgHumidities");

            migrationBuilder.DropTable(
                name: "MögelRisk");
        }
    }
}
