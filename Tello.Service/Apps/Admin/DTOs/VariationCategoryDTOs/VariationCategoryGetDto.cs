using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;

namespace Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs
{
    public class VariationCategoryGetDto
    {
        public int Id { get; set; } 
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string VariationName { get; set; }
        public int VariationId { get; set; }
    }
}
