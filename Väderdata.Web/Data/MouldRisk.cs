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
    public class MouldRisk
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime SelectDate { get; set; }
        public string Place { get; set; }
        public string RiskForMould { get; set; }
        public int MouldIndex { get; set; }
        
        //Metod som sorterar mögelrisken med mögelindex i stigande ordning
        public static IQueryable<MouldRisk> SortByMouldRisk(WeatherContext context)
        {
            var MouldSort = from T in context.MouldRisks
                            orderby T.MouldIndex ascending
                            select T;
            return MouldSort;

        }
        //Metod som lägger till textmeddelande med varje mögelindex från skalan 0-3. 
        public static string MouldText(string mould)
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
        //Metod för att ta ut data för olika dagars medelvärme och medelvärde för luftfuktighet samt avrundar utan decimaler och beräknar mögelrisken per dag-
        //-mot mögeltabellen.
        public static void PopulateMouldRisk(WeatherContext context, string position)
        {
            
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

                //Här skriver vi ut mögelrisken med tillhörande textsträngar från metoden MouldText ovan samt de olika parametrarna för mögelrisk och sparar sedan.

                string MouldFacts = MouldText(mouldRisk.ToString());
                var _MouldRisk = new MouldRisk { SelectDate = day, RiskForMould = MouldFacts, Place = position, MouldIndex = mouldRisk };
                context.MouldRisks.Add(_MouldRisk);
            }
            context.SaveChanges();
        }

    }
}

