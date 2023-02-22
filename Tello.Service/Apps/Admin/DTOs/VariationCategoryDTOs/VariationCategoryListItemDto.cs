using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;

namespace Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs
{
    public class VariationCategoryListItemDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string VariationName { get; set; }
    }
}
