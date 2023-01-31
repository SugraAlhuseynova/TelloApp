using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Data.DAL;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(CategoryPostDto categoryPostDto)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Name == categoryPostDto.Name && !x.IsDeleted);
            if (entity != null)
                throw new RecordDuplicatedException("already exist");

            var category = new Category
            {
                Name = categoryPostDto.Name
            };
            await _unitOfWork.CategoryRepository.CreateAsync(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"item not found (id = {id})");
            entity.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public async Task<CategoryGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"item not found (id = {id})");
            var categoryGetDto = new CategoryGetDto { Name = entity.Name, Id = entity.Id };
            return categoryGetDto;
        }

        public async Task UpdateAsync(int id, CategoryPostDto categoryPostDto)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"item not found (id = {id})");
            entity.Name = categoryPostDto.Name;
            await _unitOfWork.CommitAsync();
        }
    }
}
