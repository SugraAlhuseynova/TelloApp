﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Enums;

namespace Tello.Service.Apps.Admin.DTOs.CardDTOs
{
    public class CardListItemDto
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string CreatedAt { get; set; }
        public string UserFullname { get; set; }
    }
}
