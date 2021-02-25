using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Data;

namespace Väderdata.Web.Context
{
    public class DataInitializer
    {
        public static void DatabaseStarter(WeatherContext context)
        {
            // Check if info in db exists, otherwise call methods to populate data
            CsvReadHelper.CsvBuilder(context);
            if (!context.AvgTempAndHumidities.Any())
            {
                AvgTempAndHumidity.PopulateAvgTempAndHumidity(context);
            }
            if (!context.MouldRisks.Any())
            {
                MögelRisk.PopulateMouldRisk(context, "Ute");
                MögelRisk.PopulateMouldRisk(context, "Inne");
            }
            context.SaveChanges();
        }
    }
}
