using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace abcApi.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public string Med { get; set; }
        public int Cust { get; set; }
        public bool Ordered { get; set; }


    }
}
