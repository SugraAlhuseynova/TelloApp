using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class Comment : BaseEntity
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public int ProductItemId { get; set; }
        public string AppUserId { get; set; }
        public ProductItem ProductItem { get; set; }
        public AppUser AppUser { get; set; }
    }
}
