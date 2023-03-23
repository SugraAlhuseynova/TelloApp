using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Enums;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.DTOs.CardDTOs;

namespace Tello.Service.Apps.Admin.IServices.IEntityServices
{
    public interface ICardService
    {
        Task<CardGetDto> GetAsync(int id);
        List<CardListItemDto> GetAll();
        PaginatedListDto<CardListItemDto> GetAll(int page);
        Task ChangeStatus(int id, byte status);
    }
}
