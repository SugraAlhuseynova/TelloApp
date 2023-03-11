using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.CommentDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.CommentRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("comment not found");
            entity.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }
        public List<CommentGetDto> GetAll()
        {
            var query = _unitOfWork.CommentRepository.GetAll(x => !x.IsDeleted, "AppUser", "ProductItem.Product");
            List<CommentGetDto> items = _mapper.Map<List<CommentGetDto>>(query.ToList());
            return items;
        }

        public PaginatedListDto<CommentListItemDto> GetAll(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.CommentRepository.GetAll(x => !x.IsDeleted, "AppUser", "ProductItem.Product");
            List<CommentListItemDto> items = _mapper.Map<List<CommentListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            PaginatedListDto<CommentListItemDto> listDto = new PaginatedListDto<CommentListItemDto>(items, query.Count(), page, paginationCount);
            return listDto;
        }

        public PaginatedListDto<CommentListItemDto> GetAllDeleted(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.CommentRepository.GetAll(x=> x.IsDeleted, "AppUser", "ProductItem.Product");
            List<CommentListItemDto> items = _mapper.Map<List<CommentListItemDto>>(query.Skip((page-1)*paginationCount).Take(paginationCount).ToList());
            PaginatedListDto<CommentListItemDto> listDto = new PaginatedListDto<CommentListItemDto>(items, query.Count(), page, paginationCount);
            return listDto;
        }

        public async Task<CommentGetDto> GetAsync(int id)
        {
            Comment entity = await _unitOfWork.CommentRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "AppUser", "ProductItem.Product", "ProductItem.ProductItemVariations.VariationOption.VariationCategory.Variation");
            if (entity == null)
                throw new ItemNotFoundException("comment not found");
            var getDto = _mapper.Map<CommentGetDto>(entity);    
            return getDto;
        }
    }
}
