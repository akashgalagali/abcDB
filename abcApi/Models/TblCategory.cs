using System;
using System.Collections.Generic;

#nullable disable

namespace abcApi.Models
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblMedicines = new HashSet<TblMedicine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TblMedicine> TblMedicines { get; set; }
    }
}
