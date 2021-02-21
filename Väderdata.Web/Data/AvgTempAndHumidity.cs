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
    }
}
