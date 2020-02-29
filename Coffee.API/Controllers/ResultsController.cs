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
using Coffee.API.Processor;
using Coffee.API.Models;

namespace Coffee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ResultsController : ControllerBase
    {
        private readonly IHostEnvironment _hostingEnvironment;
        public ResultsController(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private double GetTotal(string name)
        {
            //return total payments based on users
            JsonHelper jh = new JsonHelper(_hostingEnvironment.ContentRootPath + "/Data/payments.json");
            List<payments> payList = jh.Read<List<payments>>("");


            if (payList != null && payList.Count > 0)
            {
                foreach (payments u in payList)
                {
                    if (u.user == name)
                    {
                        return u.amount;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Return total payments, orders prices and balance based on users
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
                    double ut = GetTotal(item.Key);
                    obj.Add(new { user = item.Key, order_total = sum, payment_total = ut, balance = sum - ut });
                }

                return Ok(obj);
            }
            return Ok(new { msg = "No data." });
        }

    }
}

