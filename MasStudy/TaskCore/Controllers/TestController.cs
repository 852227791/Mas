using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace TaskCore.Controllers
{
    [Route("controller/[action]")]
    public class TestController : Controller
    {
       
        public async Task<IActionResult> IndexAsync()
        {
            return await GetData();
        }


        public async Task<dynamic>  GetData() {
            return new { k1="v1" };
        }
        [HttpGet]
        public static async Task<JObject> GetJsonAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var jsonString = await client.GetStringAsync(uri);
                return JObject.Parse(jsonString);
            }
        }
    }
}