using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int CategoryId { get; set; }
        public int BrendId { get; set; }
        public int Count { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }

    }
}
