using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Category;
using Tello.Api.Test.DTOs.Variation;
using Tello.Api.Test.DTOs.VariationCategory;
using Tello.Api.Test.ViewModels.VariationCategory;

namespace Tello.Api.Test.Controllers
{
    public class VariationCategoriesController : Controller
    {
        string endpoint = null;
        public async Task<IActionResult> Index(int page)
        {
            HttpResponseMessage response = null;
            if (page == 0)
                page = 1;

            endpoint = "https://localhost:7067/api/admin/variationcategories/all/" + page;
            using (var client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<VariationCategoryListDto> items = JsonConvert.DeserializeObject<PaginatedListDto<VariationCategoryListDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, VariationCategoryPostDto postDto)
        {
            HttpResponseMessage response = null;
            endpoint = "https://localhost:7067/api/admin/variationcategories/" + id;
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                response = await client.PutAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage responseAllCategories = null;
            HttpResponseMessage responseAllVariations = null;
            HttpResponseMessage responseMessage = null;
            string endpointCategory = "https://localhost:7067/api/admin/categories/all";
            string endpointVariation = "https://localhost:7067/api/admin/variations/all";
            endpoint = "https://localhost:7067/api/admin/variationcategories/" + id;
            using (var client = new HttpClient())
            {
                responseAllCategories = await client.GetAsync(endpointCategory);
                responseAllVariations = await client.GetAsync(endpointVariation);
                responseMessage = await client.GetAsync(endpoint);
            }
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK && responseAllVariations.StatusCode == System.Net.HttpStatusCode.OK
                && responseAllCategories.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentVariation = await responseAllVariations.Content.ReadAsStringAsync();
                var contentCategories = await responseAllCategories.Content.ReadAsStringAsync();
                var content = await responseMessage.Content.ReadAsStringAsync();

                VariationCategoryGetDto getDto = JsonConvert.DeserializeObject<VariationCategoryGetDto>(content);
                var postDto = new VariationCategoryPostDto { CategoryId = getDto.CategoryId, VariationId = getDto.VariationId };
                VariationCategoryViewModel viewModel = new VariationCategoryViewModel
                {
                    Variations = JsonConvert.DeserializeObject<List<VariationGetDto>>(contentVariation),
                    Categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(contentCategories),
                    PostDto = postDto
                };
                return View(viewModel);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage responseAllCategories = null;
            HttpResponseMessage responseAllVariations = null;

            string endpointCategory = "https://localhost:7067/api/admin/categories/all";
            string endpointVariation = "https://localhost:7067/api/admin/variations/all";

            using (var client = new HttpClient())
            {
                responseAllCategories = await client.GetAsync(endpointCategory);
                responseAllVariations = await client.GetAsync(endpointVariation);
            }
            if (responseAllVariations.StatusCode == System.Net.HttpStatusCode.OK && responseAllCategories.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentVariation = await responseAllVariations.Content.ReadAsStringAsync();
                var contentCategories = await responseAllCategories.Content.ReadAsStringAsync();

                VariationCategoryViewModel viewModel = new VariationCategoryViewModel
                {
                    Variations = JsonConvert.DeserializeObject<List<VariationGetDto>>(contentVariation),
                    Categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(contentCategories)
                };
                return View(viewModel);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Create(VariationCategoryPostDto postDto)
        {
            HttpResponseMessage responseMessage = null;
            endpoint = "https://localhost:7067/api/admin/variationcategories/";
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                responseMessage = await client.PostAsync(endpoint, content);
            }
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = null;
            endpoint = "https://localhost:7067/api/admin/variationcategories/"+id;

            using(HttpClient client = new HttpClient())
            {
                response = await client.DeleteAsync(endpoint);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Restore(int id)
        {
            HttpResponseMessage response = null;
            endpoint = "https://localhost:7067/api/admin/variationcategories/restore/"+id;

            using (HttpClient client = new HttpClient())
            {
                response = await client.DeleteAsync(endpoint);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> GetAllDeleted(int page)
        {
            HttpResponseMessage response = null;
            if (page == 0)
                page = 1;

            endpoint = "https://localhost:7067/api/admin/variationcategories/all/deleted/" + page;
            using (var client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<VariationCategoryListDto> items = JsonConvert.DeserializeObject<PaginatedListDto<VariationCategoryListDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }


    }
}
