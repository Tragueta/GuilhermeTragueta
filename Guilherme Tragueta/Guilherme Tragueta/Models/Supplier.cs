using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guilherme_Tragueta.Models
{
    public class Supplier
    {
        public long SupplierId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}