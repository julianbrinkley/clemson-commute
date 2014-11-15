using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class ModelService
    {
        private const string UrlBase = "http://www.nhtsa.gov/webapi/api/Recalls/vehicle/modelyear/{0}/make/{1}?format=json";

        public async Task<IEnumerable<Model>> Refresh( string Year, string Make)
        {

            var client = new HttpClient();

            var uri = new Uri(string.Format(
                UrlBase,
                Year,
                Make
                ));

            var json = await client.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<ListofModels>(json);


            return result.Data;



        }
    }
}
