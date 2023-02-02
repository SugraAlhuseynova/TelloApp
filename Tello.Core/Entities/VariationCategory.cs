using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class VariationCategory : BaseEntity
    {
        public int VariationId { get; set; }
        public int CategoryId { get; set; }
        public Variation Variation{ get; set; }
        public Category Category{ get; set; }
    }
}
