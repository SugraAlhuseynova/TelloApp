using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Product;
using Tello.Api.Test.DTOs.ProductItem;
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
        public async Task<IActionResult> Create()
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
            return RedirectToAction("error", "index");
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
