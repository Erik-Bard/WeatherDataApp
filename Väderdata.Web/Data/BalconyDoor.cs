using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class BalconyDoor
    {
        public int Id { get; set; }
        public DateTime OpeningDoor { get; set; }
        public DateTime ClosingDoor { get; set; }
        public TimeSpan TimeSpan { get; set; }

        public static TimeSpan CalculateOpenTime(WeatherContext context, DateTime open, DateTime close)
        {
            DateTime opened = open;
            DateTime closed = close;
            TimeSpan totalDifference = closed - opened;
            // Retrieve correct date
            var query = (from a in context.CsvModelClasses
                         where a.Datum.Month == opened.Month
                         where a.Datum.Day == opened.Day
                         //where a.Datum.Hour == opened.Hour
                         //where a.Datum.Minute == opened.Minute
                         select a).ToList();
            // Compare to difference in time
            var queryhelvete = (from e in query
                                where e.Plats == "Inne"
                                select e.Temp).ToList();
            var querysakhelvete = (from o in query
                                   where o.Plats == "Ute"
                                   select o.Temp).ToList();

            // JÄMFÖRA ???? HUR ???


            // Calculate difference between both temps

            //foreach (var item in query)
            //{
            //    Console.WriteLine($"{item.Datum}, {item.Plats}, {item.Temp}");
            //}

            foreach (var thing in queryhelvete)
            {
                Console.WriteLine($"{thing}");
            }

            return totalDifference;
        }
    }
}
