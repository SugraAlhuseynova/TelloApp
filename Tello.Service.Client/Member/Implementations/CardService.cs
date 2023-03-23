using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.Enums;
using Tello.Core.IUnitOfWork;
using Tello.Service.Client.Exceptions;
using Tello.Service.Client.Member.DTOs.ProductOrderDTOs;
using Tello.Service.Client.Member.IServices;

namespace Tello.Service.Client.Member.Implementations
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
        public async Task CreateOrder(string userId, ProductOrderPostDto orderPostDto)
        {
            var price = orderPostDto.Count * _unitOfWork.ProductItemRepository.GetAsync(x => x.Id == orderPostDto.ProductItemId).Result.SalePrice;
            if (await _unitOfWork.ProductItemRepository.IsExistAsync(x => x.Id == orderPostDto.ProductItemId && !x.IsDeleted && x.Count >= orderPostDto.Count))
            {
                if (orderPostDto.CardId == 0)
                {
                    Card card = new Card
                    {
                        OrderStatus = OrderStatus.Pending,
                        UserId = userId,
                        TotalAmount = price
                    };
                    await _unitOfWork.CardRepository.CreateAsync(card);
                    //await _unitOfWork.CommitAsync();

                    ProductOrder order = new ProductOrder
                    {
                        ProductItemId = orderPostDto.ProductItemId,
                        Count = orderPostDto.Count,
                        //CardId = card.Id,
                        Price = price,
                        Card = card
                    };

                    await _unitOfWork.ProductOrderRepository.CreateAsync(order);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    var order = await _unitOfWork.ProductOrderRepository.GetAsync(x => x.ProductItemId == orderPostDto.ProductItemId && x.CardId == orderPostDto.CardId);
                    if (order == null)
                    {

                        order = new ProductOrder
                        {
                            ProductItemId = orderPostDto.ProductItemId,
                            Count = orderPostDto.Count,
                            CardId = orderPostDto.CardId,
                            Price = price
                        };
                        await _unitOfWork.ProductOrderRepository.CreateAsync(order);
                    }
                    else
                    {
                        order.Count += orderPostDto.Count;
                        order.Price += price;
                    }

                    var card = await _unitOfWork.CardRepository.GetAsync(x => x.Id == orderPostDto.CardId);
                    card.TotalAmount += price;
                    await _unitOfWork.CommitAsync();
                }

                var productItem = await _unitOfWork.ProductItemRepository.GetAsync(x => x.Id == orderPostDto.ProductItemId && !x.IsDeleted);
                if (productItem == null)
                    throw new ItemNotFoundException("Product not found");
                productItem.Count -= orderPostDto.Count;
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new Exception("Product is out of stock");
            }

        }
    }
}
