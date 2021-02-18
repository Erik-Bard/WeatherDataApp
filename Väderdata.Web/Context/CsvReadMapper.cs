using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;
using Väderdata.Web.Data;

namespace Väderdata.Web.Context
{
    public class CsvReadMapper : CsvMapping<CsvModelClass>
    {
        public CsvReadMapper() : base()
        {
            MapProperty(0, x => x.Datum);
            MapProperty(1, x => x.Plats);
            MapProperty(2, x => x.Temp);
            MapProperty(3, x => x.Luftfuktighet);
        }
    }
}
