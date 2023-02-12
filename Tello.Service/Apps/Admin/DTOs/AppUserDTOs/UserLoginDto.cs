using FluentValidation;

namespace Tello.Service.Apps.Admin.DTOs.AppUserDTOs
{
    public class UserLoginDto 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().MaximumLength(40).EmailAddress();
            RuleFor(x => x.Password).NotNull().MinimumLength(6).MaximumLength(16);
        }
    }
}
