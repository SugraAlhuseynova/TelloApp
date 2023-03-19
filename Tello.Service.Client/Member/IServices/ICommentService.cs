using Tello.Service.Client.Member.DTOs.CommentDTOs;

namespace Tello.Service.Client.Member.IServices
{
    public interface ICommentService
    {
        Task CreateAsync(string userId, CommentPostDto postDto);
        Task DeleteAsync(int id);
    }
}
