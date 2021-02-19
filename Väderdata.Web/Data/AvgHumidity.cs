using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web.Data
{
    public class AvgHumidity
    {
        [Required]
        public int Id { get; set; }
        // SelectDate shows average temp for a chosen day
        public DateTime SelectDate { get; set; }

        public double AverageHumidity { get; set; }

        public string Plats { get; set; }
    }
}
