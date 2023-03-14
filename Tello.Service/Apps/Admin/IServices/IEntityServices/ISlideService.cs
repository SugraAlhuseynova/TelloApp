using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.SlideDTOs;

namespace Tello.Service.Apps.Admin.IServices.IEntityServices
{
    public interface ISlideService
    {
        Task CreateAsync(SlidePostDto postDto);
        Task DeleteAsync(int id);
        Task Restore(int id);
        PaginatedListDto<SlideListItemDto> GetAll(int page);
        PaginatedListDto<SlideListItemDto> GetAllDeleted(int page);
        Task<SlideGetDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, SlidePostDto slideCreateDto);
    }
}
