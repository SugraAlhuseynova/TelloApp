using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.DTOs.ProductItemVariationDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class ProductItemVariationService : IProductItemVariationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductItemVariationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(ProductItemVariationPostDto postDto)
        {
            var entity = await _unitOfWork.ProductItemVariationRepository.GetAsync(x => x.VariationOptionId == postDto.VariationOptionId &&
            x.ProductItemId == postDto.ProductItemId && !x.IsDeleted);
            if (entity != null)
                throw new RecordDuplicatedException("Data already exist");
            if (!(await _unitOfWork.ProductItemRepository.IsExistAsync(x => x.Id == postDto.ProductItemId)))
                throw new ItemNotFoundException("Product Item not found");
            if (!(await _unitOfWork.VariationOptionRepository.IsExistAsync(x => x.Id == postDto.VariationOptionId)))
                throw new ItemNotFoundException("Variation Option not found");
            entity = _mapper.Map<ProductItemVariation>(postDto);
            await _unitOfWork.ProductItemVariationRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.ProductItemVariationRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Product Item Variation not found");
            entity.IsDeleted = true; 
            await _unitOfWork.CommitAsync();
        }
        public async Task Restore(int id)
        {
            var entity = await _unitOfWork.ProductItemVariationRepository.GetAsync(x => x.Id == id && x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Product Item Variation not found");
            entity.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }
        public async Task UpdateAsync(int id, ProductItemVariationPostDto postDto)
        {
            var entity = await _unitOfWork.ProductItemVariationRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Product Item Variation not found");
            if (await _unitOfWork.ProductItemVariationRepository.IsExistAsync(x => x.Id != entity.Id && x.VariationOptionId == postDto.VariationOptionId &&
           x.ProductItemId == postDto.ProductItemId && !x.IsDeleted))
                throw new RecordDuplicatedException("Data already exist");
            if (!(await _unitOfWork.ProductItemRepository.IsExistAsync(x => x.Id == postDto.ProductItemId)))
                throw new ItemNotFoundException("Product Item not found");
            if (!(await _unitOfWork.VariationOptionRepository.IsExistAsync(x => x.Id == postDto.VariationOptionId)))
                throw new ItemNotFoundException("Variation Option not found");
            entity.VariationOptionId = postDto.VariationOptionId;
            entity.ProductItemId = postDto.ProductItemId;
            entity.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
        
        }
        public PaginatedListDto<ProductItemVariationListItemDto> GetAll(int page)
        {
            var query = _unitOfWork.ProductItemVariationRepository.GetAll(x => !x.IsDeleted, "VariationOption.VariationCategory.Variation", "ProductItem.Product");
            List<ProductItemVariationListItemDto> items = _mapper.Map<List<ProductItemVariationListItemDto>>(query.Skip((page - 1) * 2).Take(2).ToList());
            var listDto = new PaginatedListDto<ProductItemVariationListItemDto>(items, query.Count(), page, 2);
            return listDto;
        }

        public PaginatedListDto<ProductItemVariationListItemDto> GetAllDeleted(int page)
        {
            var query = _unitOfWork.ProductItemVariationRepository.GetAll(x => x.IsDeleted, "VariationOption.VariationCategory.Variation", "ProductItem.Product");
            List<ProductItemVariationListItemDto> items = _mapper.Map<List<ProductItemVariationListItemDto>>(query.Skip((page - 1) * 2).Take(2).ToList());
            var listDto = new PaginatedListDto<ProductItemVariationListItemDto>(items, query.Count(), page, 2);
            return listDto;
        }

        public async Task<ProductItemVariationGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.ProductItemVariationRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "VariationOption.VariationCategory.Variation", "ProductItem.Product");
            if (entity == null)
                throw new ItemNotFoundException("Product Item Variation not found");
            var getDto = _mapper.Map<ProductItemVariationGetDto>(entity);
            return getDto;
        }
    }
}
