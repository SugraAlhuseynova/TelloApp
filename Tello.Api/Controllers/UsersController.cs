using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tello.Api.JWT;
using Tello.Core.Entities;
using Tello.Service.Apps.Admin.DTOs.AppUserDTOs;
using Tello.Service.Exceptions;

namespace Tello.Api.Controllers
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
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager,
            IMapper mapper, IJWTSerivce jwtService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
        }
        [HttpPost]
        //public async Task CreateRole()
        //{
        //    //IdentityResult role1 = await _roleManager.CreateAsync(new IdentityRole("Admin"));  
        //    //IdentityResult role2 = await _roleManager.CreateAsync(new IdentityRole("Member"));  
        //}

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
            var result2 = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result2.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        ///     POST api/users/login
        ///     {
        ///           "email": "string@gmail.com",
        ///           "password": "string1234"
        ///     }
        /// </remarks>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        /// <exception cref="ItemNotFoundException"></exception>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new ItemNotFoundException("Email or password is incorrect");
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                throw new ItemNotFoundException("Email or password is incorrect");

            IList<string> roles = await _userManager.GetRolesAsync(user);
            string token = _jwtService.CreateJWTToken(user, roles);

            return Ok(token);
        }



    }
}