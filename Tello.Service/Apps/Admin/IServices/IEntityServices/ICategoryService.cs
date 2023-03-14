using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;

namespace Tello.Service.Apps.Admin.IServices.IEntityServices
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryPostDto categoryPostDto);
        Task UpdateAsync(int id, CategoryPostDto categoryPostDto);
        Task<CategoryGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore(int id);
        PaginatedListDto<CategoryListItemDto> GetAll(int page);
        List<CategoryGetDto> GetAll();
        PaginatedListDto<CategoryListItemDto> GetAllDeleted(int page);
    }
}
