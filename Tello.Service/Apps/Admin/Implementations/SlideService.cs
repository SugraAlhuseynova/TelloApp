using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Api.Helpers;
using Tello.Core.Entities;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.SlideDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class SlideService : ISlideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _web;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SlideService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment web, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _web = web;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task CreateAsync(SlidePostDto postDto)
        {
            Slide slide = _mapper.Map<Slide>(postDto);
            if (slide.BackgroundPhoto == null)
                throw new Exception("Background photo cannot be null");
            if (slide.ProductPhoto == null)
                throw new Exception("Product photo cannot be null");

            if (slide.BackgroundPhoto.ContentType != "image/png" && slide.BackgroundPhoto.ContentType != "image/jpeg" && 
                slide.ProductPhoto.ContentType != "image/png" && slide.ProductPhoto.ContentType != "image/jpeg")
                throw new FileFormatException("File format must be image/png or image/jpeg");

            if (slide.ProductPhoto.Length > 3145728 && slide.BackgroundPhoto.Length > 3145728)
                throw new Exception("File siz must be less than 2MB");
            
            slide.BackgroundPhotoStr = FileManager.Save("Uploads/Slides", _web.WebRootPath, slide.BackgroundPhoto);
            slide.ProductPhotoStr = FileManager.Save("Uploads/Slides", _web.WebRootPath, slide.ProductPhoto);
            await _unitOfWork.SlideRepository.CreateAsync(slide);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Slide slide = await _unitOfWork.SlideRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (slide == null)
                throw new ItemNotFoundException("Slide not found");
            slide.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public PaginatedListDto<SlideListItemDto> GetAll(int page)
        {
            //eger orderi varsa ordere gore siralayib veririrk eger yoxdusa yaranma vaxtina gore
            var query = _unitOfWork.SlideRepository.GetAll(x => !x.IsDeleted)
                .OrderByDescending(x=>x.Order)
                .ThenByDescending(x=>x.ModifiedAt);
            List<SlideListItemDto> items = _mapper.Map<List<SlideListItemDto>>(query.Skip((page-1)*2).Take(2).ToList());
            var listDto = new PaginatedListDto<SlideListItemDto>(items, query.Count(), page, 2);
            return listDto;
        }
        public async Task<SlideGetDto> GetByIdAsync(int id)
        {
            Slide slide = await _unitOfWork.SlideRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (slide == null)
                throw new ItemNotFoundException("Slide not found");
            SlideGetDto slideGetDto = _mapper.Map<SlideGetDto>(slide);
            return slideGetDto;
        }

        public async Task UpdateAsync(int id, SlidePostDto slidePostDto)
        {
            Slide slide = await _unitOfWork.SlideRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (slide == null)
                throw new ItemNotFoundException("Slide not found");


            slide.Title = slidePostDto.Title;
            slide.Desc = slidePostDto.Desc;
            slide.ModifiedAt = DateTime.UtcNow.AddHours(4);
            await _unitOfWork.CommitAsync();
        }
    }
}
