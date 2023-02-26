using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class VariationCategoryService : IVariationCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VariationCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateAsync(VariationCategoryPostDto vcPostDto)
        {
            var entity = await _unitOfWork.VariationCategoryRepository.GetAsync(x=>x.CategoryId == vcPostDto.CategoryId && 
            x.VariationId == vcPostDto.VariationId && !x.IsDeleted);
            if (entity != null)
                throw new RecordDuplicatedException("VariationCategory already exist");
            entity = _mapper.Map<VariationCategory>(vcPostDto);
            await _unitOfWork.VariationCategoryRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.VariationCategoryRepository.GetAsync(x=>x.Id== id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("VariationCategory not found");
            entity.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public PaginatedListDto<VariationCategoryListItemDto> GetAll(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.VariationCategoryRepository.GetAll(x => !x.IsDeleted, "Category", "Variation");
            List<VariationCategoryListItemDto> items = _mapper.Map<List<VariationCategoryListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var pgList = new PaginatedListDto<VariationCategoryListItemDto>(items, query.Count(), page, paginationCount);
            return pgList;
        }

        public List<VariationCategoryGetDto> GetAll1()
        {
            var query = _unitOfWork.VariationCategoryRepository.GetAll(x => !x.IsDeleted, "Category", "Variation");
            List<VariationCategoryGetDto> items = _mapper.Map<List<VariationCategoryGetDto>>(query.ToList());
            return items;
        }

        public PaginatedListDto<VariationCategoryListItemDto> GetAllDeleted(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.VariationCategoryRepository.GetAll(x => x.IsDeleted, "Category", "Variation");
            List<VariationCategoryListItemDto> items = _mapper.Map<List<VariationCategoryListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var pgList = new PaginatedListDto<VariationCategoryListItemDto>(items, query.Count(), page, paginationCount);
            return pgList;
        }

        public async Task<VariationCategoryGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.VariationCategoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "Category", "Variation");
            if (entity == null)
                throw new ItemNotFoundException("VariationCategory not found");
            var vsGetDto = _mapper.Map<VariationCategoryGetDto>(entity);
            return vsGetDto;
        }

        public async Task Restore(int id)
        {
            var entity = await _unitOfWork.VariationCategoryRepository.GetAsync(x => x.Id == id && x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("VariationCategory not found");
            entity.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, VariationCategoryPostDto vsPostDto)
        {
            var entity = await _unitOfWork.VariationCategoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("VariationCategory not found");
            if (await _unitOfWork.VariationCategoryRepository.IsExistAsync(x => x.CategoryId == vsPostDto.CategoryId &&
            x.VariationId == vsPostDto.VariationId && x.Id != id && !x.IsDeleted))
                throw new RecordDuplicatedException("VariationCategory already exist");
            entity.CategoryId = vsPostDto.CategoryId;
            entity.VariationId= vsPostDto.VariationId;
            entity.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
        }
    }
}
