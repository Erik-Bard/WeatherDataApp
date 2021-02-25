using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class MeteorologicalSeason
    {
        public int Id { get; set; }

        public string? AutumnStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? AutumnDateTime { get; set; }

        public string? WinterStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? WinterDateTime { get; set; }

        //Metod som beräknar när hösten är här genom att den hämtar medeltempratur för utomhus och via loopen som kontrollerar
        //att villkoren för höst har stämt in fem dagar i rad
        public static DateTime? AutumnDate(WeatherContext context, DateTime? autumnDate)
        {
            int DateInRow = 0;

            var query = (from p in context.AvgTempAndHumidities
                         where p.Plats == "Ute"
                         select p.AverageTemperature)
                         .ToList();
            var selectQuery = (from q in query
                               where q > 0 && q < 10
                               select q);


            Console.WriteLine(selectQuery);

            foreach (var temp in selectQuery)
            {
                Console.WriteLine(temp);
                if (temp < 10 && temp >= 0)
                {
                    DateInRow += 1;
                }
                if (DateInRow == 5)
                {
                    Console.WriteLine(autumnDate);
                    return autumnDate;
                }
                if(temp > 10 || temp < 0)
                {
                    DateInRow = 0;
                }
            }
            return null;
        }
        //Metod som beräknar när vintern är här genom att den hämtar medeltempratur för utomhus och via loopen som kontrollerar
        //att villkoren för vinter med tempratur under 0 har stämt in fem dagar i rad
        public static DateTime? WinterDate(WeatherContext context, DateTime? winterDate)
        {
            int DateInRow = 0;

            var vinterQuery = (from p in context.AvgTempAndHumidities
                         where p.Plats == "Ute"
                         select p.AverageTemperature)
                         .ToList();
            var selectVinter = (from q in vinterQuery
                                where q < 0
                               select q);

            Console.WriteLine(selectVinter);

            foreach (var temp in selectVinter)
            {
                Console.WriteLine(temp);
                if (temp <= 0)
                {
                    DateInRow += 1;
                }
                if (DateInRow == 5)
                {
                    Console.WriteLine(winterDate);
                    return winterDate;
                }
                if (temp > 0)
                {
                    DateInRow = 0;
                }
            }
            return null;
        }
    }
}
