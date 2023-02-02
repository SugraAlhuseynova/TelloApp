using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public SlideService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment web)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _web = web;
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
            //FileManager.Remove(_web.WebRootPath, "Uploads/Slides", slide.BackgroundPhotoStr);
            //FileManager.Delete(_web.WebRootPath, "Uploads/Slides/Deleted",slide.BackgroundPhotoStr);
            //FileManager.Remove(_web.WebRootPath, "Uploads/Slides", slide.ProductPhotoStr);
            Delete(_web.WebRootPath, "Uploads/Slides/Deleted", "Uploads/Slides", slide.ProductPhoto /*<- bu gorduyun filein ozudu*/);
            slide.IsDeleted = true;
            //slide.BackgroundPhoto.CopyTo("")
            await _unitOfWork.CommitAsync();
        }
        private void Delete( string root, string folder, string prevFolder , IFormFile file)
        {
            string pathNew = Path.Combine(root, folder, file.FileName);
            string pathPrev = Path.Combine( folder, file.FileName); //UploadS/Slider/Uploads/Deleted bele bele orda axi ele sekil yoxdu null atir

            using (FileStream stream = new FileStream(pathNew, FileMode.Create))
            {
                if (File.Exists(pathPrev))
                {
                    Console.WriteLine("a");
                }
            }
        }
        public PaginatedListDto<SlideListItemDto> GetAll(int page)
        {
            //eger orderi varsa ordere gore siralayib veririrk eger yoxdusa yaranma vaxtina gore
            var query = _unitOfWork.SlideRepository.GetAll(x => !x.IsDeleted);
                //.OrderByDescending(x=>x.Order)     User terefde
                //.ThenByDescending(x=>x.ModifiedAt);
            List<SlideListItemDto> items = _mapper.Map<List<SlideListItemDto>>(query.Skip((page-1)*2).Take(2).ToList());
            var listDto = new PaginatedListDto<SlideListItemDto>(items, query.Count(), page, 2);
            return listDto;
        }
        public PaginatedListDto<SlideListItemDto> GetAllDeleted(int page)
        {
            var query = _unitOfWork.SlideRepository.GetAll(x => x.IsDeleted);
            //.OrderByDescending(x=>x.Order)     User terefde
            //.ThenByDescending(x=>x.ModifiedAt);
            List<SlideListItemDto> items = _mapper.Map<List<SlideListItemDto>>(query.Skip((page - 1) * 2).Take(2).ToList());
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
            List<string> deletePathes = new List<string>();
            if (slidePostDto.BackgroundPhoto != null)
            {
                CheckSlidePhoto(slidePostDto.BackgroundPhoto);
                deletePathes.Add(slide.BackgroundPhotoStr);
                slide.BackgroundPhotoStr = FileManager.Save("Uploads/Slides", _web.WebRootPath, slidePostDto.BackgroundPhoto);
            }
            if (slidePostDto.ProductPhoto != null)
            {
                CheckSlidePhoto(slidePostDto.ProductPhoto);
                deletePathes.Add(slide.ProductPhotoStr);
                slide.ProductPhotoStr = FileManager.Save("Uploads/Slides", _web.WebRootPath, slidePostDto.ProductPhoto);
            }
            if (deletePathes != null)
            {
                foreach (var item in deletePathes)
                {
                    FileManager.Remove(_web.WebRootPath, "Uploads/Slides", item);
                }
            }
            await _unitOfWork.CommitAsync();
        }
        private void CheckSlidePhoto(IFormFile file)
        {
            if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                throw new FileFormatException("File format must be image/png or image/jpeg");
            if (file.Length > 3145728)
                throw new Exception("File siz must be less than 2MB");
        }
    }
}
