using System.Reflection;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Category;
using Tello.Api.Test.DTOs.ProductItem;

namespace Tello.Api.Test.ViewModels.ProductItem
{
    public class ProductItemIndexViewModel
    {
        public PaginatedListDto<ProductItemListItemGetDto> PaginatedList { get; set; }
        public List<CategoryGetDto> Categories { get; set; }

    }
}
