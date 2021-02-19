using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Data;

namespace Väderdata.Web.Context
{
    public class DataInitializer
    {
        public static void PopulateAvgTemp(WeatherContext context)
        {
            string Plats = "Ute";
            bool running = true;
            while(running)
            {
                DateTime StartDate = new DateTime(2016, 10, 01);
                while (StartDate.Month != 12)
                {
                    int counter = 0;
                    List<double> counterList = new List<double>();
                    var datumSelect = (from p in context.CsvModelClasses
                                       where p.Datum.Day == StartDate.Day
                                       select p);
                    var platsSelect = (from e in datumSelect
                                       where e.Plats == Plats
                                       select e).ToList();
                    var query = (from k in platsSelect
                                 select k.Temp).ToList();
                    foreach (var item in query)
                    {
                        counter++;
                        counterList.Add(counter);
                    }
                    var total = query.Sum();
                    var avgTemp = total / counterList.Count();
                    var avgTemps = new AvgTemp { Plats = Plats, AverageTemperature = avgTemp, SelectDate = StartDate };
                    context.AvgTemp.Add(avgTemps);
                    StartDate = StartDate.AddDays(1);
                    
                }
                if(Plats == "Inne")
                {
                    running = false;
                }
                Plats = "Inne";
                context.SaveChanges();
            }
        }
        public static void PopulateAvgHumidity(WeatherContext context)
        {
            string Plats = "Ute";
            bool running = true;
            while (running)
            {
                DateTime StartDate = new DateTime(2016, 10, 01);
                while (StartDate.Month != 12)
                {
                    int counter = 0;
                    List<double> counterList = new List<double>();
                    var datumSelect = (from p in context.CsvModelClasses
                                       where p.Datum.Day == StartDate.Day
                                       select p);
                    var platsSelect = (from e in datumSelect
                                       where e.Plats == Plats
                                       select e).ToList();
                    var query = (from k in platsSelect
                                 select k.Luftfuktighet).ToList();
                    foreach (var item in query)
                    {
                        counter++;
                        counterList.Add(counter);
                    }
                    var total = query.Sum();
                    var avgHum = total / counterList.Count();
                    var avgHums = new AvgHumidity { Plats = Plats, AverageHumidity = avgHum  , SelectDate = StartDate };
                    context.AvgHumidities.Add(avgHums);
                    StartDate = StartDate.AddDays(1);
                }
                if (Plats == "Inne")
                {
                    running = false;
                }
                Plats = "Inne";
                context.SaveChanges();
            }
        }
        public static void GetMouldRisk(WeatherContext context)
        {
            Console.WriteLine("in method");
            var avgTemp = from temp in context.AvgTemp
                          select temp;

           
                          

        }
    }
}
