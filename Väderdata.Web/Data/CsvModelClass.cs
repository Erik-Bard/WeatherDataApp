﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web.Data
{
    public class CsvModelClass
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Datum { get; set; }
        public string Plats { get; set; }
        public double Temp { get; set; }
        public double Luftfuktighet { get; set; }
    }
}
