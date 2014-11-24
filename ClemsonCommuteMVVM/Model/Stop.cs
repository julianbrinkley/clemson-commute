using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{


    public class Stop
    {
        public Location Location { get; set; }
        public DateTime DepartureTime { get; set; }
        public int StopID { get; set; }
    }
}
