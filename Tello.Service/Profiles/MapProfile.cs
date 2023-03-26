using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tello.Core.Entities;
using Tello.Service.Apps.Admin.DTOs.AppUserDTOs;
using Tello.Service.Apps.Admin.DTOs.AppUserDTOs.RoleDtos;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.DTOs.CartDTOs;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.DTOs.CommentDTOs;
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
            //CategoryAdmin
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<Category, CategoryListItemDto>();
            //BrandAdmin
            CreateMap<Brand, BrandGetDto>();
            CreateMap<BrandPostDto, Brand>();
            CreateMap<Brand, BrandListItemDto>();
            //Slide
            CreateMap<Slide, SlideGetDto>();
            CreateMap<SlidePostDto, Slide>();
            CreateMap<Slide, SlideListItemDto>();

            //Variation
            CreateMap<Variation, VariationGetDto>();
            CreateMap<VariationPostDto, Variation>();
            CreateMap<Variation, VariationListItemDto>();

            //VariationCategory
            CreateMap<VariationCategory, VariationCategoryGetDto>();
            CreateMap<VariationCategoryPostDto, VariationCategory>();
            CreateMap<VariationCategory, VariationCategoryListItemDto>();

            //VariationOption
            CreateMap<VariationOption, VariationOptionGetDto>();
            CreateMap<VariationOptionPostDto, VariationOption>();
            CreateMap<VariationOption, VariationOptionListItemDto>();
            CreateMap<VariationOption, VariationOptionSelectDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.VariationCategory.Category.Name))
                .ForMember(dest => dest.VariationName, opt => opt.MapFrom(src => src.VariationCategory.Variation.Name));

            //Product
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductPostDto, Product>();
            CreateMap<Product, ProductListItemDto>();
            //.ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.ProductItems.Count()));
            CreateMap<Product, ProductSelectDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

            //ProductItem
            CreateMap<ProductItem, ProductItemGetDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Product.Brand.Name))
                .ForMember(dest => dest.ProductItemVariationIds, opt => opt.MapFrom(src => src.ProductItemVariations.Select(x => x.Id).ToList()));
            CreateMap<ProductItem, ProductItemSelectDto>()
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name))
               .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Product.Brand.Name));
            CreateMap<ProductItem, ProductItemListItemDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Product.Brand.Name));
            CreateMap<ProductItemPostDto, ProductItem>();
            CreateMap<ProductItem, ProductItemCardDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Product.Brand.Name))
                .ForMember(dest => dest.Variations, opt => opt.MapFrom(src => src.ProductItemVariations.Select(x => x.VariationOption)
                .Where(x => x.VariationCategory.Variation.Name == "Color" || x.VariationCategory.Variation.Name == "RAM")
                .ToDictionary(x => x.VariationCategory.Variation.Name, x => x.Value)));


            //ProductItemVariation
            CreateMap<ProductItemVariation, ProductItemVariationGetDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductItem.Product.Name))
                .ForMember(dest => dest.VariationName, opt => opt.MapFrom(src => src.VariationOption.VariationCategory.Variation.Name));
            CreateMap<ProductItemVariationPostDto, ProductItemVariation>();
            CreateMap<ProductItemVariation, ProductItemVariationListItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductItem.Product.Name))
                .ForMember(dest => dest.VariationName, opt => opt.MapFrom(src => src.VariationOption.VariationCategory.Variation.Name));

            //user
            CreateMap<UserPostDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<AppUser, UserGetDto>();
            CreateMap<IdentityRole, RoleGetDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Name));

            //setting
            CreateMap<Setting, SettingGetDto>();
            CreateMap<SettingPostDto, Setting>();

            //comment
            CreateMap<Comment, CommentGetDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductItem.Product.Name))
                .ForMember(dest => dest.AppName, opt => opt.MapFrom(src => src.AppUser.Fullname))
                .ForMember(dest => dest.Variations, opt => opt.MapFrom(src => src.ProductItem.ProductItemVariations.Select(x => x.VariationOption).Where(x => x.VariationCategory.Variation.Name == "Color" || x.VariationCategory.Variation.Name == "RAM").Select(x => x.Value).ToList()));
            CreateMap<Comment, CommentListItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductItem.Product.Name))
                .ForMember(dest => dest.AppName, opt => opt.MapFrom(src => src.AppUser.Fullname));

            //card
            CreateMap<Cart, CartGetDto>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()));
            CreateMap<Cart, CartListItemDto>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy, dd MMMM, HH:mm")))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.ProductOrders.Count()));
        }
    }
}
