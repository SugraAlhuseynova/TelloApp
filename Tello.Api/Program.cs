using FluentValidation.AspNetCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Tello.Api.ServiceExtentions;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Data.DAL;
using Tello.Data.Repositories;
using Tello.Data.UnitOfWork;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.Implementations;
using Tello.Service.Apps.Admin.IServices;
using Tello.Service.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryPostDto>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("TelloApi"));
});

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ISlideRepository, SlideRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISlideService, SlideService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();