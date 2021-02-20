using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class ReadOnlyEnviroment
    {
        public int Id { get; set; }
        public string Plats { get; set; }

        public static void PopulateIfEmpty(WeatherContext context)
        {
            context.Database.EnsureCreated();
            if (!context.ReadOnlyEnv.Any())
            {
                string Outdoors = "Ute";
                string Indoors = "Inne";
                context.ReadOnlyEnv.Add(new ReadOnlyEnviroment
                {
                    Plats = Outdoors,
                });
                context.ReadOnlyEnv.Add(new ReadOnlyEnviroment
                {
                    Plats = Indoors,
                });
            }
            else
            {
                // ALREADY POPULATED //
            }
        }
    }
}
