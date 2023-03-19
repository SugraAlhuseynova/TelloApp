using AutoMapper;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Service.Client.Exceptions;
using Tello.Service.Client.Member.DTOs.CommentDTOs;
using Tello.Service.Client.Member.IServices;

namespace Tello.Service.Client.Member.Implementations
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
        public async Task CreateAsync(string userId, CommentPostDto postDto)
        {
            if (!await _unitOfWork.ProductItemRepository.IsExistAsync(x => x.Id == postDto.ProductItemId && !x.IsDeleted))
                throw new ItemNotFoundException("product not found");
            Comment comment = _mapper.Map<Comment>(postDto);
            comment.AppUserId = userId;
            await _unitOfWork.CommentRepository.CreateAsync(comment);
            await _unitOfWork.CommitAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
