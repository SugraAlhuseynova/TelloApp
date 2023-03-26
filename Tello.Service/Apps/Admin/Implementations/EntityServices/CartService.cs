using AutoMapper;
using Tello.Core.Enums;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.CartDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations.EntityServices
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task ChangeStatus(int id, byte status)
        {
            var entity = await _unitOfWork.CartRepository.GetAsync(x=>x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("order not found");
            if (!Enum.IsDefined(typeof(OrderStatus), status))
                throw new ItemNotFoundException("status not found");
            
            entity.OrderStatus = (OrderStatus)status;
            await _unitOfWork.CommitAsync();
        }

        public List<CartListItemDto> GetAll()
        {
            var query = _unitOfWork.CartRepository.GetAll(x => !x.IsDeleted, "User", "ProductOrders");
            List<CartListItemDto> items = _mapper.Map<List<CartListItemDto>>(query.ToList());
            return items;
        }

        public PaginatedListDto<CartListItemDto> GetAll(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.CartRepository.GetAll(x => !x.IsDeleted, "User", "ProductOrders");
            List<CartListItemDto> items = _mapper.Map<List<CartListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var listDto = new PaginatedListDto<CartListItemDto>(items, query.Count(), page, paginationCount);
            return listDto;
        }
        public async Task<CartGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.CartRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "User");
            if (entity == null)
                throw new ItemNotFoundException("order not found");
            var cardDto = _mapper.Map<CartGetDto>(entity);
            return cardDto;
        }
    }
}
