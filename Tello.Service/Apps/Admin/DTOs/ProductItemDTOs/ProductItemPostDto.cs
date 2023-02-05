using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.ProductItemDTOs
{
    public class ProductItemPostDto
    {
        public float CostPrice { get; set; }
        public float SalePrice { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
    }
}
