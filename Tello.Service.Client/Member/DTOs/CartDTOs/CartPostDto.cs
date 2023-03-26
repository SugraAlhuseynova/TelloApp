using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.Enums;

namespace Tello.Service.Client.Member.DTOs.CartDTOs
{
    public class CartPostDto
    {
        public double TotalAmount { get; set; }
        public bool IsConfirmed { get; set; }
        public OrderStatus OrderStatus { get; set; }
        //public List<ProductOrder> ProductOrders { get; set; }

    }
}
