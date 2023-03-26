using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Client.Member.DTOs.ProductOrderDTOs;

namespace Tello.Service.Client.Member.IServices
{
    public interface ICartService
    {
        public Task CreateOrder(string userId, ProductOrderPostDto postDto);
        public Task DeleteOrder(int orderId);
        public Task DecreaseOrder(int orderId);
        public Task IncreaseOrder(int orderId);
    }
}
