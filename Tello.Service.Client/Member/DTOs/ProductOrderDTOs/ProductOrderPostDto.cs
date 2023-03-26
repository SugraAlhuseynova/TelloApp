using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;

namespace Tello.Service.Client.Member.DTOs.ProductOrderDTOs
{
    public class ProductOrderPostDto
    {
        //public ProductOrder()
        //{
        //    Price = Count * ProductItem.SalePrice;
        //}
        public int CartId { get; set; }
        public int ProductItemId { get; set; }
        public int Count { get; set; }
        //public double Price { get; set; }
    }
}
