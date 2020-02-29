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
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {


        private readonly IHostEnvironment _hostingEnvironment;
        public OrderController(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public class OrdersProvider
        {
            public static List<orders> GetList(string rootPath)
            {
                JsonHelper jh = new JsonHelper(rootPath + "/Data/orders.json");
                List<orders> list = jh.Read<List<orders>>("");
                return list;
            }

        }

        [HttpGet]
        public IActionResult List()
        {
            List<orders> list = OrdersProvider.GetList(_hostingEnvironment.ContentRootPath);
            if (list != null)
            {
                return Ok(list);
            }
            return Ok(new { msg = "No data." });
        }
    }
}
