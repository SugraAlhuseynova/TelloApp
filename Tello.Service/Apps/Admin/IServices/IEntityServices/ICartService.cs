using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.CartDTOs;

namespace Tello.Service.Apps.Admin.IServices.IEntityServices
{
    public interface ICartService
    {
        Task<CartGetDto> GetAsync(int id);
        List<CartListItemDto> GetAll();
        PaginatedListDto<CartListItemDto> GetAll(int page);
        Task ChangeStatus(int id, byte status);
    }
}
