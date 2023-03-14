using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class ProductOrder : BaseEntity
    {
        public int ProductItemId { get; set; }
        public int CardId { get; set; }
        public double Price { get; set; }
        public ProductItem ProductItem { get; set; }
        public Card Card { get; set; }
    }
}
