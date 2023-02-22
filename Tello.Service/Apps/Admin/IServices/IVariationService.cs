using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;

namespace Tello.Service.Apps.Admin.IServices
{
    public interface IVariationService
    {
        Task CreateAsync(VariationPostDto variationPostDto);
        Task UpdateAsync(int id, VariationPostDto variationPostDto);
        Task<VariationGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore (int id);
        PaginatedListDto<VariationListItemDto> GetAll(int page);
        PaginatedListDto<VariationListItemDto> GetAllDeleted(int page);
        List<VariationListItemDto> GetAll1();
    }
}
