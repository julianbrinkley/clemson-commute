using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class YearService
    {

        private const string UrlBase = "http://www.nhtsa.gov/webapi/api/SafetyRatings?format=json";

        public async Task<IEnumerable<ModelYear>> Refresh()
        {

            var client = new HttpClient();


            var json = await client.GetStringAsync(UrlBase);

            var result = JsonConvert.DeserializeObject<ListofYears>(json);
            return result.Data;



        }
    }
}
