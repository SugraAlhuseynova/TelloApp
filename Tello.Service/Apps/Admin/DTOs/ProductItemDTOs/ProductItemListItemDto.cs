using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.ProductItemDTOs
{
    public class ProductItemListItemDto
    {
        public int Id { get; set; }
        public float CostPrice { get; set; }
        public float SalePrice { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductBrandName { get; set; }
    }
}