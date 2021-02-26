using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class BalconyDoor
    {
        public int Id { get; set; }
        public DateTime DayChecked { get; set; }
        public double TemperatureDifferences { get; set; }
        public BalconyDoor()
        {
        }
        public static void PopulateBalconyDoor(WeatherContext context)
        {
            var inne = (from i in context.CsvModelClasses
                        where i.Plats == "Inne"
                        orderby i.Datum.DayOfYear
                        select i).ToList();
            var ute = (from u in context.CsvModelClasses
                       where u.Plats == "Ute"
                       orderby u.Datum.DayOfYear
                       select u).ToList();

            DateTime start = inne[0].Datum;
            DateTime end = inne[(inne.Count()) - 1].Datum;
            List<BalconyDoor> balconyDoors = new List<BalconyDoor>();
            for (DateTime date = start; date <= end; date = date.AddMinutes(1))
            {
                var dayInne = (from i in inne
                               where i.Datum == date
                               select i).ToList();
                var dayUte = (from u in ute
                              where u.Datum == date
                              select u).ToList();
                if (dayInne.Count == 0 || dayUte.Count == 0)
                {
                    continue;
                }
                double TempDiff = dayInne[0].Temp - dayUte[0].Temp;
                BalconyDoor balconyDoor = new BalconyDoor
                {
                    DayChecked = date,
                    TemperatureDifferences = TempDiff
                };
                balconyDoors.Add(balconyDoor);
            }
            foreach (var day in balconyDoors)
            {
                context.BalconyDoor.Add(day);
            }
            context.SaveChanges();
        }

        public static void DoorOpened(WeatherContext context)
        {
            var times = (from t in context.BalconyDoor
                         orderby t.DayChecked.DayOfYear
                         select t).ToList();
            DateTime start = times[0].DayChecked.AddMinutes(-times[0].DayChecked.Minute);
            DateTime end = times[(times.Count()) - 1].DayChecked;
            bool openDoor = false;
            List<DoorOpening> doorOpenings = new List<DoorOpening>();
            double OriginalTemp = 0;
            for (DateTime date = start; date <= end; date = date.AddMinutes(1))
            {
                var currentMinute = (from c in context.BalconyDoor
                                     where c.DayChecked == date
                                     select c).ToList();
                DateTime nextDate = date.AddMinutes(1);
                var nextMinute = (from n in context.BalconyDoor
                                  where n.DayChecked == nextDate
                                  select n).ToList();
                DoorOpening doorOpening = new DoorOpening();
                if (currentMinute.Count() == 0 || nextMinute.Count() == 0)
                {
                    if(currentMinute.Count() != 0)
                    {
                        doorOpening.Opened = openDoor;
                        doorOpening.TimeChecked = date;
                        doorOpenings.Add(doorOpening);
                    }
                    continue;
                }
                double checkDiff = Math.Abs(nextMinute[0].TemperatureDifferences - currentMinute[0].TemperatureDifferences);
                if(checkDiff >= 1)
                {
                    OriginalTemp = currentMinute[0].TemperatureDifferences;
                    openDoor = true;
                    doorOpening.Opened = openDoor;
                    doorOpening.TimeChecked = date;
                    doorOpenings.Add(doorOpening);
                }
                else if(Math.Abs(OriginalTemp - nextMinute[0].TemperatureDifferences) < 1)
                {
                    openDoor = false;
                    doorOpening.Opened = openDoor;
                    doorOpening.TimeChecked = date;
                    doorOpenings.Add(doorOpening);
                    OriginalTemp = 0;
                }
                else
                {
                    doorOpening.Opened = openDoor;
                    doorOpening.TimeChecked = date;
                    doorOpenings.Add(doorOpening);
                }
            }
            foreach (var item in doorOpenings)
            {
                context.DoorOpenings.Add(item);
            }
            context.SaveChanges();
        }
           
    }
}
