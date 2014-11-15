using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class Make
    {
        [JsonProperty("ModelYear")]
        public string Year { get; set; }

        [JsonProperty("Make")]
        public string VehicleMake { get; set; }

    }
}
