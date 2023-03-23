using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.ProductItemDTOs
{
    public class ProductItemCardDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float SalePrice { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        //public Dictionary<string, string> Variations { get; set; }
        public List<string> Variations { get; set; }
         
    }
}
