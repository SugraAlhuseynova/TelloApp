using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs;

namespace Tello.Service.Apps.Admin.DTOs.VariationOptionDTOs
{
    public class VariationOptionListItemDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public VariationCategoryGetDto VariationCategory { get; set; }
    }
}
