using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;

namespace Tello.Service.Apps.Admin.DTOs.ProductDTOs
{
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Count { get;  }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public List<ProductItem> ProductItems { get; set; }


    }
}
