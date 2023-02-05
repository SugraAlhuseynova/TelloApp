using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class ProductItemVariation : BaseEntity
    {
        public int ProductItemId { get; set; }
        public int VariationOptionId { get; set; }
        public ProductItem ProductItem { get; set; }
        public VariationOption VariationOption { get; set; }
    }
}
