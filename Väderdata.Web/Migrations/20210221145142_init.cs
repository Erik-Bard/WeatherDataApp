﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Väderdata.Web.Migrations
{
    public partial class init : Migration
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
                name: "avgTempInit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Plats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageTemperature = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avgTempInit", x => x.Id);
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
                name: "MögelRisk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Plats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskFörMögel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MögelIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MögelRisk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReadOnlyEnv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plats = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadOnlyEnv", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherSeason",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HöstDatum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VinterDatum = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "avgTempInit");

            migrationBuilder.DropTable(
                name: "CsvModelClass");

            migrationBuilder.DropTable(
                name: "MögelRisk");

            migrationBuilder.DropTable(
                name: "ReadOnlyEnv");

            migrationBuilder.DropTable(
                name: "WeatherSeason");
        }
    }
}
