﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Väderdata.Web.Context;

namespace Väderdata.Web.Migrations
{
    [DbContext(typeof(WeatherContext))]
    [Migration("20210220124801_AvgTempInitAddedAgain")]
    partial class AvgTempInitAddedAgain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Väderdata.Web.Data.AvgTempAndHumidity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AverageHumidity")
                        .HasColumnType("float");

                    b.Property<double>("AverageTemperature")
                        .HasColumnType("float");

                    b.Property<string>("Plats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SelectDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AvgTempAndHumidities");
                });

            modelBuilder.Entity("Väderdata.Web.Data.AvgTempInit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AverageTemperature")
                        .HasColumnType("float");

                    b.Property<string>("Plats")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SelectDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("avgTempInit");
                });

            modelBuilder.Entity("Väderdata.Web.Data.CsvModelClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Luftfuktighet")
                        .HasColumnType("int");

                    b.Property<string>("Plats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Temp")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("CsvModelClass");
                });

            modelBuilder.Entity("Väderdata.Web.Data.MeteorologiskSäsong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("HöstDatum")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VinterDatum")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("WeatherSeason");
                });

            modelBuilder.Entity("Väderdata.Web.Data.MögelRisk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Plats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RiskFörMögel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SelectDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MögelRisk");
                });
#pragma warning restore 612, 618
        }
    }
}
