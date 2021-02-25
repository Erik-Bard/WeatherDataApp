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
        public DbSet<MögelRisk> MögelRisks { get; set; }
        public DbSet<MeteorologiskSäsong> WeatherSeason { get; set; }
        public DbSet<BalconyDoor> BalconyDoor { get; set; }
        public DbSet<ReadOnlyEnviroment> ReadOnlyEnv { get; set; }
        public DbSet<InformationTableIndoor> InformationTablesIndoor { get; set; }
        public DbSet<InformationTableOutdoor> InformationTablesOutdoor { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CsvModelClass>()
                .ToTable("CsvModelClass");
            modelBuilder.Entity<MögelRisk>()
                .ToTable("MögelRisk");
        }
    }
}
