using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.ProductItemVariationDTOs
{
    public class ProductItemVariationPostDto
    {
        public int ProductItemId { get; set; }
        public int VariationOptionId { get; set; }
    }
}
