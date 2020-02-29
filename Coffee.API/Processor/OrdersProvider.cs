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

        public static Dictionary<string, List<object>> GetAll(string rootPath)
        {
            Dictionary<string, List<object>> dic = new Dictionary<string, List<object>>();

            List<orders> list = GetList(rootPath);
            if (list != null && list.Count > 0)
            {
                foreach (orders o in list)
                {
                    if (dic.ContainsKey(o.user))
                    {
                        dic[o.user].Add(new { drink = o.drink, size = o.size, prices = DrinkProvider.GetPrices(rootPath, o.drink, o.size) });
                    }
                    else
                    {
                        dic.Add(o.user, new List<object>() { new { drink = o.drink, size = o.size, prices = DrinkProvider.GetPrices(rootPath, o.drink, o.size) } });
                    }
                }
            }

            return dic;
        }
    }


}
