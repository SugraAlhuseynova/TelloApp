using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Client.Member.DTOs.ProductItemDTOs
{
    public class ProductItemListItemGetDto
    {
        public string Name { get; set; }
        public List<string> Variations { get; set; }
        public string SalePrice { get; set; }
        public string Category { get; set; }
        public int CommentCount { get; set; }

    }
}
