using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.VariationDTOs
{
    public class VariationPostDto
    {
        public string Name { get; set; }
    }
    public class VariationPostDtoValidator : AbstractValidator<VariationPostDto>
    {
        public VariationPostDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(35).WithMessage("Maximum length  is 20");
        }
    }

}
