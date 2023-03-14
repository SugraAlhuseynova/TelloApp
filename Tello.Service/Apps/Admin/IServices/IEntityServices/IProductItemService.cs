using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.ProductItemDTOs;
using Tello.Service.Apps.Admin.DTOs;

namespace Tello.Service.Apps.Admin.IServices.IEntityServices
{
    public interface IProductItemService
    {
        Task CreateAsync(ProductItemPostDto productPostDto);
        Task UpdateAsync(int id, ProductItemPostDto productPostDto);
        Task<ProductItemGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore(int id);
        List<ProductItemSelectDto> GetAll();
        PaginatedListDto<ProductItemListItemDto> GetAll(int page);
        PaginatedListDto<ProductItemListItemDto> GetAllDeleted(int page);
    }
}
