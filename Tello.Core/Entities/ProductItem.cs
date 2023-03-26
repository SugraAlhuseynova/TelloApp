using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    //configuration tam deyil
    public class ProductItem : BaseEntity
    {
        public float CostPrice { get; set; }
        public float SalePrice { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<ProductItemVariation> ProductItemVariations { get; set;}
        public List<Comment> Comments { get; set; }
        public List<ProductOrders> ProductOrders { get;set; }
    }
}
