using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tello.Core.Entities;
using Tello.Core.Enums;
using Tello.Core.IUnitOfWork;
using Tello.Service.Client.Exceptions;
using Tello.Service.Client.Member.DTOs.ProductOrderDTOs;
using Tello.Service.Client.Member.IServices;

namespace Tello.Api.Apps.Member.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService) 
        {
            _cardService = cardService;
        }
        [Authorize]
        [HttpPost]
        public async Task CreateOrder(ProductOrderPostDto orderPostDto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _cardService.CreateOrder(userId, orderPostDto);
        }


    }
}
