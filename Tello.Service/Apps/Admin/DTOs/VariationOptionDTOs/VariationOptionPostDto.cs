using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.VariationOptionDTOs
{
    public class VariationOptionPostDto
    {
        public string Value { get ; set; }
        public int VariationCategoryId { get; set; }
    }
    public class VariationOptionPostDtoValidator : AbstractValidator<VariationOptionPostDto>
    {
        public VariationOptionPostDtoValidator()
        {
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value is required").MaximumLength(50);
        }
    }
    
}
