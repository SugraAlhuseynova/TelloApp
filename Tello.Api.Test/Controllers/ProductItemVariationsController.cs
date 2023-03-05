using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.ProductItem;
using Tello.Api.Test.DTOs.ProductItemVariation;
using Tello.Api.Test.DTOs.VariationOption;
using Tello.Api.Test.ViewModels.ProductItemVariation;

namespace Tello.Api.Test.Controllers
{
    public class ProductItemVariationsController : Controller
    {
        string endpoint = null;
        HttpResponseMessage response = new();
        public async Task<IActionResult> Index(int page)
        {
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/productitemvariations/all/" + page;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<ProductItemVariationListItemGetDto> items = JsonConvert.DeserializeObject<PaginatedListDto<ProductItemVariationListItemGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        //add view
        public async Task<IActionResult> GetAllDeleted(int page)
        {
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/productitemvariations/all/deleted/" + page;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<ProductItemVariationListItemGetDto> items = JsonConvert.DeserializeObject<PaginatedListDto<ProductItemVariationListItemGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Create()
        {
            string endpointProductItem = "https://localhost:7067/api/admin/productitems/all";
            string endpointVariationOption = "https://localhost:7067/api/admin/variationoptions/all";
            HttpResponseMessage responseProductItem = null;
            HttpResponseMessage responseVariationOption = null;
            using (HttpClient client = new HttpClient())
            {
                responseProductItem = await client.GetAsync(endpointProductItem);
                responseVariationOption = await client.GetAsync(endpointVariationOption);
            }
            if (responseProductItem != null && responseProductItem.StatusCode == System.Net.HttpStatusCode.OK &&
                responseVariationOption != null && responseVariationOption.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string contentProductItem = await responseProductItem.Content.ReadAsStringAsync();
                string contentVariationOption = await responseVariationOption.Content.ReadAsStringAsync();
                List<ProductItemSelectDto> productItems = JsonConvert.DeserializeObject<List<ProductItemSelectDto>>(contentProductItem);
                List<VariationOptionSelectDto> variationOptions = JsonConvert.DeserializeObject<List<VariationOptionSelectDto>>(contentVariationOption);
                ProductItemVariationViewModel productItemVariationVM = new ProductItemVariationViewModel
                {
                    ProductItems = productItems,
                    VariationOptions = variationOptions
                };
                return View(productItemVariationVM);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductItemVariationPostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                string endpointProductItem = "https://localhost:7067/api/admin/productitems/all";
                string endpointVariationOption = "https://localhost:7067/api/admin/variationoptions/all";
                HttpResponseMessage responseProductItem = null;
                HttpResponseMessage responseVariationOption = null;
                using (HttpClient client = new HttpClient())
                {
                    responseProductItem = await client.GetAsync(endpointProductItem);
                    responseVariationOption = await client.GetAsync(endpointVariationOption);
                }
                if (responseProductItem != null && responseProductItem.StatusCode == System.Net.HttpStatusCode.OK &&
                    responseVariationOption != null && responseVariationOption.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string contentProductItem = await responseProductItem.Content.ReadAsStringAsync();
                    string contentVariationOption = await responseVariationOption.Content.ReadAsStringAsync();
                    List<ProductItemSelectDto> productItems = JsonConvert.DeserializeObject<List<ProductItemSelectDto>>(contentProductItem);
                    List<VariationOptionSelectDto> variationOptions = JsonConvert.DeserializeObject<List<VariationOptionSelectDto>>(contentVariationOption);
                    ProductItemVariationViewModel productItemVariationVM = new ProductItemVariationViewModel
                    {
                        ProductItems = productItems,
                        VariationOptions = variationOptions
                    };
                    return View(productItemVariationVM);
                }
            }
            endpoint = "https://localhost:7067/api/admin/productitemvariations/";
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                response = await client.PostAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            endpoint = "https://localhost:7067/api/admin/productitemvariations/" + id;
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
            endpoint = "https://localhost:7067/api/admin/productitemvariations/restore/" + id;
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
        public async Task<IActionResult> Edit(int id)
        {
            string endpointProductItem = "https://localhost:7067/api/admin/productitems/all";
            string endpointVariationOption = "https://localhost:7067/api/admin/variationoptions/all";
            endpoint = "https://localhost:7067/api/admin/productitemvariations/" + id;
            HttpResponseMessage responseProductItem = null;
            HttpResponseMessage responseVariationOption = null;
            using (HttpClient client = new HttpClient())
            {
                responseProductItem = await client.GetAsync(endpointProductItem);
                responseVariationOption = await client.GetAsync(endpointVariationOption);
                response = await client.GetAsync(endpoint);
            }
            if (responseProductItem != null && responseProductItem.StatusCode == System.Net.HttpStatusCode.OK &&
                responseVariationOption != null && responseVariationOption.StatusCode == System.Net.HttpStatusCode.OK &&
                response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string contentProductItem = await responseProductItem.Content.ReadAsStringAsync();
                string contentVariationOption = await responseVariationOption.Content.ReadAsStringAsync();
                string content = await response.Content.ReadAsStringAsync();
                List<ProductItemSelectDto> productItems = JsonConvert.DeserializeObject<List<ProductItemSelectDto>>(contentProductItem);
                List<VariationOptionSelectDto> variationOptions = JsonConvert.DeserializeObject<List<VariationOptionSelectDto>>(contentVariationOption);
                ProductItemVariationGetDto getDto = JsonConvert.DeserializeObject<ProductItemVariationGetDto>(content);

                ProductItemVariationViewModel productItemVariationVM = new ProductItemVariationViewModel
                {
                    ProductItems = productItems,
                    VariationOptions = variationOptions,
                    PostDto = new ProductItemVariationPostDto { ProductItemId = getDto.ProductItemId, VariationOptionId = getDto.VariationOptionId }
                };
                return View(productItemVariationVM);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductItemVariationPostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                string endpointProductItem = "https://localhost:7067/api/admin/productitems/all";
                string endpointVariationOption = "https://localhost:7067/api/admin/variationoptions/all";
                endpoint = "https://localhost:7067/api/admin/productitemvariations/" + id;
                HttpResponseMessage responseProductItem = null;
                HttpResponseMessage responseVariationOption = null;
                using (HttpClient client = new HttpClient())
                {
                    responseProductItem = await client.GetAsync(endpointProductItem);
                    responseVariationOption = await client.GetAsync(endpointVariationOption);
                    response = await client.GetAsync(endpoint);
                }
                if (responseProductItem != null && responseProductItem.StatusCode == System.Net.HttpStatusCode.OK &&
                    responseVariationOption != null && responseVariationOption.StatusCode == System.Net.HttpStatusCode.OK &&
                    response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string contentProductItem = await responseProductItem.Content.ReadAsStringAsync();
                    string contentVariationOption = await responseVariationOption.Content.ReadAsStringAsync();
                    string contentGet = await response.Content.ReadAsStringAsync();
                    List<ProductItemSelectDto> productItems = JsonConvert.DeserializeObject<List<ProductItemSelectDto>>(contentProductItem);
                    List<VariationOptionSelectDto> variationOptions = JsonConvert.DeserializeObject<List<VariationOptionSelectDto>>(contentVariationOption);
                    ProductItemVariationGetDto getDto = JsonConvert.DeserializeObject<ProductItemVariationGetDto>(contentGet);

                    ProductItemVariationViewModel productItemVariationVM = new ProductItemVariationViewModel
                    {
                        ProductItems = productItems,
                        VariationOptions = variationOptions,
                        PostDto = new ProductItemVariationPostDto { ProductItemId = getDto.ProductItemId, VariationOptionId = getDto.VariationOptionId }
                    };
                    return View(productItemVariationVM);
                }

            }
            endpoint = "https://localhost:7067/api/admin/productitemvariations/"+id;
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

    }
}
