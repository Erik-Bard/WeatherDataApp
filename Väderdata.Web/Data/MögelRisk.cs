using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;
using Väderdata;
using System.ComponentModel.DataAnnotations;

namespace Väderdata.Web.Data
{
    public class MögelRisk
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SelectDate { get; set; }
        public string Plats { get; set; }
        public string RiskFörMögel { get; set; }
        public int MögelIndex { get; set; }
        public static IQueryable<MögelRisk> SortByMögelRisk(WeatherContext context)
        {
            var MögelSort = from T in context.MouldRisks
                            orderby T.MögelIndex ascending
                            select T;
            return MögelSort;

        }
        public static string MögelText(string mould)
        {
            double risk = double.Parse(mould);
            if (risk == 0)
            {
                mould = "Ingen Risk För Mögel";
            }
            if (risk == 1)
            {
                mould = "Möjlig mögel växt efter 8 veckor";
            }
            if (risk == 2)
            {
                mould = "Mögel växt efter 4 till 8 veckor";
            }
            if (risk == 3)
            {
                mould = "Hög risk för mögel växt inom 4 veckor";
            }
            return mould;
        }
        public static void PopulateMouldRisk(WeatherContext context, string position)
        {
            // Here we will pull out all the different days average humidity and temperatures
            var Days = from d in context.AvgTempAndHumidities
                       where d.Plats == position
                       orderby d.SelectDate ascending
                       select d;
            foreach (var d in Days)
            {
                int temp = (int)Math.Round((d.AverageTemperature), 0);
                int humid = (int)Math.Round((d.AverageHumidity), 0);
                DateTime day = d.SelectDate;
                int mouldRisk = 0;
                if (temp < 0 || temp > 50 || humid < 78)
                {
                    mouldRisk = 0;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (humid < MögelTabell.MTab[temp, i])
                        {
                            mouldRisk = i - 1;
                            break;
                        }
                        else
                        {
                            mouldRisk = 3;
                        }
                    }
                }

                string MögelFakta = MögelText(mouldRisk.ToString());
                var _MögelRisk = new MögelRisk { SelectDate = day, RiskFörMögel = MögelFakta, Plats = position, MögelIndex = mouldRisk };
                context.MouldRisks.Add(_MögelRisk);
            }
            context.SaveChanges();
        }

    }
}

