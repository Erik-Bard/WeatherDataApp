using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<WeatherContext>();
                    //DataTable datatable = CsvReadHelper.GetDataTabletFromCSVFile();
                    var read = CsvReadHelper.Reader();
                    if (read == null)
                    {
                        Console.WriteLine("\n\nThis is the end of the sequence");
                    }
                    else
                    {
                        int counter = 1;
                        foreach (var item in read)
                        {
                            if (item.Error != null)
                            {
                                Console.WriteLine("NEJ");
                            }
                            else
                            {
                                Console.WriteLine(
                                                  $"{counter}," +
                                                  $"DATE:{item.Result.Datum}," +
                                                  $"LOC:{item.Result.Plats}," +
                                                  $"TEMP:{item.Result.Temp}," +
                                                  $"MOIST:{item.Result.Luftfuktighet}");
                                counter++;
                                context.CsvModelClasses.Add(item.Result);
                            }
                        }
                    }
                    context.SaveChanges();
                    //context.CsvModelClasses.Add(read);
                    //CsvReadHelper.InsertDataAsBulk();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Fel vid initialisering av databasen");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
