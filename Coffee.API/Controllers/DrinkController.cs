using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using Coffee.API.Models;
using Coffee.API.Processor;

namespace Coffee.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrinkController : ControllerBase
    {
        public class DrinkProvider
        {
            public static List<drink> GetList(string rootPath)
            {
                JsonHelper jh = new JsonHelper(rootPath + "/Data/prices.json");
                List<drink> list = jh.Read<List<drink>>("");
                return list;
            }

        }

        private readonly IHostEnvironment _hostingEnvironment;
        public DrinkController(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult List()
        {
            List<drink> list = DrinkProvider.GetList(_hostingEnvironment.ContentRootPath);
            if (list != null)
            {
                return Ok(list);
            }
            return Ok(new { msg = "No data." });
        }
    }
}
