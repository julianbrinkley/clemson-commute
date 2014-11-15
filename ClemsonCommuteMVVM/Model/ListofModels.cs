using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class ListofModels
    {
        [JsonProperty("Count")]
        public string ModelCount { get; set; }

        [JsonProperty("Message")]
        public string ModelMessage { get; set; }

        [JsonProperty("Results")]
        public List<Model> Data { get; set; }
    }
}
