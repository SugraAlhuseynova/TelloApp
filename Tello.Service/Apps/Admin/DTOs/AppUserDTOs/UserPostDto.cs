using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.AppUserDTOs
{
    public class UserPostDto
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> RolesIds { get; set; }
    }
    public class UserPostDtoValidator : AbstractValidator<UserPostDto>
    {
        public UserPostDtoValidator()
        {
            RuleFor(x => x.Fullname).NotNull().MaximumLength(30);
            RuleFor(x => x.Email).NotNull().MaximumLength(40).EmailAddress();
            RuleFor(x=>x.Password).NotNull().MinimumLength(6).MaximumLength(16);
            RuleFor(x => x.PhoneNumber).NotNull().Length(10);
        }
    }
}
