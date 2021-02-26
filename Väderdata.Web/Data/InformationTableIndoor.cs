using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class InformationTableIndoor
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime SelectDate { get; set; }
        public double AvgTemp { get; set; }
        public double AvgHum { get; set; }
        public string MouldRisk { get; set; }
        public int MouldRank { get; set; }
        public double TotalTimeBalconyDoorOpened { get; set; }
    }
}

