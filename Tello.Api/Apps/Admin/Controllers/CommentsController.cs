using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.IServices.IEntityServices;

namespace Tello.Api.Apps.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_commentService.GetAll());
        }
        [HttpGet("all/{page}")]
        public IActionResult GetAll(int page = 1)
        {
            return Ok(_commentService.GetAll(page));
        }
        [HttpGet("all/deleted/{page}")]
        public IActionResult GetAllDeleted(int page = 1)
        {
            return Ok(_commentService.GetAllDeleted(page));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _commentService.GetAsync(id));
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _commentService.Delete(id);
        }

    }
}
