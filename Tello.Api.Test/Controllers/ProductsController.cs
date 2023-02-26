using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Differencing;
using Newtonsoft.Json;
using System.Text;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Brand;
using Tello.Api.Test.DTOs.Category;
using Tello.Api.Test.DTOs.Product;
using Tello.Api.Test.ViewModels.Product;
using Tello.Api.Test.ViewModels.VariationCategory;

namespace Tello.Api.Test.Controllers
{
    public class ProductsController : Controller
    {
        HttpResponseMessage response = new();
        string endpoint = null;

        public async Task<IActionResult> Index(int page)
        {
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/products/all/"+page;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync(); 
                PaginatedListDto<ProductListItemGetDto> items = JsonConvert.DeserializeObject<PaginatedListDto<ProductListItemGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("error","home");
        }
        public async Task<IActionResult> GetAllDeleted(int page)
        {
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/products/all/deleted/" + page;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<ProductListItemGetDto> items = JsonConvert.DeserializeObject<PaginatedListDto<ProductListItemGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Create()
        {
            string categoryEndpoint = "https://localhost:7067/api/admin/categories/all";
            string brandEndpoint = "https://localhost:7067/api/admin/brands/";

            HttpResponseMessage responseCategory = null;
            HttpResponseMessage responseBrand = null;
            using (HttpClient client = new HttpClient())
            {
                responseCategory = await client.GetAsync(categoryEndpoint);
                responseBrand = await client.GetAsync(brandEndpoint);
            }
            if (responseCategory.StatusCode == System.Net.HttpStatusCode.OK && responseBrand.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string contentCategory = await responseCategory.Content.ReadAsStringAsync();
                string contentBrand = await responseBrand.Content.ReadAsStringAsync();

                ProductViewModel productVM = new ProductViewModel
                {
                    Categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(contentCategory),
                    Brands = JsonConvert.DeserializeObject<List<BrandGetDto>>(contentBrand)
                };
                return View(productVM);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductPostDto product)
        {
            if (!ModelState.IsValid)
            {
                string categoryEndpoint = "https://localhost:7067/api/admin/categories/all";
                string brandEndpoint = "https://localhost:7067/api/admin/brands/";

                HttpResponseMessage responseCategory = null;
                HttpResponseMessage responseBrand = null;
                using (HttpClient client = new HttpClient())
                {
                    responseCategory = await client.GetAsync(categoryEndpoint);
                    responseBrand = await client.GetAsync(brandEndpoint);
                }
                if (responseCategory.StatusCode == System.Net.HttpStatusCode.OK && responseBrand.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string contentCategory = await responseCategory.Content.ReadAsStringAsync();
                    string contentBrand = await responseBrand.Content.ReadAsStringAsync();

                    ProductViewModel productVM = new ProductViewModel
                    {
                        Categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(contentCategory),
                        Brands = JsonConvert.DeserializeObject<List<BrandGetDto>>(contentBrand)
                    };
                    return View(productVM);
                }
                else
                    return RedirectToAction("error", "home");
            }
            endpoint = "https://localhost:7067/api/admin/products/";
            StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            
            using (HttpClient client = new HttpClient())
            {
                response = await client.PostAsync(endpoint, content);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Edit(int id)
        {
            string categoryEndpoint = "https://localhost:7067/api/admin/categories/all";
            string brandEndpoint = "https://localhost:7067/api/admin/brands/";
            endpoint = "https://localhost:7067/api/admin/products/" + id;

            HttpResponseMessage responseCategory = null;
            HttpResponseMessage responseBrand = null;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
                responseCategory = await client.GetAsync(categoryEndpoint);
                responseBrand = await client.GetAsync(brandEndpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK &&
                responseCategory.StatusCode == System.Net.HttpStatusCode.OK &&
                responseBrand.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                string contentCategory = await responseCategory.Content.ReadAsStringAsync();
                string contentBrand = await responseBrand.Content.ReadAsStringAsync();

                ProductGetDto item = JsonConvert.DeserializeObject<ProductGetDto>(content);
                ProductViewModel productVM = new ProductViewModel
                {
                    Categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(contentCategory),
                    Brands = JsonConvert.DeserializeObject<List<BrandGetDto>>(contentBrand),
                    Product = new ProductPostDto { BrandId = item.BrandId, CategoryId = item.CategoryId, Desc = item.Desc, Name = item.Name }
                };
                return View(productVM);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductPostDto product)
        {
            if (!ModelState.IsValid)
            {
                string categoryEndpoint = "https://localhost:7067/api/admin/categories/all";
                string brandEndpoint = "https://localhost:7067/api/admin/brands/";

                HttpResponseMessage responseCategory = null;
                HttpResponseMessage responseBrand = null;
                using (HttpClient client = new HttpClient())
                {
                    responseCategory = await client.GetAsync(categoryEndpoint);
                    responseBrand = await client.GetAsync(brandEndpoint);
                }
                if (responseCategory.StatusCode == System.Net.HttpStatusCode.OK && responseBrand.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string contentCategory = await responseCategory.Content.ReadAsStringAsync();
                    string contentBrand = await responseBrand.Content.ReadAsStringAsync();

                    ProductViewModel productVM = new ProductViewModel
                    {
                        Categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(contentCategory),
                        Brands = JsonConvert.DeserializeObject<List<BrandGetDto>>(contentBrand)
                    };
                    return View(productVM);
                }
            }
            endpoint = "https://localhost:7067/api/admin/products/"+id;
            StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                response = await client.PutAsync(endpoint, content);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            endpoint = "https://localhost:7067/api/admin/products/" + id;
            using (HttpClient client = new HttpClient())
            {
                response = await client.DeleteAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Restore(int id)
        {
            endpoint = "https://localhost:7067/api/admin/products/restore/" + id;
            using (HttpClient client = new HttpClient())
            {
                response = await client.DeleteAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Detail(int id)
        {
            endpoint = "https://localhost:7067/api/admin/products/" + id;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                ProductGetDto getDto = JsonConvert.DeserializeObject<ProductGetDto>(content);
                return View(getDto);
            }
            return RedirectToAction("error", "home");
        }

    }
}
