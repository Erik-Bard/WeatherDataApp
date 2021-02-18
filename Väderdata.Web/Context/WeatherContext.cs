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
        public DbSet<AvgTemp> AvgTemp { get; set; }
        // DbSet's go here:
        public DbSet<CsvModelClass> CsvModelClasses { get; set; }

        public DbSet<> MyProperty { get; set; }
    }
}
