using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class CsvModelClass
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Datum { get; set; }
        public string Plats { get; set; }
        public double Temp { get; set; }
        public double Luftfuktighet { get; set; }


        public static List<CsvModelClass> GetRequiredPlace(string place, WeatherContext context)
        {
            var places = (from d in context.CsvModelClasses
                          where d.Plats == place
                          orderby d.Datum.DayOfYear ascending
                          select d
                          ).ToList();
            return places;
        }


        public static List<CsvModelClass> GetRequiredMinutes(List<CsvModelClass> daysToCheck, DateTime StartDate, int increment)
        {
            var days = (from CM in daysToCheck
                        where CM.Datum.Month == StartDate.Month
                        where CM.Datum.Day == StartDate.Day
                        where CM.Datum.Hour == StartDate.Hour
                        where CM.Datum.Minute == StartDate.Minute + increment
                        select CM).ToList();
            return days;
        }
    }
}
