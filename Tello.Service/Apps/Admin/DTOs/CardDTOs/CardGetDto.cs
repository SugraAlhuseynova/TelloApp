using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.Enums;
using Tello.Service.Apps.Admin.DTOs.ProductItemDTOs;

namespace Tello.Service.Apps.Admin.DTOs.CardDTOs
{
    public class CardGetDto
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserFullname { get; set; }
        public List<ProductItemCardDto> ProductItems { get; set; }
    }
}
