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
        public DbSet<MögelRisk> MouldRisks { get; set; }
        public DbSet<MeteorologiskSäsong> WeatherSeason { get; set; }
        public DbSet<BalconyDoor> BalconyDoor { get; set; }
        public DbSet<InformationTableIndoor> InformationTable { get; set; }
        public DbSet<InformationTableOutdoor> InformationTableOutdoor { get; set; }

        // Modelbuilder for all entities that need specific mapping etc
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CsvModelClass>()
                .ToTable("CsvModelClass");
            modelBuilder.Entity<MögelRisk>()
                .ToTable("MögelRisk");
        }
    }
}
