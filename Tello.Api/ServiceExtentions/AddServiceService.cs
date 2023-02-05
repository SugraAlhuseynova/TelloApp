using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Data.Repositories;
using Tello.Data.UnitOfWork;
using Tello.Service.Apps.Admin.Implementations;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Profiles;

namespace Tello.Api.ServiceExtentions
{
    public static class AddServiceService
    {
        public static void AddService(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MapProfile));
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ISlideRepository, SlideRepository>();
            builder.Services.AddScoped<IVariationRepository, VariationRepository>();
            builder.Services.AddScoped<IVariationCategoryRepository, VariationCategoryRepository>();
            builder.Services.AddScoped<IVariationOptionRepository, VariationOptionRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();
            builder.Services.AddScoped<IProductItemVariationRepository, ProductItemVariationRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISlideService, SlideService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IVariationService, VariationService>();
            builder.Services.AddScoped<IVariationCategoryService, VariationCategoryService>();
            builder.Services.AddScoped<IVariationOptionService, VariationOptionService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductItemService, ProductItemService>();
            builder.Services.AddScoped<IProductItemVariationService, ProductItemVariationService>();
            
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
