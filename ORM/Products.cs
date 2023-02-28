using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Product
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string? Dummy { get; set; }
    }
}
