using AutoMapper;
using Tello.Core.Entities;
using Tello.Service.Client.Member.DTOs.BrandDTOs;
using Tello.Service.Client.Member.DTOs.CategoryDTOs;
using Tello.Service.Client.Member.DTOs.CommentDTOs;
using Tello.Service.Client.Member.DTOs.ProductItemDTOs;
using Tello.Service.Client.Member.DTOs.UserDTOs;

namespace Tello.Service.Client.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //CategoryMember
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryFilterDto>();
          
            //BrandMember
            CreateMap<Brand, BrandGetDto>();
            CreateMap<Brand, BrandFilterDto>();
         
            //ProductItem
            CreateMap<ProductItem, ProductItemGetDto>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Product.Category.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product.Brand.Name))
                //.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Count()))
                .ForMember(dest => dest.Variations, opt => opt.MapFrom(src => src.ProductItemVariations
                    .Select(x => x.VariationOption).Select(x => x.Value).ToList()));

            CreateMap<ProductItem, ProductItemListItemGetDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Product.Category.Name))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count()))
                .ForMember(dest => dest.Variations, opt => opt.MapFrom(src => src.ProductItemVariations
                    .Select(x => x.VariationOption)
                    .Where(x => x.VariationCategory.Variation.Name == "Color" || x.VariationCategory.Variation.Name == "Memory Storage")
                    .Select(x => x.Value).ToList()));


            //user
            CreateMap<UserPostDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            //CreateMap<AppUser, UserGetDto>();
            //CreateMap<IdentityRole, RoleGetDto>()
            //    .ForMember(dest => dest.Role, opt=>opt.MapFrom(src => src.Name));

            //setting
            //CreateMap<Setting, SettingGetDto>();
            //CreateMap<SettingPostDto, Setting>();

            //comment
            CreateMap<CommentPostDto, Comment>();
            CreateMap<Comment, CommentGetDto>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.Fullname));

            ////card
            //CreateMap<Card, CardGetDto>();
            //CreateMap<Card, CardListItemDto>();
        }
    }
}
