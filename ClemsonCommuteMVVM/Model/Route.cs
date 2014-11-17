using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class Route
    {

        public int RouteID { get; set; }
        public string RouteName { get; set; }
        public string ProviderName { get; set; }
        public List<Stop> Stops { get; set; }
    }
}
