using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.DTOs.SettingDTOs;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Exceptions;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SettingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Delete(int id)
        {
            var entity = await _unitOfWork.SettingRepository.GetAsync(x=>x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Setting not found");
            entity.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public PaginatedSettingListDto<SettingGetDto> GetAll(int page)
        {
            int paginationCount = int.Parse(_unitOfWork.SettingRepository.GetAsync(x => x.Key == "PaginationCount").Result.Value);
            var query = _unitOfWork.SettingRepository.GetAll(x => !x.IsDeleted);
            List<SettingGetDto> items = _mapper.Map<List<SettingGetDto>>(query.Skip((page - 1) * paginationCount).Take(paginationCount).ToList());
            var listDto = new PaginatedSettingListDto<SettingGetDto>(items, query.Count(), page, paginationCount);
            return listDto;
        }

        public async Task<SettingGetDto> GetAsync(int id)
        {
            var entity = await _unitOfWork.SettingRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("setting not found");
            var settingDto = _mapper.Map<SettingGetDto>(entity);
            return settingDto;
        }

        public async Task UpdateAsync(int id, SettingPostDto postDto)
        {
            var entity = await _unitOfWork.SettingRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                throw new ItemNotFoundException("Setting not found");
            if (await _unitOfWork.SettingRepository.IsExistAsync(x => x.Key == postDto.Key && x.Value == postDto.Value && !x.IsDeleted && x.Id != id))
                throw new RecordDuplicatedException("Setting already exist");
            entity.Value = postDto.Value;
            entity.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
        }
    }
}
