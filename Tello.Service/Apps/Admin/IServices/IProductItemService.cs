using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.ProductItemDTOs;
using Tello.Service.Apps.Admin.DTOs;

namespace Tello.Service.Apps.Admin.IServices
{
    public interface IProductItemService
    {
        Task CreateAsync(ProductItemPostDto ProductPostDto);
        Task UpdateAsync(int id, ProductItemPostDto ProductPostDto);
        Task<ProductItemGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore(int id);
        PaginatedListDto<ProductItemListItemDto> GetAll(int page);
        PaginatedListDto<ProductItemListItemDto> GetAllDeleted(int page);
    }
}
