using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Data;

namespace Väderdata.Web.Context
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {

        }
        // DbSet's go here:
        public DbSet<AvgTempAndHumidity> AvgTempAndHumidities { get; set; }
        public DbSet<CsvModelClass> CsvModelClasses { get; set; }
        public DbSet<MouldRisk> MouldRisks { get; set; }
        public DbSet<MeteorologicalSeason> WeatherSeason { get; set; }
        public DbSet<BalconyDoor> BalconyDoor { get; set; }
        public DbSet<DoorOpening> DoorOpenings { get; set; }
        public DbSet<InformationTableIndoor> InformationTablesIndoor { get; set; }
        public DbSet<InformationTableOutdoor> InformationTablesOutdoor { get; set; }
        public DbSet<InformationTableIndoor> InformationTableIndoor { get; set; }
        public DbSet<InformationTableOutdoor> InformationTableOutdoor { get; set; }

        // Modelbuilder for all entities that need specific mapping etc
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CsvModelClass>()
                .ToTable("CsvModelClass");
            modelBuilder.Entity<MouldRisk>()
                .ToTable("MouldRisk");
        }
    }
}
