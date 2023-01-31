using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.SlideDTOs;

namespace Tello.Service.Apps.Admin.IServices
{
    public interface ISlideService
    {
        Task CreateAsync(SlidePostDto postDto);
        Task DeleteAsync(int id);
        Task<List<SlideGetDto>> GetAllAsync();
        Task<SlideGetDto> GetByIdAsync(int id);
        Task UpdateAsync(SlidePostDto slidePostDto);
    }
}
