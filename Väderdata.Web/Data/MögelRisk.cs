using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web.Data
{
    public class MögelRisk
    {
        public int Id { get; set; }

        public DateTime SelectDate { get; set; }
        //public double AvgTemperature { get; set; }

        //public double AvgFuktighet { get; set; }
        public string Plats { get; set; }
        public string RiskFörMögel { get; set; }






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
    }

}
