using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Context;

namespace Väderdata.Web.Data
{
    public class AvgTemp
    {
        [Required]
        public int Id { get; set; }
        // SelectDate shows average temp for a chosen day
        public DateTime SelectDate { get; set; }

        public double AverageTemperature { get; set; }

        public string Plats { get; set; }
    }

    public enum Position
    {
        inne,
        ute
    }
}
