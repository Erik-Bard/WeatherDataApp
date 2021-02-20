using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Väderdata.Web.Migrations
{
    public partial class AvgTempInitAddedAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "avgTempInit");
        }
    }
}
