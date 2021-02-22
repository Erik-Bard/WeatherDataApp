using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web.Data
{
    public class BalconyDoor
    {
        public int Id { get; set; }
        public DateTime OpeningDoor { get; set; }
        public DateTime ClosingDoor { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}
