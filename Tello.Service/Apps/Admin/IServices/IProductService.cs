using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.ProductDTOs;
using Tello.Service.Apps.Admin.DTOs;

namespace Tello.Service.Apps.Admin.IServices
{
    public interface IProductService
    {
        Task CreateAsync(ProductPostDto ProductPostDto);
        Task UpdateAsync(int id, ProductPostDto ProductPostDto);
        Task<ProductGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore(int id);
        PaginatedListDto<ProductListItemDto> GetAll(int page);
        PaginatedListDto<ProductListItemDto> GetAllDeleted(int page);
    }
}
