using Tello.Service.Client.Member.DTOs.CategoryDTOs;

namespace Tello.Service.Client.Member.IServices
{
    public interface ICategoryService
    {
        Task<CategoryGetDto> GetAsync(int id);
        List<CategoryFilterDto> GetAll();
    }
}
