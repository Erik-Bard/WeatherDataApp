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
            TimeSpan total = closed - opened;
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


            double days = ((closed.Day - opened.Day));
            if (days > 1)
            {
                days *= 24;
            }
            else
            {
                days = 0;
            }
            double minutes = total.Minutes;
            double hours = total.Hours;
            hours = hours + days;
            double TotalMinutes = (hours * 60) + minutes;

            // Calculate difference between both temps

            //foreach (var item in query)
            //{
            //    Console.WriteLine($"{item.Datum}, {item.Plats}, {item.Temp}");
            //}

            foreach (var thing in queryhelvete)
            {
                Console.WriteLine($"{thing}");
            }
            Console.WriteLine($"Using Timespan: {total}, Using Double: {TotalMinutes}");
            return total;
        }

        public static void GetTimeBalcony(WeatherContext context)
        {
            var days = (from d in context.CsvModelClasses
                        where d.Plats == "Inne"
                        orderby d.Datum.DayOfYear ascending
                        select d
                       ).ToList();
            bool running = true;
            DateTime StartDate = days[0].Datum;
            while (running)
            {
                var currentMinute = (from CM in days
                                     where CM.Datum.Month == StartDate.Month
                                     where CM.Datum.Day == StartDate.Day
                                     where CM.Datum.Hour == StartDate.Hour
                                     where CM.Datum.Minute == StartDate.Minute
                                     select CM).ToList();
                double initialTemp = currentMinute[0].Temp;
                double checkTemp = initialTemp;
                bool Rising = true;
                int increment = 1;
                while (Rising)
                {
                    var nextMinute = (from m in days
                                      where m.Datum.Month == StartDate.Month
                                      where m.Datum.Day == StartDate.Day
                                      where m.Datum.Hour == StartDate.Hour
                                      where m.Datum.Minute == StartDate.Minute + increment
                                      select m).ToList();
                    if(nextMinute.Count() == 0 || StartDate.Minute == 59)
                    {
                        StartDate = StartDate.AddHours(1);
                        StartDate = StartDate.AddMinutes(-59);
                        Rising = false;
                    }
                    else
                    {
                        if ((nextMinute[0].Temp - checkTemp) > 1 || (checkTemp - nextMinute[0].Temp) > 1 || (initialTemp - checkTemp) > 1)
                        {
                            checkTemp = nextMinute[0].Temp;
                            increment++;
                            Console.WriteLine("Love är Great");

                        }
                        else if ((initialTemp - checkTemp) < 1 || (checkTemp - initialTemp) < 1)
                        {
                            StartDate = nextMinute[0].Datum;
                            Rising = false;
                        }
                    }
                    
                }

                Console.WriteLine("Hej");
                //if(nextMinute)
                //int tempRiseCount = 0;
            }

        }

        
    }
}
