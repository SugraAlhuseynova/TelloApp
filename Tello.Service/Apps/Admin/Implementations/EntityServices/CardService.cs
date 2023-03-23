using AutoMapper;
using Tello.Core.Enums;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.CardDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations.EntityServices
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task ChangeStatus(int id, byte status)
        {
            var entity = await _unitOfWork.CardRepository.GetAsync(x=>x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("order not found");
            if (!Enum.IsDefined(typeof(OrderStatus), status))
                throw new ItemNotFoundException("status not found");
            
            entity.OrderStatus = (OrderStatus)status;
            await _unitOfWork.CommitAsync();
        }

        public List<CardListItemDto> GetAll()
        {
            var query = _unitOfWork.CardRepository.GetAll(x => !x.IsDeleted, "User", "ProductOrders");
            List<CardListItemDto> items = _mapper.Map<List<CardListItemDto>>(query.ToList());
            return items;
        }

        public PaginatedListDto<CardListItemDto> GetAll(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.CardRepository.GetAll(x => !x.IsDeleted, "User", "ProductOrders");
            List<CardListItemDto> items = _mapper.Map<List<CardListItemDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var listDto = new PaginatedListDto<CardListItemDto>(items, query.Count(), page, paginationCount);
            return listDto;
        }
        public async Task<CardGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.CardRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "User");
            if (entity == null)
                throw new ItemNotFoundException("order not found");
            var cardDto = _mapper.Map<CardGetDto>(entity);
            return cardDto;
        }
    }
}
