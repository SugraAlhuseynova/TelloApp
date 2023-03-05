using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.ProductDTOs;

namespace Tello.Service.Apps.Admin.DTOs.CategoryDTOs
{
    public class CategoryGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductSelectDto> Products { get; set; }
    }
}
