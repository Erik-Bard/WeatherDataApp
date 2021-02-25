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
        public static void GetTimeBalcony(WeatherContext context)
        {
            bool runningDays = true;
            List<BalconyDoor> BalconyDoors = new List<BalconyDoor>();
            var daysInside = CsvModelClass.GetRequiredPlace("Inne", context);
            var daysOutside = CsvModelClass.GetRequiredPlace("Ute", context);
            DateTime StartDate = daysInside[0].Datum;
            int dayIncrement = 0;
            while (runningDays)
            {
                StartDate = StartDate.AddDays(dayIncrement);
                bool runningMinutes = true;
                DateTime TimeChecked = new DateTime();
                int currentDay = StartDate.Day;
                while (runningMinutes)
                {
                    bool Rising = true;
                    int increment = 1;
                    double TempDifferenceCurrent = 0;
                    double OriginalTempDifference = 0;
                    int doorCounter = 0;
                    DateTime CurrentMinute = TimeChecked;
                    var currentMinuteInside = CsvModelClass.GetRequiredMinutes(daysInside, StartDate, 0);
                    var currentMinuteOutside = CsvModelClass.GetRequiredMinutes(daysOutside, StartDate, 0);
                    if (currentMinuteInside.Count() == 0 || currentMinuteOutside.Count() == 0)
                    {
                        StartDate = StartDate.AddMinutes(2);
                        Rising = false;
                    }
                    
                    else if (currentMinuteInside.Count() != 0 && currentMinuteOutside.Count() != 0)
                    {
                        TimeChecked = currentMinuteInside[0].Datum;
                        TempDifferenceCurrent = currentMinuteInside[0].Temp - currentMinuteOutside[0].Temp;
                        OriginalTempDifference = TempDifferenceCurrent;
                    }
                    while (Rising)
                    {
                        var nextMinuteInside = CsvModelClass.GetRequiredMinutes(daysInside, StartDate, increment);
                        var nextMinuteOutside = CsvModelClass.GetRequiredMinutes(daysOutside, StartDate, increment);
                        BalconyDoor balconyDoor = new BalconyDoor();
                        if (nextMinuteInside.Count() == 0 || nextMinuteOutside.Count() == 0)
                        {
                            StartDate = StartDate.AddMinutes(2);
                            Rising = false;
                        }
                        else
                        {
                            double TempDifferenceNext = nextMinuteInside[0].Temp - nextMinuteOutside[0].Temp;
                            if (TempDifferenceCurrent - TempDifferenceNext >= 0.4 || OriginalTempDifference - TempDifferenceNext >= 0.4)
                            {
                                TempDifferenceCurrent = TempDifferenceNext;
                                increment++;
                                doorCounter++;
                            }
                            else if (TempDifferenceCurrent - TempDifferenceNext < 0.4 || OriginalTempDifference - TempDifferenceNext < 0.4)
                            {
                                StartDate = nextMinuteInside[0].Datum;
                                if (doorCounter > 1)
                                {
                                    DateTime Open = CurrentMinute;
                                    DateTime Close = currentMinuteInside[0].Datum;
                                    DoorOpening doorOpening = new DoorOpening(Open, Close, doorCounter);
                                    context.DoorOpenings.Add(doorOpening);
                                }
                                Rising = false;
                            }
                        }
                        balconyDoor.DayChecked = TimeChecked;
                        balconyDoor.TemperatureDifferences = Math.Round(TempDifferenceCurrent, 2);
                        if (nextMinuteInside.Count() != 0 && nextMinuteOutside.Count() != 0)
                        {
                            if (nextMinuteInside[0].Datum.Minute == 59 && nextMinuteOutside[0].Datum.Minute == 59)
                            {
                                StartDate = StartDate.AddHours(1);
                                StartDate = StartDate.AddMinutes(-59);
                                Rising = false;
                            }
                        }
                        BalconyDoors.Add(balconyDoor);
                        // end of rising
                    }
                    if (StartDate.Hour == 23 && StartDate.Minute == 59 || currentDay != StartDate.Day)
                    {
                        runningMinutes = false;
                        StartDate = StartDate.AddHours(-StartDate.Hour);
                        StartDate = StartDate.AddMinutes(-StartDate.Minute);
                        dayIncrement++;
                        if (StartDate.Month == 12)
                        {
                            runningDays = false;
                        }
                    }
                    // end of running minutes
                }
                foreach (var item in BalconyDoors)
                {
                    context.BalconyDoor.Add(item);
                }
               context.SaveChanges();
            }
        }
    }
}
