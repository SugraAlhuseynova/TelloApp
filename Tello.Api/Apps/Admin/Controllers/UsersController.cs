using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tello.Api.JWT;
using Tello.Core.Entities;
using Tello.Service.Apps.Admin.DTOs.AppUserDTOs;
using Tello.Service.Email;
using Tello.Service.Exceptions;
using Tello.Service.Apps.Admin.DTOs.AppUserDTOs.RoleDtos;
using System.Data;
using NuGet.Packaging.Signing;

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
        #region RoleManager
        [HttpPost("createrole")]
        public async Task CreateRole(RolePostDto postDto)
        {
            IdentityResult role = await _roleManager.CreateAsync(new IdentityRole(ModifyRole(postDto.Role)));
        }
        private string ModifyRole(string roleStr)
        {
            string role = char.ToUpper(roleStr.Trim()[0]) + roleStr.Trim().ToLower().Substring(1);
            return role;
        }
        [HttpGet("getRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var data = _mapper.Map<List<RoleGetDto>>(_roleManager.Roles.ToList());
            foreach (var role in data)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Role);
                role.UsersCount = usersInRole.Count;
            }
            return Ok(data);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetRole(string id)
        {
            var data = _mapper.Map<RoleGetDto>(_roleManager.Roles.FirstOrDefault(x => x.Id == id));
            return Ok(data);
        }
        [HttpPut("role/{id}")]
        public async Task UpdateRole(string id, RolePostDto postDto)
        {
            var data = await (_roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id));
            data.Name = postDto.Role;
            data.NormalizedName = postDto.Role.ToUpper();
            await _roleManager.UpdateAsync(data);
        }
        #endregion

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
            UserLoginResponseDto loginResponseDto = new UserLoginResponseDto(){ Token = token};

            return Ok(loginResponseDto);
        }

        [HttpGet("getallmembers")]
        public async Task<IActionResult> GetAllMembers()
        {
            var users = _userManager.Users.Where(x => !x.IsAdmin).ToList();
            var data = _mapper.Map<List<UserGetDto>>(users);
            foreach (var user in users.Zip(data, (user, dto) => new { User = user, Dto = dto }))
            {
                var roles = await _userManager.GetRolesAsync(user.User);
                user.Dto.Roles = roles;
            }
            return Ok(data);
        }
        [HttpDelete("member/{id}")]
        public async Task BlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsBlocked = true;
                await _userManager.UpdateSecurityStampAsync(user);
                await _userManager.UpdateAsync(user);
            }
        }

        #region AdminManager
        [HttpPost("admin")]
        [Authorize(Roles = "Superadmin")]
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

            List<string> roles = _roleManager.Roles.Where(r => postDto.RolesIds.Contains(r.Id)).Select(x => x.Name).ToList();

            var result2 = await _userManager.AddToRolesAsync(user, roles);
            if (!result2.Succeeded)
                return BadRequest(result2.Errors);
            return Ok();
        }
        [HttpPut("admin/{id}")]
        public async Task UpdateAdmin(string id, UserEditDto postDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new ItemNotFoundException("user not found");
            var userDto = _mapper.Map<UserGetDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            var RolesIds = _roleManager.Roles.Where(r => roles.Contains(r.Name)).Select(x => x.Id).ToList();
            if (postDto.RolesIds != RolesIds)
            {
                await _userManager.RemoveFromRolesAsync(user, roles);
                List<string> newRoles = _roleManager.Roles.Where(r => postDto.RolesIds.Contains(r.Id)).Select(x => x.Name).ToList();
                var result = await _userManager.AddToRolesAsync(user, newRoles);
            }
        }
        [HttpGet("getalladmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var users = _userManager.Users.Where(x => x.IsAdmin).ToList();
            var data = _mapper.Map<List<UserGetDto>>(users);
            foreach (var user in users.Zip(data, (user, dto) => new { User = user, Dto = dto }))
            {
                var roles = await _userManager.GetRolesAsync(user.User);
                user.Dto.Roles = roles;
            }
            return Ok(data);
        }
        [HttpGet("getuser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            var userDto = _mapper.Map<UserGetDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            userDto.RolesIds = _roleManager.Roles.Where(r => roles.Contains(r.Name)).Select(x => x.Id).ToList();
            return Ok(userDto);
        }
        [HttpGet("LoggedUser")]
        public async Task<IActionResult> FindCurrentUser()
        {
            AppUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email).Value);
                IList<string> roles = await _userManager.GetRolesAsync(user);
                UserGetDto getDto = _mapper.Map<UserGetDto>(user);
                getDto.Roles = roles;
                return Ok(getDto);
            }
            return NotFound();
        }
        [HttpDelete("admin/{id}")]
        public async Task DeleteAdmin(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(user != null) 
                await _userManager.DeleteAsync(user);
        }
        
        #endregion

        #region forgotPassword
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
        #endregion
 
    }
}