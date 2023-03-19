using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Client.Member.DTOs.CommentDTOs;

namespace Tello.Service.Client.Member.DTOs.ProductItemDTOs
{
    public class ProductItemGetDto
    {
        public string Name { get; set; }
        public List<string> Variations { get; set; }
        public string SalePrice { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public List<CommentGetDto> Comments { get; set; }
    }
}
