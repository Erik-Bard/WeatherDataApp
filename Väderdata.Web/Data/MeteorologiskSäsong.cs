using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web.Data
{
    public class MeteorologiskSäsong
    {
        public int Id { get; set; }

        public DateTime HöstDatum { get; set; }

        public DateTime VinterDatum { get; set; }


    }
}
