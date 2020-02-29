using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Coffee.Core.Entities
{
    public class expectedResult
    {
        public string user { get; set; }
        public double order_total { get; set; }
        public double payment_total { get; set; }
        public double balance { get; set; }
    }
}
