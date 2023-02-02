using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.DTOs.VariationOptionDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class VariationOptionService : IVariationOptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VariationOptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(VariationOptionPostDto variationOptionPostDto)
        {
            var entity = await _unitOfWork.VariationOptionRepository.GetAsync(x => x.Value == variationOptionPostDto.Value
            && x.VariationCategoryId == variationOptionPostDto.VariationCategoryId && !x.IsDeleted);
            if (entity != null)
                throw new RecordDuplicatedException("Variation option already exist");
            entity = _mapper.Map<VariationOption>(variationOptionPostDto);
            await _unitOfWork.VariationOptionRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.VariationOptionRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Variation option not found");
            entity.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public PaginatedListDto<VariationOptionListItemDto> GetAll(int page)
        {
            var query = _unitOfWork.VariationOptionRepository.GetAll(x => !x.IsDeleted, "VariationCategory.Category", "VariationCategory.Category");
            List<VariationOptionListItemDto> items = _mapper.Map<List<VariationOptionListItemDto>>(query.Skip((page - 1) * 2).Take(2).ToList());
            var listDto = new PaginatedListDto<VariationOptionListItemDto>(items, query.Count(), page, 2);
            return listDto;
        }

        public PaginatedListDto<VariationOptionListItemDto> GetAllDeleted(int page)
        {
            var query = _unitOfWork.VariationOptionRepository.GetAll(x => x.IsDeleted, "VariationCategory.Category", "VariationCategory.Category");
            List<VariationOptionListItemDto> items = _mapper.Map<List<VariationOptionListItemDto>>(query.Skip((page - 1) * 2).Take(2).ToList());
            var listDto = new PaginatedListDto<VariationOptionListItemDto>(items, query.Count(), page, 2);
            return listDto;
        }

        public async Task<VariationOptionGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.VariationOptionRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "VariationCategory.Category", "VariationCategory.Variation");
            if (entity == null)
                throw new ItemNotFoundException("Variation option not found");
            var voGetDto = _mapper.Map<VariationOptionGetDto>(entity);
            //voGetDto.Category = entity.VariationCategory.Category;
            //voGetDto.Variation= entity.VariationCategory.Variation;
            return voGetDto;
        }

        public async Task Restore(int id)
        {
            var entity = await _unitOfWork.VariationOptionRepository.GetAsync(x => x.Id == id && x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Variation option not found");
            entity.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, VariationOptionPostDto variationOptionPostDto)
        {
            var entity = await _unitOfWork.VariationOptionRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Variation option not found");
            if (await _unitOfWork.VariationOptionRepository.IsExistAsync(x => x.Value == variationOptionPostDto.Value
            && x.VariationCategoryId == variationOptionPostDto.VariationCategoryId && !x.IsDeleted))
                throw new RecordDuplicatedException("Variation option already exist");
            entity.Value = variationOptionPostDto.Value;
            entity.VariationCategoryId = variationOptionPostDto.VariationCategoryId;
            await _unitOfWork.CommitAsync();
        }
    }
}
