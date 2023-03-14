using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Tello.Api.Helpers;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.SlideDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;
using Tello.Service.Apps.Admin.IServices.Storage;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations.Service
{
    public class SlideService : ISlideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _web;
        private readonly IStorageService _storageService;

        public SlideService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment web, IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _web = web;
            _storageService = storageService;
        }
        public async Task CreateAsync(SlidePostDto postDto)
        {
            Slide slide = _mapper.Map<Slide>(postDto);
            if (slide.BackgroundPhoto == null)
                throw new Exception("Background photo cannot be null");
            if (slide.ProductPhoto == null)
                throw new Exception("Product photo cannot be null");
            CheckSlidePhoto(slide.BackgroundPhoto);
            CheckSlidePhoto(slide.ProductPhoto);
            slide.BackgroundPhotoStr = await _storageService.SaveAsync("slides", slide.BackgroundPhoto);
            slide.ProductPhotoStr = await _storageService.SaveAsync("slides", slide.ProductPhoto);
            await _unitOfWork.SlideRepository.CreateAsync(slide);
            await _unitOfWork.CommitAsync();
        }
        //has to modified
        public async Task DeleteAsync(int id)
        {
            Slide slide = await _unitOfWork.SlideRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (slide == null)
                throw new ItemNotFoundException("Slide not found");

            await _storageService.DeleteAsync("slides", slide.BackgroundPhotoStr);
            await _storageService.DeleteAsync("slides", slide.ProductPhotoStr);
            slide.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }
        public PaginatedListDto<SlideListItemDto> GetAll(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            //eger orderi varsa ordere gore siralayib veririrk eger yoxdusa yaranma vaxtina gore
            var query = _unitOfWork.SlideRepository.GetAll(x => !x.IsDeleted);
            //.OrderByDescending(x=>x.Order)     User terefde
            //.ThenByDescending(x=>x.ModifiedAt);
            List<SlideListItemDto> items = _mapper.Map<List<SlideListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var listDto = new PaginatedListDto<SlideListItemDto>(items, query.Count(), page, paginationCount);
            return listDto;
        }
        public PaginatedListDto<SlideListItemDto> GetAllDeleted(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.SlideRepository.GetAll(x => x.IsDeleted);
            //.OrderByDescending(x=>x.Order)     User terefde
            //.ThenByDescending(x=>x.ModifiedAt);
            List<SlideListItemDto> items = _mapper.Map<List<SlideListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var listDto = new PaginatedListDto<SlideListItemDto>(items, query.Count(), page, paginationCount);
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
            List<string> deletePathes = new List<string>();
            if (slidePostDto.BackgroundPhoto != null)
            {
                CheckSlidePhoto(slidePostDto.BackgroundPhoto);
                deletePathes.Add(slide.BackgroundPhotoStr);
                slide.BackgroundPhotoStr = await _storageService.SaveAsync("slides", slidePostDto.BackgroundPhoto);
            }
            if (slidePostDto.ProductPhoto != null)
            {
                CheckSlidePhoto(slidePostDto.ProductPhoto);
                deletePathes.Add(slide.ProductPhotoStr);
                slide.ProductPhotoStr = await _storageService.SaveAsync("slides", slidePostDto.ProductPhoto);
            }
            if (deletePathes != null)
            {
                foreach (var item in deletePathes)
                {
                    await _storageService.DeleteAsync("slides", item);

                }
            }
            slide.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
        }
        private void CheckSlidePhoto(IFormFile file)
        {
            if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                throw new FileFormatException("File format must be image/png or image/jpeg");
            if (file.Length > 3145728)
                throw new Exception("File siz must be less than 2MB");
        }
        public async Task Restore(int id)
        {
            Slide slide = await _unitOfWork.SlideRepository.GetAsync(x => x.Id == id && x.IsDeleted);
            if (slide == null)
                throw new ItemNotFoundException("Slide not found");
            await _storageService.RestoreAsync("slides", slide.BackgroundPhotoStr);
            await _storageService.RestoreAsync("slides", slide.ProductPhotoStr);
            slide.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }
    }
}
