using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tello.Api.JWT;
using Tello.Core.Entities;
using Tello.Service.Apps.Admin.DTOs.AppUserDTOs;
using Tello.Service.Client.Exceptions;

namespace Tello.Api.Apps.Member.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJWTSerivce _jWTSerivce;

        public UserController(UserManager<AppUser> userManager, IMapper mapper, IJWTSerivce jWTSerivce)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jWTSerivce = jWTSerivce;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserPostDto postDto)
        {
            if (_userManager.Users.Any(x => x.Email == postDto.Email))
                throw new RecordDuplicatedException("This email has already used");
            AppUser user = _mapper.Map<AppUser>(postDto);
            user.PhoneNumber = "1234567898";
            var result = await _userManager.CreateAsync(user, postDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            var result2 = await _userManager.AddToRoleAsync(user, "Member");
            if (!result2.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new ItemNotFoundException("Email or password is incorrect");
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                throw new ItemNotFoundException("Email or password is incorrect");

            IList<string> roles = await _userManager.GetRolesAsync(user);
            string token = _jWTSerivce.CreateJWTToken(user, roles);
            UserLoginResponseDto loginResponseDto = new UserLoginResponseDto() { Token = token };
            return Ok(loginResponseDto);
        }


    }
}
