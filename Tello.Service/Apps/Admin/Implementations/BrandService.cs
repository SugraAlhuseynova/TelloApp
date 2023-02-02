﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Data.UnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(BrandPostDto PostDto)
        {
            if (await _unitOfWork.BrandRepository.IsExistAsync(x=>x.Name == PostDto.Name && !x.IsDeleted))
                throw new RecordDuplicatedException("Brand already exist");
            var brand = _mapper.Map<Brand>(PostDto);
            await _unitOfWork.BrandRepository.CreateAsync(brand);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int id)
        {
            Brand entity = await _unitOfWork.BrandRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"Brand not found (Id = {id})");
            entity.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public PaginatedListDto<BrandListItemDto> GetAll(int page)
        {
            var query = _unitOfWork.BrandRepository.GetAll(x=>!x.IsDeleted);
            List<BrandListItemDto> items = _mapper.Map<List<BrandListItemDto>>(query.Skip((page - 1) * 2).Take(2).ToList());
            var listDto = new PaginatedListDto<BrandListItemDto>(items,query.Count(),page, 2);
            return listDto;
        }

        public PaginatedListDto<BrandListItemDto> GetAllDeleted(int page)
        {
            var query = _unitOfWork.BrandRepository.GetAll(x => x.IsDeleted);
            List<BrandListItemDto> items = _mapper.Map<List<BrandListItemDto>>(query.Skip((page - 1) * 2).Take(2).ToList());
            var listDto = new PaginatedListDto<BrandListItemDto>(items, query.Count(), page, 2);
            return listDto;
        }

        public async Task<BrandGetDto> GetAsync(int id)
        {
            Brand entity = await _unitOfWork.BrandRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"Brand not found (Id = {id})");
            var brandGetDto = _mapper.Map<BrandGetDto>(entity);
            return brandGetDto;
        }

        public async Task Restore(int id)
        {
            Brand entity = await _unitOfWork.BrandRepository.GetAsync(x => x.Id == id && x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"Brand not found (Id = {id})");
            entity.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, BrandPostDto PostDto)
        {
            Brand entity = await _unitOfWork.BrandRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException($"Brand not found (Id = {id})");
            entity.Name = PostDto.Name;
            await _unitOfWork.CommitAsync();
        }
    }
}