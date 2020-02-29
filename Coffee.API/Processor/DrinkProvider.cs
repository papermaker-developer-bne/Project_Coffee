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


    }

}
