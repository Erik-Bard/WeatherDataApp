﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Väderdata.Web.Context;

namespace Väderdata.Web.Migrations
{
    [DbContext(typeof(WeatherContext))]
    partial class WeatherContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Väderdata.Web.Data.AvgHumidity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AverageHumidity")
                        .HasColumnType("float");

                    b.Property<string>("Plats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SelectDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AvgHumidities");
                });

            modelBuilder.Entity("Väderdata.Web.Data.AvgTemp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AverageTemperature")
                        .HasColumnType("float");

                    b.Property<string>("Plats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SelectDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AvgTemp");
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

                    b.Property<decimal>("AvgFuktighet")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AvgTemperature")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RiskFörMögel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MögelRisk");
                });
#pragma warning restore 612, 618
        }
    }
}
