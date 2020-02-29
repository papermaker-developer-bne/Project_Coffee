using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Coffee.Core.Entities;
using Coffee.API.Models;

namespace Coffee.API.Processor
{
    public class DrinkProvider
    {

        public static List<drink> GetList(string rootPath)
        {
            JsonHelper jh = new JsonHelper(rootPath + "/Data/prices.json");
            List<drink> list = jh.Read<List<drink>>("");
            return list;
        }

        public static double GetPrices(string rootPath, string drinkname, string sizename)
        {
            List<drink> list = GetList(rootPath);
            if (list != null && list.Count > 0)
            {
                foreach (drink dr in list)
                {
                    if (drinkname == dr.drink_name)
                    {
                        object obj = dr.prices.GetType().GetProperty(sizename).GetValue(dr.prices, null);
                        return obj == null ? 0 : Convert.ToDouble(obj);
                    }
                }
            }
            return 0;
        }


    }


}

