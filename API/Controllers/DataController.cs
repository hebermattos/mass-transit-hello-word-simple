using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using services;

namespace Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET api/values
        [HttpGet("{key}")]
        public async Task<string> Get(string key)
        {
            return await _dataService.Get(key); 
        }

    }
}
