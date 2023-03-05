using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.IRepositories;
using Tello.Service.Apps.Admin.DTOs.ProductDTOs;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Apps.Admin.DTOs.ProductItemDTOs;
using Tello.Core.IUnitOfWork;
using AutoMapper;
using Tello.Service.Exceptions;
using Tello.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class ProductItemService : IProductItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateAsync(ProductItemPostDto productPostDto)
        {
            if (!(await _unitOfWork.ProductRepository.IsExistAsync(x=>x.Id == productPostDto.ProductId)))
                throw new ItemNotFoundException("Product not found");
            ProductItem entity = _mapper.Map<ProductItem>(productPostDto);
            await _unitOfWork.ProductItemRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.ProductItemRepository.GetAsync(x=>x.Id ==id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("ProductItem not found");
            entity.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public PaginatedListDto<ProductItemListItemDto> GetAll(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.ProductItemRepository.GetAll(x => !x.IsDeleted, "Product.Category", "Product.Brand");
            List<ProductItemListItemDto> items = _mapper.Map<List<ProductItemListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var paginationList = new PaginatedListDto<ProductItemListItemDto>(items, query.Count(), page, paginationCount);
            return paginationList;
        }

        public List<ProductItemSelectDto> GetAll()
        {
            var query = _unitOfWork.ProductItemRepository.GetAll(x => !x.IsDeleted, "Product.Category", "Product.Brand");
            List<ProductItemSelectDto> items = _mapper.Map<List<ProductItemSelectDto>>(query.ToList());
            return items;
        }

        public PaginatedListDto<ProductItemListItemDto> GetAllDeleted(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.ProductItemRepository.GetAll(x => x.IsDeleted, "Product.Category", "Product.Brand");
            List<ProductItemListItemDto> items = _mapper.Map<List<ProductItemListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var paginationList = new PaginatedListDto<ProductItemListItemDto>(items, query.Count(), page, paginationCount);
            return paginationList;
        }

        public async Task<ProductItemGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.ProductItemRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "Product");
            if (entity == null)
                throw new ItemNotFoundException("ProductItem not found");
            var piGetDto = _mapper.Map<ProductItemGetDto>(entity);
            return piGetDto;
        }

        public async Task Restore(int id)
        {

            var entity = await _unitOfWork.ProductItemRepository.GetAsync(x => x.Id == id && x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("ProductItem not found");
            entity.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, ProductItemPostDto productPostDto)
        {
            var entity = await _unitOfWork.ProductItemRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("ProductItem not found");

            entity.CostPrice = productPostDto.CostPrice;
            entity.SalePrice = productPostDto.SalePrice;
            entity.ProductId = productPostDto.ProductId;
            entity.Count = productPostDto.Count;
            entity.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
        }
    }
}
