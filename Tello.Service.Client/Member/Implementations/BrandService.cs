using AutoMapper;
using Tello.Core.Entities;
using Tello.Core.IUnitOfWork;
using Tello.Service.Client.Member.DTOs.BrandDTOs;
using Tello.Service.Client.Member.IServices;

namespace Tello.Service.Client.Member.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<BrandFilterDto> GetAll()
        {
            var query = _unitOfWork.BrandRepository.GetAll(x => !x.IsDeleted);
            List<BrandFilterDto> items = _mapper.Map<List<BrandFilterDto>>(query);
            return items;
        }

        public async Task<BrandGetDto> GetAsync(int id)
        {
            Brand brand = await _unitOfWork.BrandRepository.GetAsync(x => x.Id == id);
            BrandGetDto getDto = _mapper.Map<BrandGetDto>(brand);
            return getDto;
        }
    }
}
