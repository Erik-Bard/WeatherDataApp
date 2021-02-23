﻿using Microsoft.Extensions.Logging;
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
            CsvReadHelper.CsvBuilder(context);
            if (!context.AvgTempAndHumidities.Any())
            {
                AvgTempAndHumidity.PopulateAvgTempAndHumidity(context);
            }
            if (!context.MögelRisks.Any())
            {
                MögelRisk.PopulateMouldRisk(context, "Ute");
                MögelRisk.PopulateMouldRisk(context, "Inne");
            }
            BalconyDoor.GetTimeBalcony(context);
            context.SaveChanges();
        }
    }
}
