﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.DTOs.SlideDTOs;
using Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;
using Tello.Service.Apps.Admin.DTOs.VariationOptionDTOs;

namespace Tello.Service.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<BrandPostDto, Brand>();
            CreateMap<Brand, BrandGetDto>();
            CreateMap<Category, CategoryListItemDto>();
            CreateMap<Brand, BrandListItemDto>();
            CreateMap<Slide, SlideGetDto>();
            CreateMap<SlidePostDto, Slide>();
            CreateMap<Slide, SlideListItemDto>();
            CreateMap<Variation, VariationGetDto>();
            CreateMap<VariationPostDto, Variation>();
            CreateMap<Variation, VariationListItemDto>();
            CreateMap<VariationCategory, VariationCategoryGetDto>();
            CreateMap<VariationCategoryPostDto, VariationCategory>();
            CreateMap<VariationCategory, VariationCategoryListItemDto>();
            CreateMap<VariationOption, VariationOptionGetDto>();
            CreateMap<VariationOptionPostDto, VariationOption>();
            CreateMap<VariationOption, VariationOptionListItemDto>();
        }
    }
}