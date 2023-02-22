using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Data.DAL;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateAsync(CategoryPostDto categoryPostDto)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Name == categoryPostDto.Name && !x.IsDeleted);
            if (entity != null)
                throw new RecordDuplicatedException("Category already exist");
            Category category = _mapper.Map<Category>(categoryPostDto);
            await _unitOfWork.CategoryRepository.CreateAsync(category);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"Category not found (id = {id})");
            entity.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public PaginatedListDto<CategoryListItemDto> GetAll(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.CategoryRepository.GetAll(x => !x.IsDeleted);
            List<CategoryListItemDto> items = _mapper.Map<List<CategoryListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var listDto = new PaginatedListDto<CategoryListItemDto>(items, query.Count(), page, paginationCount);
            return listDto;

        }

        public List<CategoryListItemDto> GetAll1()
        {
            var query = _unitOfWork.CategoryRepository.GetAll(x => !x.IsDeleted);
            List<CategoryListItemDto> items = _mapper.Map<List<CategoryListItemDto>>(query.ToList());
            return items;
        }

        public PaginatedListDto<CategoryListItemDto> GetAllDeleted(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.CategoryRepository.GetAll(x => x.IsDeleted);
            List<CategoryListItemDto> items = _mapper.Map<List<CategoryListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var listDto = new PaginatedListDto<CategoryListItemDto>(items, query.Count(), page, paginationCount);
            return listDto;
        }

        public async Task<CategoryGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"Category not found (id = {id})");
            var categoryGetDto = _mapper.Map<CategoryGetDto>(entity);
            return categoryGetDto;
        }

        public async Task Restore(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id && x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"Category not found (id = {id})");
            entity.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, CategoryPostDto categoryPostDto)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"Category not found (id = {id})");
            entity.Name = categoryPostDto.Name;
            entity.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
        }
    }
}
