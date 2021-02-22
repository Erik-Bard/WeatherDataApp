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
        private static string MouldTxtPath = "MögelTabell.txt";
        public MögelRisk _MögelRisk = new MögelRisk();
        public static void PopulateAvgTempAndHumidity(WeatherContext context)
        {
            string Plats = "Ute";
            bool running = true;
            while (running)
            {
                DateTime StartDate = new DateTime(2016, 10, 01);
                while (StartDate.Month != 12)
                {
                    int counterTemp = 0;
                    int counterHumi = 0;
                    List<double> counterListHum = new List<double>();
                    List<double> counterListTemp = new List<double>();
                    var CSV = context.CsvModelClasses.ToList();
                    var CSVSort = from p in CSV
                                  orderby p.Datum
                                  select p;
                    var datumSelect = (from p in CSVSort
                                       where p.Datum.Day == StartDate.Day
                                       select p);
                    var platsSelect = (from e in datumSelect
                                       where e.Plats == Plats
                                       select e).ToList();
                    var queryTemp = (from k in platsSelect
                                     select k.Temp).ToList();
                    var queryHumid = (from k in platsSelect
                                      select k.Luftfuktighet).ToList();
                    foreach (var item in queryHumid)
                    {
                        counterHumi++;
                        counterListHum.Add(counterHumi);
                    }
                    var total = queryHumid.Sum();
                    var avgHum = total / counterListHum.Count();
                    foreach (var item in queryTemp)
                    {
                        counterTemp++;
                        counterListTemp.Add(counterTemp);
                    }
                    var total2 = queryTemp.Sum();
                    var avgTemp = total2 / counterListTemp.Count();
                    var avgTemps = new AvgTempAndHumidity { Plats = Plats, AverageTemperature = avgTemp, AverageHumidity = avgHum, SelectDate = StartDate };
                    context.AvgTempAndHumidities.Add(avgTemps);
                    StartDate = StartDate.AddDays(1);
                }
                if (Plats == "Inne")
                {
                    running = false;
                }
                Plats = "Inne";
                context.SaveChanges();
            }
        }
        public static void PopulateMouldRisk(WeatherContext context, string position)
        {
            // here we read from the file MögelTabell to be used in the calculation further down
            List<string> mögelCalc = new List<string>();
            using (StreamReader read = new StreamReader(MouldTxtPath))
            {
                string line = read.ReadLine();
                while (line != null)
                {
                    mögelCalc.Add(line);
                    line = read.ReadLine();
                }
                read.Dispose();


                // Here we will pull out all the different days average humidity and temperatures
                var Days = from d in context.AvgTempAndHumidities
                           where d.Plats == position
                           orderby d.SelectDate ascending
                           select d;
                foreach (var d in Days)
                {
                    double temp = d.AverageTemperature;
                    double humid = d.AverageHumidity;
                    DateTime day = d.SelectDate;
                    var CalcLine = new double[5];
                    // This part searches from the mould txt file where the temperature is and returns the specific line
                    foreach (var l in mögelCalc)
                    {
                        var sp = Array.ConvertAll(l.Split(','), double.Parse);
                        if (l[4] == temp)
                        {
                            CalcLine = sp;
                            break;
                        }
                    }
                    // here we calculate the mould risk according to formula from Claes :-)
                    int mouldRisk = 0;
                    if (temp < 0 || temp > 50 || humid < 78)
                    {
                        mouldRisk = 0;
                    }
                    else
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            if (humid < CalcLine[i])
                            {
                                mouldRisk = i - 1;
                                break;
                            }
                            else
                            {
                                mouldRisk = 3;
                                break;
                            }
                        }
                    }

                    string MögelFakta = MögelRisk.MögelText(mouldRisk.ToString());
                    var _MögelRisk = new MögelRisk { SelectDate = day, RiskFörMögel = MögelFakta, Plats = position };
                    context.MögelRisks.Add(_MögelRisk);
                }
            }
            context.SaveChanges();
        }

        public static void DatabaseStarter(WeatherContext context)
        {
            var read = CsvReadHelper.Reader();
            // Ensure the DB exists and doesnt have any data in the table we want to populate
            context.Database.EnsureCreated();
            if (context.CsvModelClasses.Any())
            {
                Console.WriteLine("\nData already exists in database. Please remove old data before inserting new.");
            }
            else
            {
                if (read == null)
                {
                    Console.WriteLine("\n\nThis is the end of the sequence");
                }
                else
                {
                    foreach (var item in read)
                    {
                        if (item.Error != null)
                        {
                            continue;
                        }
                        else
                        {
                            context.CsvModelClasses.Add(item.Result);
                        }
                    }
                }
                context.SaveChanges();
            }

            if (!context.AvgTempAndHumidities.Any())
            {
                PopulateAvgTempAndHumidity(context);
            }
            if (!context.MögelRisks.Any())
            {
                PopulateMouldRisk(context, "Ute");
                PopulateMouldRisk(context, "Inne");
            }
            context.SaveChanges();
        }

    }
}
