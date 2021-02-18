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

            modelBuilder.Entity("Väderdata.Web.Data.AvgTemp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<double>("Luftfuktighet")
                        .HasColumnType("float");

                    b.Property<string>("Plats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Temp")
                        .HasColumnType("float");

                    b.HasKey("Id");
                    b.ToTable("CsvModelClasses");
                    b.ToTable("CsvModel");
                });
        }
    }
}
