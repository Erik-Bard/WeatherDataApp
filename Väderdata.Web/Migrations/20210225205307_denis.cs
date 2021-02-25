using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Väderdata.Web.Migrations
{
    public partial class denis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvgTempAndHumidities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AverageHumidity = table.Column<double>(type: "float", nullable: false),
                    AverageTemperature = table.Column<double>(type: "float", nullable: false),
                    Plats = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvgTempAndHumidities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BalconyDoor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayChecked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TemperatureDifferences = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalconyDoor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CsvModelClass",
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
                    table.PrimaryKey("PK_CsvModelClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoorOpenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeChecked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Opened = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorOpenings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InformationTableIndoor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvgTemp = table.Column<double>(type: "float", nullable: false),
                    AvgHum = table.Column<double>(type: "float", nullable: false),
                    MouldRisk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MouldRank = table.Column<int>(type: "int", nullable: false),
                    TotalTimeBalconyDoorOpened = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationTableIndoor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InformationTableOutdoor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvgTemp = table.Column<double>(type: "float", nullable: false),
                    AvgHum = table.Column<double>(type: "float", nullable: false),
                    MouldRisk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MouldRank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationTableOutdoor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MouldRisk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskForMould = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MouldIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouldRisk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherSeason",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutumnStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutumnDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WinterStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WinterDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherSeason", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvgTempAndHumidities");

            migrationBuilder.DropTable(
                name: "BalconyDoor");

            migrationBuilder.DropTable(
                name: "CsvModelClass");

            migrationBuilder.DropTable(
                name: "DoorOpenings");

            migrationBuilder.DropTable(
                name: "InformationTableIndoor");

            migrationBuilder.DropTable(
                name: "InformationTableOutdoor");

            migrationBuilder.DropTable(
                name: "MouldRisk");

            migrationBuilder.DropTable(
                name: "WeatherSeason");
        }
    }
}
