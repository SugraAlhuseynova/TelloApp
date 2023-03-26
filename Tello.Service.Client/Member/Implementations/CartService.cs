using AutoMapper;
using Tello.Core.Entities;
using Tello.Core.Enums;
using Tello.Core.IUnitOfWork;
using Tello.Service.Client.Exceptions;
using Tello.Service.Client.Member.DTOs.ProductOrderDTOs;
using Tello.Service.Client.Member.IServices;

namespace Tello.Service.Client.Member.Implementations
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
        public async Task CreateOrder(string userId, ProductOrderPostDto orderPostDto)
        {
            var price = orderPostDto.Count * _unitOfWork.ProductItemRepository.GetAsync(x => x.Id == orderPostDto.ProductItemId).Result.SalePrice;
            if (await _unitOfWork.ProductItemRepository.IsExistAsync(x => x.Id == orderPostDto.ProductItemId && !x.IsDeleted && x.Count >= orderPostDto.Count))
            {
                if (orderPostDto.CartId == 0)
                {
                    Cart cart = new Cart
                    {
                        OrderStatus = OrderStatus.Pending,
                        UserId = userId,
                        TotalAmount = price
                    };
                    await _unitOfWork.CartRepository.CreateAsync(cart);
                    await _unitOfWork.CommitAsync();

                    ProductOrders order = new ProductOrders
                    {
                        ProductItemId = orderPostDto.ProductItemId,
                        Count = orderPostDto.Count,
                        CartId = cart.Id,
                        Price = price,
                        Cart = cart
                    };

                    await _unitOfWork.ProductOrderRepository.CreateAsync(order);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    var order = await _unitOfWork.ProductOrderRepository.GetAsync(x => x.ProductItemId == orderPostDto.ProductItemId && x.CartId == orderPostDto.CartId);
                    if (order == null)
                    {

                        order = new ProductOrders
                        {
                            ProductItemId = orderPostDto.ProductItemId,
                            Count = orderPostDto.Count,
                            CartId = orderPostDto.CartId,
                            Price = price
                        };
                        await _unitOfWork.ProductOrderRepository.CreateAsync(order);
                    }
                    else
                    {
                        order.Count += orderPostDto.Count;
                        order.Price += price;
                    }

                    var card = await _unitOfWork.CartRepository.GetAsync(x => x.Id == orderPostDto.CartId);
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

        public async Task DecreaseOrder(int orderId)
        {
            ProductOrders order = await _unitOfWork.ProductOrderRepository.GetAsync(x => x.Id == orderId && !x.IsDeleted, "ProductItem");
            if (order == null)
                throw new ItemNotFoundException("order not found");
            Cart cart = await _unitOfWork.CartRepository.GetAsync(x => x.Id == order.CartId && !x.IsDeleted);
            if (order.Count > 1)
            {
                order.Count -= 1;
                cart.TotalAmount -= order.ProductItem.SalePrice;
                order.Price -= order.ProductItem.SalePrice;
                order.ProductItem.Count += 1;
                await _unitOfWork.CommitAsync();
            }
            else if(order.Count == 1) 
            {
                order.Count = 0;
                order.IsDeleted = true;
                cart.TotalAmount -= order.ProductItem.SalePrice;
                order.ProductItem.Count += 1;
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task DeleteOrder(int orderId)
        {
            ProductOrders order = await _unitOfWork.ProductOrderRepository.GetAsync(x => x.Id == orderId && !x.IsDeleted);
            if (order == null)
                throw new ItemNotFoundException("order not found");
            order.IsDeleted = true;
            order.ProductItem.Count += order.Count;
            await _unitOfWork.CommitAsync();
        }

        public async Task IncreaseOrder(int orderId)
        {
            ProductOrders order = await _unitOfWork.ProductOrderRepository.GetAsync(x => x.Id == orderId && !x.IsDeleted, "ProductItem");
            if (order == null)
                throw new ItemNotFoundException("order not found");
            if (order.ProductItem.Count < 1)
                throw new ItemNotFoundException("product is limited");

            order.Count += 1;
            Cart cart = await _unitOfWork.CartRepository.GetAsync(x => x.Id == order.CartId && !x.IsDeleted);
            order.Price += order.ProductItem.SalePrice;
            cart.TotalAmount += order.ProductItem.SalePrice;
            var productItem = await _unitOfWork.ProductItemRepository.GetAsync(x => x.Id == order.ProductItemId && !x.IsDeleted);
            if (productItem == null)
                throw new ItemNotFoundException("Product not found");
            productItem.Count -= 1;
            await _unitOfWork.CommitAsync();
        }
    }
}
