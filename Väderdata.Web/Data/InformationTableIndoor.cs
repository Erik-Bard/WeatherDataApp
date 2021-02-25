using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class InformationTableIndoor
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime SelectDate { get; set; }
        public double AvgTemp { get; set; }
        public double AvgHum { get; set; }
        public string MouldRisk { get; set; }
        public int MouldRank { get; set; }
        public double TotalTimeBalconyDoorOpened { get; set; }

        public static void AddBalconyTime(WeatherContext context)
        {

            var Dates = new List<DateTime>();

            foreach (var item in context.InformationTableIndoor)
            {
                if (item != null)
                {
                    Dates.Add(item.SelectDate);
                }
                else
                {
                    continue;
                }
            }
            var OrderedDates = Dates.OrderByDescending(x => x).ToList();
            var first = OrderedDates.Last();
            var last = OrderedDates.First();
            for (DateTime date = first; date <= last; date.AddDays(1))
            {
                var day = from t in context.DoorOpenings
                          where t.OpeningDoor.DayOfYear == date.DayOfYear
                          select t;
                int counter = 0;
                var changeDay = (from c in context.InformationTableIndoor
                                 where c.SelectDate.DayOfYear == date.DayOfYear
                                 select c).ToList(); ;
                foreach (var time in day)
                {
                    counter += time.TimeOpened;
                }
                changeDay[0].TotalTimeBalconyDoorOpened = counter;
            }     
        }
    }
}

