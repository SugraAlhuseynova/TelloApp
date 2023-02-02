﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.DTOs;
using Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs;

namespace Tello.Service.Apps.Admin.IServices
{
    public interface IVariationCategoryService
    {
        Task CreateAsync(VariationCategoryPostDto vsPostDto);
        Task UpdateAsync(int id, VariationCategoryPostDto vcPostDto);
        Task<VariationCategoryGetDto> GetAsync(int id);
        Task Delete(int id);
        Task Restore(int id);
        PaginatedListDto<VariationCategoryListItemDto> GetAll(int page);
        PaginatedListDto<VariationCategoryListItemDto> GetAllDeleted(int page);

    }
}