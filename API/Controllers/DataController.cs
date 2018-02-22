using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace net_core_api_docker.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        // GET api/values
        [HttpGet("{key}")]
        public async Task<string> Get(string key)
        {
            using (var redis = await ConnectionMultiplexer.ConnectAsync("redis"))
            {
                var db = redis.GetDatabase();

                var value = await db.StringGetAsync(key);

                return value;
            }
        }

    }
}
