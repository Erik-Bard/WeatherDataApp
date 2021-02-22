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
            var query = (from a in context.AvgTempAndHumidities
                         where a.SelectDate.Month == opened.Month
                         where a.SelectDate.Day == opened.Day
                         select a);
            var queryFollowing = (from e in query
                                  where e.Plats == "Inne"
                                  select e.AverageTemperature).ToList();
            var querycompare = (from e in query
                                where e.Plats == "Ute"
                                select e.AverageTemperature).ToList();
            var calc = queryFollowing[0] - querycompare[0];
            Console.WriteLine(calc);
            // Calculate difference between both temps
            Console.WriteLine(querycompare[0]);

            foreach (var item in context.AvgTempAndHumidities)
            {
                Console.WriteLine(item.AverageTemperature);
            }

            return totalDifference;
        }
    }
}
