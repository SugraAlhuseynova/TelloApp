using Tello.Service.Client.Member.DTOs.BrandDTOs;

namespace Tello.Service.Client.Member.IServices
{
    public interface IBrandService
    {
        Task<BrandGetDto> GetAsync(int id);
        List<BrandFilterDto> GetAll();
    }
}
