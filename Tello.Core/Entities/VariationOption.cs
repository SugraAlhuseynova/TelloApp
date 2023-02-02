using System;
using System.Collections.Generic;
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
    }

}
