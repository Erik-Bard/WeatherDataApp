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
        public DbSet<AvgTemp> AvgTemp { get; set; }
        public DbSet<CsvModelClass> CsvModelClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CsvModelClass>()
                .ToTable("CsvModelClasses");
        }
    }
}
