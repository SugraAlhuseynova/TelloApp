using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tello.Api.JWT;
using Tello.Core.Entities;
using Tello.Service.Apps.Admin.DTOs.AppUserDTOs;
using Tello.Service.Email;
using Tello.Service.Exceptions;

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IJWTSerivce _jwtService;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager,
            IMapper mapper, IJWTSerivce jwtService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        /// <summary>
        /// Action create roles 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateRole(string roleStr)
        {
            IdentityResult role = await _roleManager.CreateAsync(new IdentityRole(ModifyRole(roleStr)));
        }
        private string ModifyRole(string roleStr)
        {
            string role = char.ToUpper(roleStr.Trim()[0]) + roleStr.Trim().ToLower().Substring(1);
            return role;
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
            var result2 = await _userManager.AddToRoleAsync(user, "Superadmin");
            if (!result2.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <remarks>
        ///// Sample request: 
        ///// 
        /////     POST api/users/login
        /////     {
        /////           "email": "super@example.com",
        /////           "password": "Super123"
        /////     }
        ///// </remarks>
        ///// <param name="loginDto"></param>
        ///// <returns></returns>
        ///// <exception cref="ItemNotFoundException"></exception>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new ItemNotFoundException("Email or password is incorrect");
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                throw new ItemNotFoundException("Email or password is incorrect");

            IList<string> roles = await _userManager.GetRolesAsync(user);
            string token = _jwtService.CreateJWTToken(user, roles);
            UserLoginResponseDto loginResponseDto = new UserLoginResponseDto(){ Token = token };
            return Ok(loginResponseDto);
        }

        /// <summary>
        /// Action create admin 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Superadmin")]
        [HttpPost("admin")]
        public async Task<IActionResult> CreateAdmin(UserPostDto postDto)
        {
            if (_userManager.Users.Any(x => x.Email == postDto.Email && x.IsAdmin))
                throw new RecordDuplicatedException("This email has been used");
            AppUser user = _mapper.Map<AppUser>(postDto);
            user.IsAdmin = true;
            user.PhoneNumber = "1234567898";
            var result = await _userManager.CreateAsync(user, postDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            var result2 = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result2.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="email">alhuseynovasugra@gmail.com</param>
        /// <returns></returns>
        /// <exception cref="ItemNotFoundException"></exception>
        [HttpPost("forgotPassword/admin")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new ItemNotFoundException("Email is incorrect");

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(ResetPassword), "Users", new { token, email }, Request.Scheme);

            EmailSender.SendEmail(email, link);
            return Ok(new { url = link });
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var member = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (member == null)
                throw new ItemNotFoundException("item not found");

            ChangePasswordDto vm = new ChangePasswordDto
            {
                Email = email,
                Token = token
            };
            return Ok(vm);
        }
        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword(ChangePasswordDto changePasswordDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
            if (user == null)
                throw new ItemNotFoundException("Email is incorrect");
            var result = await _userManager.ResetPasswordAsync(user, changePasswordDto.Token, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Password succesfully reset");
        }
    }
}