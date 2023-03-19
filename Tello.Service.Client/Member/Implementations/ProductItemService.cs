using AutoMapper;
using Tello.Core.IUnitOfWork;
using Tello.Service.Client.Exceptions;
using Tello.Service.Client.Member.DTOs;
using Tello.Service.Client.Member.DTOs.ProductItemDTOs;
using Tello.Service.Client.Member.IServices;

namespace Tello.Service.Client.Member.Implementations
{
    public class ProductItemService : IProductItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PaginationListDto<ProductItemListItemGetDto> GetAll(int page)
        {
            var paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PageCountProduct").Result.Value);
            var query = _unitOfWork.ProductItemRepository.GetAll(x => !x.IsDeleted, "Comments", "Product.Category", "ProductItemVariations.VariationOption.VariationCategory.Variation");
            var items = _mapper.Map<List<ProductItemListItemGetDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            PaginationListDto<ProductItemListItemGetDto> listDto = new PaginationListDto<ProductItemListItemGetDto>(items, query.Count(), page, paginationCount);
            return listDto;
        }

        public async Task<ProductItemGetDto> GetAsync(int id)
        {
            var product = await _unitOfWork.ProductItemRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "Comments.AppUser", "Product.Category", "Product.Brand", "ProductItemVariations.VariationOption.VariationCategory.Variation");
            if (product == null)
                throw new ItemNotFoundException("product not found");
            var productDto = _mapper.Map<ProductItemGetDto>(product);
            return productDto;
        }
    }
}
