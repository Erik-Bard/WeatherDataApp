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
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
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
                    List<double> counterListHum = new List<double>();
                    List<double> counterListTemp = new List<double>();
                    var CSV = context.CsvModelClasses.ToList();
                    var datumSelect = (from m in CSV
                                       where m.Datum.DayOfYear == StartDate.DayOfYear
                                       select m).ToList();
                    var platsSelect = (from e in datumSelect
                                       where e.Plats == Plats
                                       select e).ToList();
                    var queryTemp = (from k in platsSelect
                                     select k.Temp).ToList();
                    var queryHumid = (from k in platsSelect
                                      select k.Luftfuktighet).ToList();
                    if(queryTemp.Count() != 0 || queryHumid.Count() != 0)
                    {
                        foreach (var item in queryHumid)
                        {
                            counterHumi++;
                            counterListHum.Add(counterHumi);
                        }
                        double total = queryHumid.Sum();
                        double avgHum = Math.Round((total / counterListHum.Count()), 2);
                        foreach (var item in queryTemp)
                        {
                            counterTemp++;
                            counterListTemp.Add(counterTemp);
                        }
                        double total2 = queryTemp.Sum();
                        double avgTemp = Math.Round((total2 / counterListTemp.Count()), 2);
                        AvgTempAndHumidity avgTemps = new AvgTempAndHumidity { Plats = Plats, AverageTemperature = avgTemp, AverageHumidity = avgHum, SelectDate = StartDate };
                        context.AvgTempAndHumidities.Add(avgTemps);
                    }
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
