using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class Variation : BaseEntity
    {
        public string Name { get; set; }
        [NotMapped]
        public List<VariationCategory> VariationCategories { get; set;}
    }
}
