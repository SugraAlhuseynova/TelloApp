using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Client.Member.DTOs.ProductOrderDTOs;

namespace Tello.Service.Client.Member.IServices
{
    public interface ICardService
    {
        public Task CreateOrder(string userId, ProductOrderPostDto postDto);
    }
}
