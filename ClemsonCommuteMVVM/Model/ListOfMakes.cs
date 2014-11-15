using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class ListOfMakes
    {
        [JsonProperty("Count")]
        public string MakeCount { get; set; }

        [JsonProperty("Message")]
        public string MakeMessage { get; set; }

        [JsonProperty("Results")]
        public List<Make> Data
        {

            get;
            set;
        }

    }
}
