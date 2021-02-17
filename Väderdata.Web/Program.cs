using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();


            ////////// --> TESTING IN PROGRESS!!! <--- ///////////////

            // read data from csv and store in datatable:
            DataTable datatable = CsvReader.GetDataTabletFromCSVFile();
            // insert from datatable into sql-table:
            CsvReader.InsertDataAsBulk(datatable);

            /////////////////////////////////////////////////////////


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
