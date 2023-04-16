using JwtApp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace JwtApp.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("https://localhost:7081/api/categories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<CategoryListVM>>(jsonData, new JsonSerializerOptions
                    {
                        //proplarım camelcase oldugu ıcın jsondata da prop eslestiriyorum 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    return View(result);
                }
            }


            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"https://localhost:7081/api/categories/{id}");



            }
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View(new CreateCategoryVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM categoryVM)
        {

            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                
               
                
                if (token != null)
                {
                    var client = _httpClientFactory.CreateClient();


                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                   
                    var jsonData = JsonSerializer.Serialize(categoryVM);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://localhost:7081/api/categories", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred ");
                    }

                }
            }

            return View(categoryVM);


        }

        public async Task<IActionResult> Update(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;



            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();


                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"https://localhost:7081/api/categories/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<CategoryListVM>(jsonData, new JsonSerializerOptions
                    {
                        //proplarım camelcase oldugu ıcın jsondata da prop eslestiriyorum 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    return View(result);
                }
            }

            return RedirectToAction("List");

        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryListVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;



                if (token != null)
                {
                    var client = _httpClientFactory.CreateClient();


                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var jsonData = JsonSerializer.Serialize(categoryVM);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync("https://localhost:7081/api/categories", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred ");
                    }

                }
            }

            return View(categoryVM);

        }

    }


}
