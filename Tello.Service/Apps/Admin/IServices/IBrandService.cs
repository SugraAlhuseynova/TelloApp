﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;

namespace Tello.Service.Apps.Admin.IServices
{
    public interface IBrandService 
    {
        Task CreateAsync(BrandPostDto PostDto);
        Task UpdateAsync(int id, BrandPostDto PostDto);
        Task<BrandGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore(int id);
        PaginatedListDto<BrandListItemDto> GetAll(int page);
        PaginatedListDto<BrandListItemDto> GetAllDeleted(int page);
    }
}