using Tello.Api.JWT;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Data.Repositories;
using Tello.Data.UnitOfWork;
using Tello.Service.Apps.Admin.Implementations.EntityServices;
using Tello.Service.Apps.Admin.Implementations.Service;
using Tello.Service.Apps.Admin.Implementations.Storage;
using Tello.Service.Apps.Admin.IServices.IEntityServices;
using Tello.Service.Apps.Admin.IServices.Storage;
using Tello.Service.Profiles;

namespace Tello.Api.ServiceExtentions
{
    public static class AddServiceService
    {
        public static void AddAdminService(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MapProfile));
            builder.Services.AddAutoMapper(typeof(Service.Client.Profiles.MapProfile));
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ISlideRepository, SlideRepository>();
            builder.Services.AddScoped<IVariationRepository, VariationRepository>();
            builder.Services.AddScoped<IVariationCategoryRepository, VariationCategoryRepository>();
            builder.Services.AddScoped<IVariationOptionRepository, VariationOptionRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();
            builder.Services.AddScoped<IProductItemVariationRepository, ProductItemVariationRepository>();
            builder.Services.AddScoped<ISettingRepository, SettingRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISlideService, SlideService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IVariationService, VariationService>();
            builder.Services.AddScoped<IVariationCategoryService, VariationCategoryService>();
            builder.Services.AddScoped<IVariationOptionService, VariationOptionService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductItemService, ProductItemService>();
            builder.Services.AddScoped<IProductItemVariationService, ProductItemVariationService>();
            builder.Services.AddScoped<ISettingService, SettingService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ICardService, CardService>();
            
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IJWTSerivce, JWTService>();
        }
        public static void AddMemberService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<Service.Client.Member.IServices.ICategoryService, Service.Client.Member.Implementations.CategoryService>();
            builder.Services.AddScoped<Service.Client.Member.IServices.IBrandService, Service.Client.Member.Implementations.BrandService>();
            builder.Services.AddScoped<Service.Client.Member.IServices.IProductItemService, Service.Client.Member.Implementations.ProductItemService>();
            builder.Services.AddScoped<Service.Client.Member.IServices.ICommentService, Service.Client.Member.Implementations.CommentService>();
        }
        public static void AddStorageService<T>(this WebApplicationBuilder builder) where T : class, IStorage
        {
            builder.Services.AddScoped<IStorage, T>();
            builder.Services.AddScoped<IStorageService, StorageService>();
        }
    }
}
