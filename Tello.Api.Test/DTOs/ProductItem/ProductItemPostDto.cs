using System.ComponentModel.DataAnnotations;
using System.Composition.Convention;

namespace Tello.Api.Test.DTOs.ProductItem
{
    public class ProductItemPostDto
    {
        public float CostPrice { get; set; }
        public float SalePrice { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public int VariationOptionId { get; set; }
        //public int CategoryId { get;  }
    }
}