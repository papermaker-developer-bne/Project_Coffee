using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Coffee.API.Models
{
    public class drink
    {
        public string drink_name { get; set; }
        public prices prices { get; set; }
    }

    public class prices {
        public double small { get; set; }
        public double medium { get; set; }
        public double large { get; set; }
        public double huge { get; set; }
        public double mega { get; set; }
        public double ultra { get; set; }
    }
}
