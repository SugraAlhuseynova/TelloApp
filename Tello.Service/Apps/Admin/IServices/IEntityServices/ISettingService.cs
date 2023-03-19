using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.DTOs.SettingDTOs;

namespace Tello.Service.Apps.Admin.IServices.IEntityServices
{
    public interface ISettingService
    {
        Task CreateAsync(SettingPostDto postDto);
        Task UpdateAsync(int id, SettingPostDto postDto);
        Task<SettingGetDto> GetAsync(int id);
        Task Delete(int id);
        PaginatedSettingListDto<SettingGetDto> GetAll(int page);
    }
}
