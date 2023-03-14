using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.DTOs.CommentDTOs;

namespace Tello.Service.Apps.Admin.IServices.IEntityServices
{
    public interface ICommentService
    {
        Task<CommentGetDto> GetAsync(int id);
        Task Delete(int id);
        List<CommentGetDto> GetAll();
        PaginatedListDto<CommentListItemDto> GetAll(int page);
        PaginatedListDto<CommentListItemDto> GetAllDeleted(int page);
    }
}
