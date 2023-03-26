using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class ProductOrders : BaseEntity
    {
        public int CartId { get; set; }
        public int ProductItemId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public Cart Cart { get; set; }
        public ProductItem ProductItem { get; set; }
    }
}
