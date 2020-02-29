using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.ComponentModel;
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

        /// <summary>
        /// All data from orders.json
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

        /// <summary>
        /// Get orders with drink details and total cost for each user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult All()
        {
            Dictionary<string, List<object>> dic = OrdersProvider.GetAll(_hostingEnvironment.ContentRootPath);

            if (dic != null)
            {
                List<object> obj = new List<object>();
                foreach (KeyValuePair<string, List<object>> item in dic)
                {
                    double sum = 0;
                    foreach (var one in item.Value)
                    {
                        PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(one);
                        PropertyDescriptor pdID = pdc.Find("prices", true);
                        sum += (pdID.GetValue(one) == null ? 0 : Convert.ToDouble(pdID.GetValue(one)));
                    }
                    obj.Add(new { user = item.Key, amount = sum, list = item.Value });
                }
                return Ok(obj);
            }
            return Ok(new { msg = "No data." });
        }


        /// <summary>
        /// Provided with the user name, then return orders with drink details and total cost for this user 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Info(string name)
        {
            Dictionary<string, List<object>> dic = OrdersProvider.GetAll(_hostingEnvironment.ContentRootPath);
            if (dic != null && !string.IsNullOrWhiteSpace(name) && dic.ContainsKey(name) && dic[name] != null && dic[name].Count > 0)
            {
                double sum = 0;
                foreach (var one in dic[name])
                {
                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(one);
                    PropertyDescriptor pdID = pdc.Find("prices", true);
                    sum += (pdID.GetValue(one) == null ? 0 : Convert.ToDouble(pdID.GetValue(one)));
                }
                return Ok(new { user = name, amount = sum, list = dic[name] });
            }
            return Ok(new { msg = "No data." });
        }

    }
}
