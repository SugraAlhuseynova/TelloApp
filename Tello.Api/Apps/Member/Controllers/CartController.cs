using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tello.Core.Entities;
using Tello.Service.Client.Member.DTOs.ProductOrderDTOs;
using Tello.Service.Client.Member.IServices;

namespace Tello.Api.Apps.Member.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cardService;

        public CartController(ICartService cardService)
        {
            _cardService = cardService;
        }
        [HttpPost]
        public async Task CreateOrder(ProductOrderPostDto orderPostDto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _cardService.CreateOrder(userId, orderPostDto);
        }
        [HttpDelete("decrease")]    
        public async Task DecreaseOrder(int orderId)
        {
            await _cardService.DecreaseOrder(orderId);
        }
        [HttpDelete("delete")]

        public async Task DeleteOrder(int orderId)
        {
            await _cardService.DeleteOrder(orderId);
        }
        [HttpDelete("increase")]

        public async Task IncreaseOrder(int orderId)
        {
            await _cardService.IncreaseOrder(orderId);
        }

    }
}
