using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Data;
using System.Text;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Category;
using Tello.Api.Test.DTOs.Product;
using Tello.Api.Test.DTOs.ProductItem;
using Tello.Api.Test.DTOs.ProductItemVariation;
using Tello.Api.Test.DTOs.VariationCategory;
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
                responseCategory != null && responseCategory.StatusCode == System.Net.HttpStatusCode.OK)
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
        //public async Task<IActionResult> PreCreate()
        //{
        //    endpoint = "https://localhost:7067/api/admin/categories/all";
        //    using (var client = new HttpClient())
        //    {
        //        response = await client.GetAsync(endpoint);
        //    }
        //    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        string content = await response.Content.ReadAsStringAsync();
        //        List<CategoryListItemGetDto> items = JsonConvert.DeserializeObject<List<CategoryListItemGetDto>>(content);
        //        return Json(items);
        //    }
        //    return Ok();
        //}
        public async Task<IActionResult> Create(int id)
        {
            string endpointProduct = "https://localhost:7067/api/admin/products/all/";
            string endpointVariationOption = "https://localhost:7067/api/admin/variationoptions/all";
            string endpointVariationCategory = "https://localhost:7067/api/admin/variationcategories/all";
            string endpointCategory = "https://localhost:7067/api/admin/categories/all";
            HttpResponseMessage responseVariationOption = null;
            HttpResponseMessage responseVariationCategory = null;
            HttpResponseMessage responseCategory = null;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpointProduct);
                responseVariationOption = await client.GetAsync(endpointVariationOption);
                responseVariationCategory = await client.GetAsync(endpointVariationCategory);
                responseCategory = await client.GetAsync(endpointCategory);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK &&
                responseVariationOption != null && responseVariationOption.StatusCode == System.Net.HttpStatusCode.OK &&
                responseCategory != null && responseCategory.StatusCode == System.Net.HttpStatusCode.OK &&
                responseVariationCategory != null && responseVariationCategory.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string contentProduct = await response.Content.ReadAsStringAsync();
                string contentVariationOption = await responseVariationOption.Content.ReadAsStringAsync();
                string contentCategory = await responseCategory.Content.ReadAsStringAsync();
                string contentVariationCategory = await responseVariationCategory.Content.ReadAsStringAsync();
                List<VariationOptionSelectDto> variationOptions = JsonConvert.DeserializeObject<List<VariationOptionSelectDto>>(contentVariationOption);
                List<ProductSelectDto> items = JsonConvert.DeserializeObject<List<ProductSelectDto>>(contentProduct);
                List<CategoryGetDto> categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(contentCategory);
                List<VariationCategoryGetDto> variationCategories = JsonConvert.DeserializeObject<List<VariationCategoryGetDto>>(contentVariationCategory);
                var category = categories.FirstOrDefault(x => x.Id == id);
                ProductItemViewModel productItemVM = new ProductItemViewModel
                {
                    Products = items.Where(x => x.CategoryName == category.Name).ToList(),
                    VariationOptions = variationOptions.Where(x => x.CategoryName == category.Name).ToList(),
                    VariationCategories = variationCategories.Where(x => x.CategoryId == id).ToList()
                };
                return View(productItemVM);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductItemPostDto postDto)
        {
            #region modelstate
            //if (!ModelState.IsValid)
            //{
            //    string endpointProduct = "https://localhost:7067/api/admin/products/all/";
            //    using (HttpClient client = new HttpClient())
            //    {
            //        response = await client.GetAsync(endpointProduct);
            //    }
            //    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        string contentProduct = await response.Content.ReadAsStringAsync();
            //        List<ProductSelectDto> items = JsonConvert.DeserializeObject<List<ProductSelectDto>>(contentProduct);
            //        ProductItemViewModel productItemVM = new ProductItemViewModel
            //        {
            //            Products = items
            //        };
            //        return View(productItemVM);
            //    }
            //}
            #endregion

            endpoint = "https://localhost:7067/api/admin/productitems/";
            //productitemid vs variationoptionId
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                response = await client.PostAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string endpointProductItem = "https://localhost:7067/api/admin/productitems/all";
                HttpResponseMessage responseProductItems = new();
                using (HttpClient client = new HttpClient())
                {
                    responseProductItems = await client.GetAsync(endpointProductItem);
                }
                if (responseProductItems.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string contentProductItems = await responseProductItems.Content.ReadAsStringAsync();
                    List<ProductItemGetDto> productItemGetDtos = JsonConvert.DeserializeObject<List<ProductItemGetDto>>(contentProductItems);
                    var productItem = productItemGetDtos.OrderByDescending(x => x.Id).FirstOrDefault();
                    foreach (var item in postDto.VariationOptionIds)
                    {
                        var productItemVariation = new ProductItemVariationPostDto
                        {
                            VariationOptionId = item,
                            ProductItemId = productItem.Id
                        };
                        string endpointproductItemVariation = "https://localhost:7067/api/admin/productitemvariations/";
                        HttpResponseMessage responseproductItemVariation = null;
                        StringContent contentproductItemVariation = new StringContent(JsonConvert.SerializeObject(productItemVariation), Encoding.UTF8, "application/json");
                        using (HttpClient client = new HttpClient())
                        {
                            responseproductItemVariation = await client.PostAsync(endpointproductItemVariation, contentproductItemVariation);
                        }
                        if (responseproductItemVariation == null && responseproductItemVariation.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            return RedirectToAction("error", "home");
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction("error", "home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            endpoint = "https://localhost:7067/api/admin/productitems/" + id;
            string endpointVariationCategory = "https://localhost:7067/api/admin/variationcategories/all";
            string endpointVariationOption = "https://localhost:7067/api/admin/variationoptions/all";
            string endpoinProductItemVariation = "https://localhost:7067/api/admin/productitemvariations/all";

            HttpResponseMessage responseVariationCategory = null;
            HttpResponseMessage responseVariationOption = null;
            HttpResponseMessage responseProductItemVariation = null;

            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
                responseVariationCategory = await client.GetAsync(endpointVariationCategory);
                responseVariationOption = await client.GetAsync(endpointVariationOption);
                responseProductItemVariation = await client.GetAsync(endpoinProductItemVariation);

            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK &&
                responseVariationCategory != null && responseVariationCategory.StatusCode == System.Net.HttpStatusCode.OK &&
                responseVariationOption != null && responseVariationOption.StatusCode == System.Net.HttpStatusCode.OK &&
                responseProductItemVariation != null && responseProductItemVariation.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                string contentVariationCategory = await responseVariationCategory.Content.ReadAsStringAsync();
                string contentVariationOption = await responseVariationOption.Content.ReadAsStringAsync();
                string contentProductItemVariation = await responseProductItemVariation.Content.ReadAsStringAsync();

                ProductItemGetDto productItem = JsonConvert.DeserializeObject<ProductItemGetDto>(content);
                List<VariationCategoryGetDto> variationCategories = JsonConvert.DeserializeObject<List<VariationCategoryGetDto>>(contentVariationCategory);
                List<VariationOptionSelectDto> variationOptions = JsonConvert.DeserializeObject<List<VariationOptionSelectDto>>(contentVariationOption);
                List<ProductItemVariationGetDto> productItemVariations = JsonConvert.DeserializeObject<List<ProductItemVariationGetDto>>(contentProductItemVariation);
                List<ProductItemVariationGetDto> mainDto = new List<ProductItemVariationGetDto>();
                foreach (var item in productItem.ProductItemVariationIds)
                {
                    mainDto.Add(productItemVariations.FirstOrDefault(x => x.Id == item));   
                }
                List< int> mainVariationOptions = new List<int>();
                foreach (var item in mainDto)
                {
                    mainVariationOptions.Add(variationOptions.FirstOrDefault(x=>x.Id == item.VariationOptionId ).Id);
                }
                ProductItemViewModel productItemVM = new ProductItemViewModel
                {
                    //Products = items,
                    PostDto = new ProductItemPostDto {ProductName = productItem.ProductName, CostPrice = productItem.CostPrice, Count = productItem.Count,
                        ProductId = productItem.ProductId, SalePrice = productItem.SalePrice, VariationOptionIds = mainVariationOptions },
                    VariationCategories = variationCategories.Where(x=>x.CategoryName == productItem.CategoryName).ToList(),
                    VariationOptions = variationOptions.Where(x=>x.CategoryName==productItem.CategoryName).ToList()

                };
                return View(productItemVM);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductItemPostDto postDto)
        {
            #region modelstate
            //if (!ModelState.IsValid)
            //{
            //    string endpointProduct = "https://localhost:7067/api/admin/products/all/";
            //    endpoint = "https://localhost:7067/api/admin/productitems/" + id;
            //    HttpResponseMessage responseProducts = new();
            //    using (HttpClient client = new HttpClient())
            //    {
            //        responseProducts = await client.GetAsync(endpointProduct);
            //        response = await client.GetAsync(endpoint);
            //    }
            //    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK &&
            //        responseProducts.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        string contentProduct = await responseProducts.Content.ReadAsStringAsync();
            //        string contentPut = await response.Content.ReadAsStringAsync();
            //        List<ProductSelectDto> items = JsonConvert.DeserializeObject<List<ProductSelectDto>>(contentProduct);
            //        ProductItemGetDto productItem = JsonConvert.DeserializeObject<ProductItemGetDto>(contentPut);

            //        ProductItemViewModel productItemVM = new ProductItemViewModel
            //        {
            //            Products = items,
            //            PostDto = new ProductItemPostDto { CostPrice = productItem.CostPrice, Count = productItem.Count, ProductId = productItem.ProductId, SalePrice = productItem.SalePrice }
            //        };
            //        return View(productItemVM);
            //    }

            //}
            #endregion

            endpoint = "https://localhost:7067/api/admin/productitems/" + id;
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                response = await client.PutAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string endpointProductItem = "https://localhost:7067/api/admin/productitems/all";
                HttpResponseMessage responseProductItems = new();
                using (HttpClient client = new HttpClient())
                {
                    responseProductItems = await client.GetAsync(endpointProductItem);
                }
                if (responseProductItems.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    foreach (var item in postDto.VariationOptionIds)
                    {
                        var productItemVariation = new ProductItemVariationPostDto
                        {
                            VariationOptionId = item,
                            ProductItemId = id
                        };
                        string endpointproductItemVariation = "https://localhost:7067/api/admin/productitemvariations/";
                        HttpResponseMessage responseproductItemVariation = null;
                        StringContent contentproductItemVariation = new StringContent(JsonConvert.SerializeObject(productItemVariation), Encoding.UTF8, "application/json");
                        using (HttpClient client = new HttpClient())
                        {
                            responseproductItemVariation = await client.PostAsync(endpointproductItemVariation, contentproductItemVariation);
                        }
                        if (responseproductItemVariation == null && responseproductItemVariation.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            return RedirectToAction("error", "home");
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
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
        public async Task<IActionResult> Detail(int id)
        {
            //getsin productitemvariationlari getirsin onlardan uygunlari secsn sonra variationoptionlari yeni valuelari ve variation adlarini getirsin
            endpoint = "https://localhost:7067/api/admin/productitems/" + id;
            string endpointVariationOption = "https://localhost:7067/api/admin/variationoptions/all";
            string endpoinProductItemVariation = "https://localhost:7067/api/admin/productitemvariations/all";
            HttpResponseMessage responseVariationOption = null;
            HttpResponseMessage responseProductItemVariation = null;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
                responseVariationOption = await client.GetAsync(endpointVariationOption);
                responseProductItemVariation = await client.GetAsync(endpoinProductItemVariation);
            }
            if (response!=null && response.StatusCode == System.Net.HttpStatusCode.OK &&
                responseVariationOption != null && responseVariationOption.StatusCode == System.Net.HttpStatusCode.OK &&
                responseProductItemVariation != null && responseProductItemVariation.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                string contentVariationOption = await responseVariationOption.Content.ReadAsStringAsync();
                string contentProductItemVariation = await responseProductItemVariation.Content.ReadAsStringAsync();

                ProductItemGetDto getDto = JsonConvert.DeserializeObject<ProductItemGetDto>(content);
                List<VariationOptionSelectDto> variationOptions = JsonConvert.DeserializeObject<List<VariationOptionSelectDto>>(contentVariationOption);
                List<ProductItemVariationGetDto> productItemVariations = JsonConvert.DeserializeObject<List<ProductItemVariationGetDto>>(contentProductItemVariation);
                List<ProductItemVariationGetDto> mainProductItemVariations = new List<ProductItemVariationGetDto>();
                foreach (var item in getDto.ProductItemVariationIds)
                {
                    mainProductItemVariations.Add(productItemVariations.FirstOrDefault(x => x.Id == item));
                }
                List<VariationOptionSelectDto> mainVariationOptions = new List<VariationOptionSelectDto>();
                foreach (var item in mainProductItemVariations)
                {
                    mainVariationOptions.Add(variationOptions.FirstOrDefault(x => x.Id == item.VariationOptionId));
                }
                ProductItemDetailViewModel viewModel = new ProductItemDetailViewModel
                {
                    VariationOptions = mainVariationOptions,
                    GetDto = getDto
                };
                return View(viewModel);
            }
            return RedirectToAction("error", "home");
        }
    }
}
