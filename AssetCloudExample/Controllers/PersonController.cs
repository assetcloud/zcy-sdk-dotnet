using LoveKicher.AssetCloudSDK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetCloudExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private AssetCloudClient client;

        public PersonController(AssetCloudConfig config, ILogger<PersonController> logger)
        {
            _logger = logger;
            client = new AssetCloudClient(config);
        }

        [HttpGet]
        [Route("getByUserId")]
        public async Task<object> GetPersonByUserId([FromQuery]long userId)
        {
            var res = await client.GetAsync<object>("/asset-system/person/get/person/by/id",
                new Dictionary<string, object>
                {
                    ["userId"] = userId
                });
            _logger.LogInformation(res.ToString());
            return res;
        }
    }
}
