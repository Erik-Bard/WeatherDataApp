using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web.Context
{
    public class DataInitializer
    {
        public static void GetMouldRisk(WeatherContext context)
        {
            Console.WriteLine("in method");
            var avgTemp = from temp in context.AvgTemp
                          select temp;

           
                          

        }
    }
}
