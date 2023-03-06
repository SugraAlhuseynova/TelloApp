using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.ProductItemVariationDTOs;
using Tello.Service.Apps.Admin.DTOs;

namespace Tello.Service.Apps.Admin.IServices
{
    public interface IProductItemVariationService
    {
        Task CreateAsync(ProductItemVariationPostDto postDto);
        Task UpdateAsync(int id, ProductItemVariationPostDto postDto);
        Task<ProductItemVariationGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore(int id);
        List<ProductItemVariationGetDto> GetAll();
        PaginatedListDto<ProductItemVariationListItemDto> GetAll(int page);
        PaginatedListDto<ProductItemVariationListItemDto> GetAllDeleted(int page);
    }
}
