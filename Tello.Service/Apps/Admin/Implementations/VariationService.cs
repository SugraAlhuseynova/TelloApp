using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
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
            var variation = await _unitOfWork.VariationRepository.GetAsync(x=>x.Name == variationPostDto.Name &&
            x.CategoryId == variationPostDto.CategoryId && !x.IsDeleted);
            if (variation != null)
                throw new RecordDuplicatedException("Variation already exist");
            if (!(await _unitOfWork.CategoryRepository.IsExistAsync(x => x.Id == variationPostDto.CategoryId && !x.IsDeleted)))
                throw new ItemNotFoundException("Category does not exist");
            variation = _mapper.Map<Variation>(variationPostDto);
            await _unitOfWork.VariationRepository.CreateAsync(variation);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var variation = await _unitOfWork.VariationRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (variation != null)
                throw new ItemNotFoundException("Variation not found");
            variation.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        //public PaginatedListDto<VariationListItemDto> GetAll(int page)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<VariationGetDto> GetAsync(int id)
        //{
        //    var variation = await _unitOfWork.VariationRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
        //    if (variation != null)
        //        throw new ItemNotFoundException("Variation not found");
        //    var variationGetDto = _mapper.Map<VariationGetDto>(variation);
        //}

        public async Task UpdateAsync(int id, VariationPostDto variationPostDto)
        {
            //variasiyani goturdum 
            var variation = await _unitOfWork.VariationRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (variation != null)
                throw new ItemNotFoundException("Variation not found");

            if (await _unitOfWork.VariationRepository.IsExistAsync(x => x.Name == variationPostDto.Name && x.Id != variation.Id
            && x.CategoryId == variationPostDto.CategoryId && !x.IsDeleted))
                throw new RecordDuplicatedException("Variation already exist");
            if (!(await _unitOfWork.CategoryRepository.IsExistAsync(x => x.Id == variationPostDto.CategoryId && !x.IsDeleted)))
                throw new ItemNotFoundException("Category does not exist");
           
            variation.Name = variationPostDto.Name;
            variation.CategoryId = variationPostDto.CategoryId;
            await _unitOfWork.CommitAsync();
        }
    }
}
