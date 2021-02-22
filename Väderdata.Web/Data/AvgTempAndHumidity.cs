using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class AvgTempAndHumidity
    {
        [Required]
        public int Id { get; set; }
        // SelectDate shows average temp for a chosen day
        public DateTime SelectDate { get; set; }

        public double AverageHumidity { get; set; }

        public double AverageTemperature { get; set; }

        public string Plats { get; set; }

        public static IQueryable<AvgTempAndHumidity> SortByHumidity(WeatherContext context)
        {
            var HumSort = (from H in context.AvgTempAndHumidities
                          orderby H.AverageHumidity ascending
                          select H);
            return HumSort;
                         
        }
        public static IQueryable<AvgTempAndHumidity> SortByTemperature(WeatherContext context)
        {
            var TempSort = (from T in context.AvgTempAndHumidities
                           orderby T.AverageTemperature ascending
                           select T);
            return TempSort;
        }

        public static void PopulateAvgTempAndHumidity(WeatherContext context)
        {
            string Plats = "Ute";
            bool running = true;
            while (running)
            {
                DateTime StartDate = new DateTime(2016, 10, 01);
                while (StartDate.Month != 12)
                {
                    int counterTemp = 0;
                    int counterHumi = 0;
                    List<float> counterListHum = new List<float>();
                    List<float> counterListTemp = new List<float>();
                    var CSV = context.CsvModelClasses.ToList();
                    var CSVSort = from p in CSV
                                  orderby p.Datum
                                  select p;
                    var datumSelect = (from p in CSVSort
                                       where p.Datum.Day == StartDate.Day
                                       select p);
                    var platsSelect = (from e in datumSelect
                                       where e.Plats == Plats
                                       select e).ToList();
                    var queryTemp = (from k in platsSelect
                                     select k.Temp).ToList();
                    var queryHumid = (from k in platsSelect
                                      select k.Luftfuktighet).ToList();
                    foreach (var item in queryHumid)
                    {
                        counterHumi++;
                        counterListHum.Add(counterHumi);
                    }
                    var total = queryHumid.Sum();
                    var avgHum = Math.Round((total / counterListHum.Count()), 2);
                    foreach (var item in queryTemp)
                    {
                        counterTemp++;
                        counterListTemp.Add(counterTemp);
                    }
                    var total2 = queryTemp.Sum();
                    var avgTemp = Math.Round((total2 / counterListTemp.Count()), 2);
                    var avgTemps = new AvgTempAndHumidity { Plats = Plats, AverageTemperature = avgTemp, AverageHumidity = avgHum, SelectDate = StartDate };
                    context.AvgTempAndHumidities.Add(avgTemps);
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
    }
}
