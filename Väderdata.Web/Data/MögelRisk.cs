﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web.Data
{
    public class MögelRisk
    {
        public int Id { get; set; }

        public decimal Temperature { get; set; }

        public decimal Fuktighet { get; set; }
    }
}
