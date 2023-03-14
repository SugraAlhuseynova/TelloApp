using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations.Service
{
    public class VariationService : IVariationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VariationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(VariationPostDto variationPostDto)
        {
            var variation = await _unitOfWork.VariationRepository.GetAsync(x => x.Name == variationPostDto.Name && !x.IsDeleted);
            if (variation != null)
                throw new RecordDuplicatedException("Variation already exist");
            variation = _mapper.Map<Variation>(variationPostDto);
            await _unitOfWork.VariationRepository.CreateAsync(variation);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int id)
        {
            Variation variation = await _unitOfWork.VariationRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (variation == null)
                throw new ItemNotFoundException("Variation not found");
            variation.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public PaginatedListDto<VariationListItemDto> GetAll(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.VariationRepository.GetAll(x => !x.IsDeleted);
            List<VariationListItemDto> items = _mapper.Map<List<VariationListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            PaginatedListDto<VariationListItemDto> variationListItems = new PaginatedListDto<VariationListItemDto>(items, query.Count(), page, paginationCount);
            return variationListItems;
        }

        public List<VariationListItemDto> GetAll1()
        {
            var query = _unitOfWork.VariationRepository.GetAll(x => !x.IsDeleted);
            List<VariationListItemDto> items = _mapper.Map<List<VariationListItemDto>>(query.ToList());

            return items;
        }

        public PaginatedListDto<VariationListItemDto> GetAllDeleted(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.VariationRepository.GetAll(x => x.IsDeleted);
            List<VariationListItemDto> items = _mapper.Map<List<VariationListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            PaginatedListDto<VariationListItemDto> variationListItems = new PaginatedListDto<VariationListItemDto>(items, query.Count(), page, paginationCount);
            return variationListItems;
        }

        public async Task<VariationGetDto> GetAsync(int id)
        {
            var variation = await _unitOfWork.VariationRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (variation == null)
                throw new ItemNotFoundException("Variation not found");
            var variationGetDto = _mapper.Map<VariationGetDto>(variation);
            return variationGetDto;
        }

        public async Task Restore(int id)
        {
            Variation variation = await _unitOfWork.VariationRepository.GetAsync(x => x.Id == id && x.IsDeleted);
            if (variation == null)
                throw new ItemNotFoundException("Variation not found");
            variation.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, VariationPostDto PostDto)
        {
            //variasiyani goturdum 
            var variation = await _unitOfWork.VariationRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (variation == null)
                throw new ItemNotFoundException("Variation not found");
            if (await _unitOfWork.VariationRepository.IsExistAsync(x => x.Name == PostDto.Name && x.Id != variation.Id
             && !x.IsDeleted))
                throw new RecordDuplicatedException("Variation already exist");
            variation.Name = PostDto.Name;
            variation.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
        }
    }
}
