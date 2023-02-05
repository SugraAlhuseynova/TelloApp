using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class Variation : BaseEntity
    {
        public string Name { get; set; }
        public List<VariationCategory> VariationCategories { get; set;}
        public List<VariationOption> VariationOptions { get; set;}
    }
}
