using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.SlideDTOs
{
    public class SlidePostDto
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public int? Order { get; set; }
        public IFormFile BackgroundPhoto { get; set; }
        public IFormFile ProductPhoto { get; set; }
    }
    public class SlidePostDtoValidator : AbstractValidator<SlidePostDto>
    {
        public SlidePostDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MaximumLength(40).WithMessage("Maximum length  is 40");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Description is required").MaximumLength(150).WithMessage("Maximum length is 150");
        }
    }
}
