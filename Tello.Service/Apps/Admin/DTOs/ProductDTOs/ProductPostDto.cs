using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;

namespace Tello.Service.Apps.Admin.DTOs.ProductDTOs
{
    public class ProductPostDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Count { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}
