using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Väderdata.Web.Migrations
{
    public partial class tjohoo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadOnlyEnv");

            migrationBuilder.AddColumn<double>(
                name: "TotalTimeBalconyDoorOpened",
                table: "InformationTablesIndoor",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "DoorOpenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpeningDoor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDoor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOpened = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorOpenings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoorOpenings");

            migrationBuilder.DropColumn(
                name: "TotalTimeBalconyDoorOpened",
                table: "InformationTablesIndoor");

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
        }
    }
}
