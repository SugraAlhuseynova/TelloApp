using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class VariationOption : BaseEntity
    {
        public string Value { get; set; }
        public int VariationCategoryId { get; set; }
        public VariationCategory VariationCategory { get; set; }
        [NotMapped]
        public List<ProductItemVariation> ProductItemVariations { get; set; }
    }

}
