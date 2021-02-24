using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class MeteorologiskSäsong
    {
        public int Id { get; set; }

        public string? HöstStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? HöstDatum { get; set; }

        public string? VinterStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? VinterDatum { get; set; }


        public static DateTime? AutumnDate(WeatherContext context, DateTime? höstDatum)
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
                    Console.WriteLine(höstDatum);
                    return höstDatum;
                }
                if(temp > 10 || temp < 0)
                {
                    DateInRow = 0;
                }
            }
            return null;
        }
        public static DateTime? WinterDate(WeatherContext context, DateTime? vinterDatum)
        {
            int DateInRow = 0;

            var vinterQuery = (from p in context.AvgTempAndHumidities
                         where p.Plats == "Ute"
                         select p.AverageTemperature)
                         .ToList();
            var selectVinter = (from q in vinterQuery
                                where q <= 0
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
                    Console.WriteLine(vinterDatum);
                    return vinterDatum;
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
