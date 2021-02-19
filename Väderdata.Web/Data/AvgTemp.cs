using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class AvgTemp
    {
        WeatherContext context;

        [Required]
        public int Id { get; set; }
        // SelectDate shows average temp for a chosen day
        public DateTime SelectDate { get; set; }

        public double AverageTemperature { get; set; }

        public string Plats { get; set; }

        public void CalculateAvgTemp(DateTime selectDate, string plats)
        {
            int counter = 0;
            var datumSelect = (from p in context.CsvModelClasses
                               where p.Datum == SelectDate
                               select p);
            var platsSelect = (from e in datumSelect
                               where e.Plats == plats
                               select e);
            var query = (from k in platsSelect
                         select k.Temp);
            // Date and place selected, now calc:
            foreach (var item in query)
            {
                Console.WriteLine(item);
                counter++;
            }
            var avgTemp = platsSelect.Count() / counter;
            Console.WriteLine(avgTemp);
        }

        //public static void CalculateAvgTemp(DateTime SelectDate)
        //{
        //    var yikers = (from p in CsvModelClasses
        //                  where p.Datum )
        //}
    }

    public enum Position
    {
        inne,
        ute
    }
}
