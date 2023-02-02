using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.SlideDTOs;

namespace Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs
{
    public class VariationCategoryPostDto
    {
        public int CategoryId { get; set; }
        public int VariationId { get; set; }
    }
}
