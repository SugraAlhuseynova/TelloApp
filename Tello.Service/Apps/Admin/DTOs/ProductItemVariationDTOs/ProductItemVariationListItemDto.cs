using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.ProductItemVariationDTOs
{
    public class ProductItemVariationListItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string VariationOptionValue { get; set; }
        public string VariationName { get; set; }
    }
}
