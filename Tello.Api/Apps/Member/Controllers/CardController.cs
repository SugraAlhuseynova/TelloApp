using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tello.Core.Entities;
using Tello.Core.Enums;
using Tello.Core.IUnitOfWork;
using Tello.Service.Client.Member.DTOs.ProductOrderDTOs;

namespace Tello.Api.Apps.Member.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CardController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public async Task CreateOrder(ProductOrderPostDto orderPostDto)
        {
            if (orderPostDto.CardId == 0)
            {
                Card card = new Card
                {
                    OrderStatus = OrderStatus.Pending,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                await _unitOfWork.CardRepository.CreateAsync(card);
                await _unitOfWork.CommitAsync();

                ProductOrder order = new ProductOrder
                {
                      ProductItemId = orderPostDto.ProductItemId,
                      Count = orderPostDto.Count,
                      CardId = card.Id
                };
                await _unitOfWork.ProductOrderRepository.CreateAsync(order);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                ProductOrder order = new ProductOrder
                {
                    ProductItemId = orderPostDto.ProductItemId,
                    Count = orderPostDto.Count,
                    CardId = orderPostDto.CardId
                };
                await _unitOfWork.ProductOrderRepository.CreateAsync(order);
                await _unitOfWork.CommitAsync();
            }

        }


    }
}
