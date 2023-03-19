using Tello.Service.Client.Member.DTOs;
using Tello.Service.Client.Member.DTOs.ProductItemDTOs;

namespace Tello.Service.Client.Member.IServices
{
    public interface IProductItemService
    {
        Task<ProductItemGetDto> GetAsync(int id);
        PaginationListDto<ProductItemListItemGetDto> GetAll(int page);
    }
}
