using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.BrandDTOs
{
    public class BrandPostDto
    {
        public string Name { get; set; }
    }
    public class BrandPostDtoValidator : AbstractValidator<BrandPostDto>
    {
        public BrandPostDtoValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Brand name is Required").MaximumLength(20).WithMessage("Max length is 20");
        }
    }

}
