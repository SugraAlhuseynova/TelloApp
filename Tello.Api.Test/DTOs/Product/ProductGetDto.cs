﻿namespace Tello.Api.Test.DTOs.Product
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Count { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
