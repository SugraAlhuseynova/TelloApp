using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Category;
using Tello.Api.Test.DTOs.Variation;
using Tello.Api.Test.DTOs.VariationCategory;
using Tello.Api.Test.DTOs.VariationOption;
using Tello.Api.Test.ViewModels.VariationCategory;
using Tello.Api.Test.ViewModels.VariationOption;

namespace Tello.Api.Test.Controllers
{
    public class VariationOptionsController : Controller
    {
        string endpoint = null;
        public async Task<IActionResult> Index(int page)
        {
            HttpResponseMessage response = null;
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/variationoptions/all/" + page;
            using (var client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<VariationOptionListDto> items = JsonConvert.DeserializeObject<PaginatedListDto<VariationOptionListDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> GetAllDeleted(int page)
        {
            HttpResponseMessage response = null;
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/variationoptions/all/deleted" + page;
            using (var client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<VariationOptionListDto> items = JsonConvert.DeserializeObject<PaginatedListDto<VariationOptionListDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = null;
            endpoint = "https://localhost:7067/api/admin/variationoptions/" + id;

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
        public async Task<IActionResult> Restore(int id)
        {
            HttpResponseMessage response = null;
            endpoint = "https://localhost:7067/api/admin/variationoptions/restore/" + id;

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
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage responseAllVariationCategories = null;

            string endpointVariationCategories = "https://localhost:7067/api/admin/variations/all";

            using (var client = new HttpClient())
            {
                responseAllVariationCategories = await client.GetAsync(endpointVariationCategories);
            }
            if (responseAllVariationCategories.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentVariationCategories = await responseAllVariationCategories.Content.ReadAsStringAsync();

                VariationOptionsViewModel viewModel = new VariationOptionsViewModel
                {
                    VariationCategories = JsonConvert.DeserializeObject<List<VariationCategoryGetDto>>(contentVariationCategories),
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
    }
}
