using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.IRepositories;
using Tello.Service.Apps.Admin.DTOs.ProductDTOs;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Apps.Admin.DTOs.ProductItemDTOs;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class ProductItemService : IProductItemService
    {
        public Task CreateAsync(ProductItemPostDto ProductPostDto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PaginatedListDto<ProductItemListItemDto> GetAll(int page)
        {
            throw new NotImplementedException();
        }

        public PaginatedListDto<ProductItemListItemDto> GetAllDeleted(int page)
        {
            throw new NotImplementedException();
        }

        public Task<ProductItemGetDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task Restore(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, ProductItemPostDto ProductPostDto)
        {
            throw new NotImplementedException();
        }
    }
}
