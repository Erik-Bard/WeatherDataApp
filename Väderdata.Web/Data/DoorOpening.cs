using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web.Data
{
    public class DoorOpening
    {
        public int Id { get; set; }
        public DateTime ClosingDoor { get; set; }
        public int TimeOpened { get; set; }

        public DoorOpening(DateTime closingDoor, int timeOpened)
        {
            ClosingDoor = closingDoor;
            TimeOpened = timeOpened;
        }
    }
}

