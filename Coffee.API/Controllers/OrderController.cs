using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coffee.Core.Entities;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.ComponentModel;
using Coffee.API.Processor;
using Coffee.API.Models;

namespace Coffee.API.Controllers
{
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


            /// <summary>
            /// Orders from all users
            /// </summary>
            /// <returns></returns>
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
