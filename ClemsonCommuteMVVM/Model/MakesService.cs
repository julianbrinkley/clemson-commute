using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class MakesService 
    {
        private const string UrlBase = "http://www.nhtsa.gov/webapi/api/SafetyRatings/modelyear/{0}?format=json";

        public async Task<IEnumerable<Make>> Refresh(string Year)
        {


            var client = new HttpClient();

            var uri = new Uri(string.Format(
                UrlBase,
                Year
                ));

            var json = await client.GetStringAsync(uri);

                var result = JsonConvert.DeserializeObject<ListOfMakes>(json);
                return result.Data;



        }

    }
}
