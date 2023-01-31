using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;

namespace Tello.Service.Apps.Admin.IServices
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryPostDto categoryPostDto);
        Task UpdateAsync(int id, CategoryPostDto categoryPostDto);
        Task<CategoryGetDto> GetAsync(int id);
        Task Delete(int id);
        //Task GetAll();
    }
}
