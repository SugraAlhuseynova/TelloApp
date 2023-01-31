using FluentValidation;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.BrandDTOs
{
    public class BrandGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
