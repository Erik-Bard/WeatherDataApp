using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class AvgTempInit
    {
        public static double Calculate(WeatherContext context, DateTime dateTime, string Plats)
        {
            int counter = 0;
            List<double> counterList = new List<double>();
            var datumSelect = (from p in context.CsvModelClasses
                               where p.Datum.Day == dateTime.Day
                               select p);
            var platsSelect = (from e in datumSelect
                               where e.Plats == Plats
                               select e).ToList();
            var query = (from k in platsSelect
                         select k.Temp).ToList();
            // Date and place selected, now calc:
            foreach (var item in query)
            {
                Console.WriteLine(item);
                counter++;
                counterList.Add(counter);
            }
            var total = query.Sum();
            Console.WriteLine(counterList.Count());
            var avgTemp = total / counterList.Count();
            Console.WriteLine(avgTemp);
            return avgTemp;
        }
    }
}
