using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Tello.Api.Test.DTOs.User;
using Tello.Api.Test.DTOs.User.Role;

namespace Tello.Api.Test.Controllers
{
    public class UsersController : Controller
    {
        HttpResponseMessage response = new();
        string endpoint = string.Empty;
        
        public async Task<IActionResult> Index()
        {
            endpoint = "https://localhost:7067/api/admin/users/Loggeduser";
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserGetDto getDto = JsonConvert.DeserializeObject<UserGetDto>(content);
                return View(getDto);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            endpoint = "https://localhost:7067/api/admin/users/login";
            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            using(HttpClient client = new HttpClient())
            {
                response = await client.PostAsync(endpoint, requestContent);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserLoginResponseDto responseToken = JsonConvert.DeserializeObject<UserLoginResponseDto>(content);
                if (responseToken.Token != null)
                {
                    Response.Cookies.Append("AuthToken", responseToken.Token);
                    return RedirectToAction("index", "home");
                }
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Logout()
        {
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies.Delete("AuthToken");
                return RedirectToAction(nameof(Login));
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
       
        //public async Task<IActionResult> ChangePassword()
        //{
        //    //meni 

        //    return View();
        //}
        public async Task<IActionResult> GetAllMembers()
        {
            endpoint = "https://localhost:7067/api/admin/users/getallmembers";
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<UserGetDto> getDto = JsonConvert.DeserializeObject<List<UserGetDto>>(content);
                return View(getDto);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> BlockUser(string id)
        {
            endpoint = "https://localhost:7067/api/admin/users/member/" + id;
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.DeleteAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(GetAllMembers));
            }
            return RedirectToAction("error", "home");

        }

        #region AdminManager
        public async Task<IActionResult> IndexAdmin()
        {
            endpoint = "https://localhost:7067/api/admin/users/getalladmins";
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<UserGetDto> getDto = JsonConvert.DeserializeObject<List<UserGetDto>>(content);
                return View(getDto);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> CreateAdmin()
        {
            endpoint = "https://localhost:7067/api/admin/users/getroles";
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<RoleGetDto> getDto = JsonConvert.DeserializeObject<List<RoleGetDto>>(content);
                ViewBag.Roles = getDto.Where(x=>x.Role != "Member" && x.Role != "Superadmin").ToList();
                return View();
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(UserPostDto postDto)
        {
            endpoint = "https://localhost:7067/api/admin/users/admin";
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.PostAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(IndexAdmin));
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> EditAdmin(string id)
        {
            endpoint = "https://localhost:7067/api/admin/users/getuser/"+id;
            string endpointRoles = "https://localhost:7067/api/admin/users/getroles";
            HttpResponseMessage responseRoles = null;
            
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
                responseRoles = await client.GetAsync(endpointRoles);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentRoles = await responseRoles.Content.ReadAsStringAsync();
                List<RoleGetDto> getRolesDto = JsonConvert.DeserializeObject<List<RoleGetDto>>(contentRoles);
                ViewBag.Roles = getRolesDto.Where(x => x.Role != "Member" && x.Role != "Superadmin").ToList();

                var content = await response.Content.ReadAsStringAsync();
                UserGetDto getDto = JsonConvert.DeserializeObject<UserGetDto>(content);
                UserEditDto postDto = new UserEditDto { Email = getDto.Email, Fullname = getDto.Fullname, RolesIds = getDto.RolesIds };
                return View(postDto);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> EditAdmin(string id, UserEditDto postDto)
        {
            #region modelstate
            //if (!ModelState.IsValid)
            //{
            //    endpoint = "https://localhost:7067/api/admin/users/getuser/" + id;
            //    string endpointRoles = "https://localhost:7067/api/admin/users/getroles";
            //    HttpResponseMessage responseRoles = null;

            //    using (HttpClient client = new HttpClient())
            //    {
            //        if (Request.Cookies["AuthToken"] != null)
            //        {
            //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
            //        }
            //        response = await client.GetAsync(endpoint);
            //        responseRoles = await client.GetAsync(endpointRoles);
            //    }
            //    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        var contentRoles = await responseRoles.Content.ReadAsStringAsync();
            //        List<RoleGetDto> getRolesDto = JsonConvert.DeserializeObject<List<RoleGetDto>>(contentRoles);
            //        ViewBag.Roles = getRolesDto.Where(x => x.Role != "Member" && x.Role != "Superadmin").ToList();

            //        var contentPostDto = await response.Content.ReadAsStringAsync();
            //        UserGetDto getDto = JsonConvert.DeserializeObject<UserGetDto>(contentPostDto);
            //        UserPostDto postUser = new UserPostDto { Email = getDto.Email, Fullname = getDto.Fullname, RolesIds = getDto.RolesIds };
            //        return View(postUser);
            //    }
            //}
            #endregion
            endpoint = "https://localhost:7067/api/admin/users/admin/" + id;
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.PutAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(IndexAdmin));
            }
            return RedirectToAction("error", "home");
        }

        public async Task<IActionResult> DeleteAdmin(string id)
        {
            endpoint = "https://localhost:7067/api/admin/users/admin/" + id;
            using(HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.DeleteAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(IndexAdmin));
            }
            return RedirectToAction("error", "home");
        }
        #endregion


        #region RoleManager
        public async Task<IActionResult> RoleIndex()
        {
            endpoint = "https://localhost:7067/api/admin/users/getroles";
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<RoleGetDto> getDto = JsonConvert.DeserializeObject<List<RoleGetDto>>(content);
                return View(getDto);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RolePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            endpoint = "https://localhost:7067/api/admin/users/createrole";
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.PostAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(RoleIndex));
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> EditRole(string id)
        {

            endpoint = "https://localhost:7067/api/admin/users/" + id;
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                RoleGetDto getDto = JsonConvert.DeserializeObject<RoleGetDto>(content);
                var postDto = new RolePostDto
                {
                    //Id = getDto.Id,
                    Role = getDto.Role
                };
                return View(postDto);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(string id, RolePostDto postDto)
        {
            endpoint = "https://localhost:7067/api/admin/users/role/" + id;
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.PutAsync(endpoint, content);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(RoleIndex));
            }
            return RedirectToAction("error", "home");
        }
        #endregion


        #region LoggedUser
        public async Task<IActionResult> LoggedUser()
        {
            endpoint = "https://localhost:7067/api/admin/users/LoggedUser";
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("error", "home");
        }

        #endregion
    }

}
