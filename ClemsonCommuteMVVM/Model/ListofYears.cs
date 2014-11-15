using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class ListofYears
    {

        [JsonProperty("Count")]
        public string MakeCount { get; set; }

        [JsonProperty("Message")]
        public string MakeMessage { get; set; }

        [JsonProperty("Results")]
        public List<ModelYear> Data { get; set; }
    }
}
