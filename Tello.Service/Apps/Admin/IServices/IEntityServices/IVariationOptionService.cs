using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.VariationOptionDTOs;

namespace Tello.Service.Apps.Admin.IServices.IEntityServices
{
    public interface IVariationOptionService
    {
        Task CreateAsync(VariationOptionPostDto variationOptionPostDto);
        Task UpdateAsync(int id, VariationOptionPostDto variationOptionPostDto);
        Task<VariationOptionGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore(int id);
        List<VariationOptionSelectDto> GetAll();
        PaginatedListDto<VariationOptionListItemDto> GetAll(int page);
        PaginatedListDto<VariationOptionListItemDto> GetAllDeleted(int page);
    }
}
