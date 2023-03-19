using AutoMapper;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Service.Client.Member.DTOs.CategoryDTOs;
using Tello.Service.Client.Member.IServices;

namespace Tello.Service.Client.Member.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<CategoryFilterDto> GetAll()
        {
            var query = _unitOfWork.CategoryRepository.GetAll(x => !x.IsDeleted);
            List<CategoryFilterDto> items = _mapper.Map<List<CategoryFilterDto>>(query);
            return items;
        }

        public async Task<CategoryGetDto> GetAsync(int id)
        {
            Category category = await _unitOfWork.CategoryRepository.GetAsync(x=>x.Id == id);
            CategoryGetDto getDto = _mapper.Map<CategoryGetDto>(category);
            return getDto;
        }
    }
}
