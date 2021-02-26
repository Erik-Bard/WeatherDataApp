using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web.Data
{
    public class DoorOpening
    {
        public int Id { get; set; }
        public DateTime TimeChecked { get; set; }

        public bool Opened { get; set; }
        public DoorOpening()
        {

        }
    }
}

