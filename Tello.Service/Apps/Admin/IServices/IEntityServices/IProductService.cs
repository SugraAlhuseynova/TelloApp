using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.ProductDTOs;
using Tello.Service.Apps.Admin.DTOs;

namespace Tello.Service.Apps.Admin.IServices.IEntityServices
{
    public interface IProductService
    {
        Task CreateAsync(ProductPostDto productPostDto);
        Task UpdateAsync(int id, ProductPostDto productPostDto);
        Task<ProductGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore(int id);
        List<ProductSelectDto> GetAll();
        PaginatedListDto<ProductListItemDto> GetAll(int page);
        PaginatedListDto<ProductListItemDto> GetAllDeleted(int page);
    }
}
