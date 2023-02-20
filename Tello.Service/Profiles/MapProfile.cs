using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Service.Apps.Admin.DTOs.AppUserDTOs;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.DTOs.ProductDTOs;
using Tello.Service.Apps.Admin.DTOs.ProductItemDTOs;
using Tello.Service.Apps.Admin.DTOs.ProductItemVariationDTOs;
using Tello.Service.Apps.Admin.DTOs.SettingDTOs;
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
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductPostDto, Product>();
            CreateMap<Product, ProductListItemDto>();
            CreateMap<ProductItem, ProductItemGetDto>();
            CreateMap<ProductItemPostDto, ProductItem>();
            CreateMap<ProductItem, ProductItemListItemDto>();
            CreateMap<ProductItemVariation, ProductItemVariationGetDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductItem.Product.Name))
                .ForMember(dest => dest.VariationName , opt => opt.MapFrom(src => src.VariationOption.VariationCategory.Variation.Name));
            CreateMap<ProductItemVariationPostDto, ProductItemVariation>();
            CreateMap<ProductItemVariation, ProductItemVariationListItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductItem.Product.Name))
                .ForMember(dest => dest.VariationName, opt => opt.MapFrom(src => src.VariationOption.VariationCategory.Variation.Name));
            CreateMap<UserPostDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<Setting, SettingGetDto>();
            CreateMap<SettingPostDto, Setting>();
        
        }
    }
}
