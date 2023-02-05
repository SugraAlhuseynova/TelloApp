using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.ProductDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateAsync(ProductPostDto productPostDto)
        {
            var entity =await _unitOfWork.ProductRepository.GetAsync(x => x.Name == productPostDto.Name && 
            x.CategoryId == productPostDto.CategoryId && x.BrandId == productPostDto.BrandId && !x.IsDeleted, "Category", "Brand");
            if (entity != null)
                throw new RecordDuplicatedException("Product already exist");
            if (!(await _unitOfWork.CategoryRepository.IsExistAsync(x => x.Id == productPostDto.CategoryId)))
                throw new ItemNotFoundException("Category not found");
            if (!(await _unitOfWork.BrandRepository.IsExistAsync(x => x.Id == productPostDto.BrandId)))
                throw new ItemNotFoundException("Brand not found");
            //yoxladiq sonra elave ederik
            entity = _mapper.Map<Product>(productPostDto);
            await _unitOfWork.ProductRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Product not found");
            entity.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public PaginatedListDto<ProductListItemDto> GetAll(int page)
        {
            var query = _unitOfWork.ProductRepository.GetAll(x => !x.IsDeleted, "Category", "Brand");
            List<ProductListItemDto> items = _mapper.Map<List<ProductListItemDto>>(query.Skip((page - 1) * 2).Take(2).ToList());
            var paginationList = new PaginatedListDto<ProductListItemDto>(items, query.Count(), page, 2);
            return paginationList;
        }

        public PaginatedListDto<ProductListItemDto> GetAllDeleted(int page)
        {
            var query = _unitOfWork.ProductRepository.GetAll(x => x.IsDeleted, "Category", "Brand");
            List<ProductListItemDto> items = _mapper.Map<List<ProductListItemDto>>(query.Skip((page - 1) * 2).Take(2).ToList());
            var paginationList = new PaginatedListDto<ProductListItemDto>(items, query.Count(), page, 2);
            return paginationList;
        }

        public async Task<ProductGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "Category", "Brand");
            if (entity == null)
                throw new ItemNotFoundException("Product not found");
            var productGetDto = _mapper.Map<ProductGetDto>(entity);
            return productGetDto;
        }

        public async Task Restore(int id)
        {
            var entity = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == id && x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Product not found");
            entity.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, ProductPostDto productPostDto)
        {
            var entity = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "Category", "Brand");
            if (entity == null)
                throw new ItemNotFoundException("Product not found");
            if (!(await _unitOfWork.CategoryRepository.IsExistAsync(x => x.Id == productPostDto.CategoryId)))
                throw new ItemNotFoundException("Category not found");
            if (!(await _unitOfWork.BrandRepository.IsExistAsync(x => x.Id == productPostDto.BrandId)))
                throw new ItemNotFoundException("Brand not found");

            entity.Name = productPostDto.Name;
            entity.Desc = productPostDto.Desc;
            entity.Count = productPostDto.Count;
            entity.CategoryId = productPostDto.CategoryId;
            entity.BrandId = productPostDto.BrandId;
            entity.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
        }
    }
}
