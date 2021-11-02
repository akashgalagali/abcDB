using System;
using System.Collections.Generic;

#nullable disable

namespace abcApi.Models
{
    public partial class TblMedicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Seller { get; set; }
        public int? CidId { get; set; }

        public virtual TblCategory Cid { get; set; }
    }
}
