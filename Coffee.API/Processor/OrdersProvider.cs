using Coffee.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.API.Processor
{
    public class OrdersProvider
    {
        public static List<orders> GetList(string rootPath)
        {
            JsonHelper jh = new JsonHelper(rootPath + "/Data/orders.json");
            List<orders> list = jh.Read<List<orders>>("");
            return list;
        }

    }


}
