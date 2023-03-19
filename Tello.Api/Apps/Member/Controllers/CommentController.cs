using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tello.Core.Entities;
using Tello.Service.Client.Member.DTOs.CommentDTOs;
using Tello.Service.Client.Member.IServices;

namespace Tello.Api.Apps.Member.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Member")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task Create(CommentPostDto commentPostDto)
        {
            string userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            await _commentService.CreateAsync(userId, commentPostDto);
        }
    }
}
