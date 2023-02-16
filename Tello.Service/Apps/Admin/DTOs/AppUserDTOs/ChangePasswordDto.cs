using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.AppUserDTOs
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().MaximumLength(40).EmailAddress();
            RuleFor(x => x.Token).NotNull();
            RuleFor(x => x.NewPassword).NotNull().MinimumLength(6).MaximumLength(16);
            RuleFor(x => x.ConfirmPassword).NotNull().MinimumLength(6).MaximumLength(16).Equal(x=>x.NewPassword);
            
        }
    }
}
