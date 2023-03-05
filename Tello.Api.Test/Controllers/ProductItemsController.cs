using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop.Implementation;
using Newtonsoft.Json;
using System.Text;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Category;
using Tello.Api.Test.DTOs.Product;
using Tello.Api.Test.DTOs.ProductItem;
using Tello.Api.Test.DTOs.Variation;
using Tello.Api.Test.DTOs.VariationOption;
using Tello.Api.Test.ViewModels.ProductItem;

namespace Tello.Api.Test.Controllers
{
    public class ProductItemsController : Controller
    {
        string endpoint = null;
        HttpResponseMessage response = new();
        public async Task<IActionResult> Index(int page)
        {
            if (page == 0)
                page = 1;

            endpoint = "https://localhost:7067/api/admin/productitems/all/" + page;
            string endpointCategory = "https://localhost:7067/api/admin/categories/all";
            HttpResponseMessage responseCategory = null;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
                responseCategory = await client.GetAsync(endpointCategory);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK &&
                responseCategory != null && responseCategory.StatusCode == System.Net.HttpStatusCode.OK )
            {
                string content = await response.Content.ReadAsStringAsync();
                string contentCategory = await responseCategory.Content.ReadAsStringAsync();
                PaginatedListDto<ProductItemListItemGetDto> items = JsonConvert.DeserializeObject<PaginatedListDto<ProductItemListItemGetDto>>(content);
                List<CategoryGetDto> categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(contentCategory);
                ProductItemIndexViewModel viewModel = new ProductItemIndexViewModel
                {
                    PaginatedList = items,
                    Categories = categories
                };
                return View(viewModel);
            }
            return RedirectToAction("error", "index");
        }
        public async Task<IActionResult> GetAllDeleted(int page)
        {
            if (page == 0)
                page = 1;

            endpoint = "https://localhost:7067/api/admin/productitems/all/deleted/" + page;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<ProductItemListItemGetDto> items = JsonConvert.DeserializeObject<PaginatedListDto<ProductItemListItemGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "index");
        }
        public async Task<IActionResult> PreCreate()
        {
            endpoint = "https://localhost:7067/api/admin/categories/all";
            using (var client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<CategoryListItemGetDto> items = JsonConvert.DeserializeObject<List<CategoryListItemGetDto>>(content);
                return Json(items);
            }
            return Ok();
        }
        public async Task<IActionResult> Create()
        {
            string endpointProduct = "https://localhost:7067/api/admin/products/all/";
            string endpointVariationOption = "https://localhost:7067/api/admin/variationoptions/all";
            string endpointVariation = "https://localhost:7067/api/admin/variations/all";
            string endpointCategory = "https://localhost:7067/api/admin/categories/all";
            HttpResponseMessage responseVariationOption = null;
            HttpResponseMessage responseVariation = null;
            HttpResponseMessage responseCategory = null;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpointProduct);
                responseVariationOption = await client.GetAsync(endpointVariationOption);
                responseVariation = await client.GetAsync(endpointVariation);
                responseCategory = await client.GetAsync(endpointCategory);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK &&
                responseVariationOption != null && responseVariationOption.StatusCode == System.Net.HttpStatusCode.OK &&
                responseVariation != null && responseVariation.StatusCode == System.Net.HttpStatusCode.OK &&
                responseCategory != null && responseCategory.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string contentProduct = await response.Content.ReadAsStringAsync();
                string contentVariationOption = await responseVariationOption.Content.ReadAsStringAsync();
                string contentVariation = await responseVariation.Content.ReadAsStringAsync();
                string contentCategory = await responseVariation.Content.ReadAsStringAsync();
                List<VariationOptionSelectDto> variationOptions = JsonConvert.DeserializeObject<List<VariationOptionSelectDto>>(contentVariationOption);
                List<ProductGetDto> items = JsonConvert.DeserializeObject<List<ProductGetDto>>(contentProduct);
                List<VariationGetDto> variations = JsonConvert.DeserializeObject<List<VariationGetDto>>(contentVariation);
                List<CategoryGetDto> categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(contentCategory);
                ProductItemViewModel productItemVM = new ProductItemViewModel
                {
                    Products = items,
                    VariationOptions = variationOptions,
                    Variations = variations, 
                    Categories = categories
                };
                return View(productItemVM);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductItemPostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                string endpointProduct = "https://localhost:7067/api/admin/products/all/";
                using (HttpClient client = new HttpClient())
                {
                    response = await client.GetAsync(endpointProduct);
                }
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string contentProduct = await response.Content.ReadAsStringAsync();
                    List<ProductGetDto> items = JsonConvert.DeserializeObject<List<ProductGetDto>>(contentProduct);
                    ProductItemViewModel productItemVM = new ProductItemViewModel
                    {
                        Products = items
                    };
                    return View(productItemVM);
                }
            }
            endpoint = "https://localhost:7067/api/admin/productitems/";
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                response = await client.PostAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            string endpointProduct = "https://localhost:7067/api/admin/products/all/";
            endpoint = "https://localhost:7067/api/admin/productitems/" + id;
            HttpResponseMessage responseProducts = new();
            using (HttpClient client = new HttpClient())
            {
                responseProducts = await client.GetAsync(endpointProduct);
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK &&
                responseProducts.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string contentProduct = await responseProducts.Content.ReadAsStringAsync();
                string content = await response.Content.ReadAsStringAsync();
                List<ProductGetDto> items = JsonConvert.DeserializeObject<List<ProductGetDto>>(contentProduct);
                ProductItemGetDto productItem = JsonConvert.DeserializeObject<ProductItemGetDto>(content);


                ProductItemViewModel productItemVM = new ProductItemViewModel
                {
                    Products = items,
                    PostDto = new ProductItemPostDto { CostPrice = productItem.CostPrice, Count = productItem.Count, ProductId = productItem.ProductId, SalePrice = productItem.SalePrice }
                };
                return View(productItemVM);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductItemPostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                string endpointProduct = "https://localhost:7067/api/admin/products/all/";
                endpoint = "https://localhost:7067/api/admin/productitems/" + id;
                HttpResponseMessage responseProducts = new();
                using (HttpClient client = new HttpClient())
                {
                    responseProducts = await client.GetAsync(endpointProduct);
                    response = await client.GetAsync(endpoint);
                }
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK &&
                    responseProducts.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string contentProduct = await responseProducts.Content.ReadAsStringAsync();
                    string contentPut = await response.Content.ReadAsStringAsync();
                    List<ProductGetDto> items = JsonConvert.DeserializeObject<List<ProductGetDto>>(contentProduct);
                    ProductItemGetDto productItem = JsonConvert.DeserializeObject<ProductItemGetDto>(contentPut);


                    ProductItemViewModel productItemVM = new ProductItemViewModel
                    {
                        Products = items,
                        PostDto = new ProductItemPostDto { CostPrice = productItem.CostPrice, Count = productItem.Count, ProductId = productItem.ProductId, SalePrice = productItem.SalePrice }
                    };
                    return View(productItemVM);
                }

            }
            endpoint = "https://localhost:7067/api/admin/productitems/" + id;
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                response = await client.PutAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            endpoint = "https://localhost:7067/api/admin/productitems/" + id;
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
            endpoint = "https://localhost:7067/api/admin/productitems/restore/" + id;
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

    }
}
