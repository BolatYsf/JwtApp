using JwtApp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace JwtApp.Mvc.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
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

                var response = await client.GetAsync("https://localhost:7081/api/products");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<ProductListVM>>(jsonData, new JsonSerializerOptions
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

                var response = await client.DeleteAsync($"https://localhost:7081/api/products/{id}");



            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateProductVM();

            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"https://localhost:7081/api/categories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    var data = JsonSerializer.Deserialize<List<CategoryListVM>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    //CategoryListVM deki proplarının ısımlerini geciyorum
                    model.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data, "Id", "Definition");

                    return View(model);
                }

            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {

            var data = TempData["Categories"]?.ToString();

            if (data != null)
            {
                //hatalı bir kayıtta tekrar selectlist dolduruyorun
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);

                productVM.Categories = new SelectList(categories, "Value", "Text");
            }

            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

                if (token != null)
                {
                    var client = _httpClientFactory.CreateClient();

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var jsonData = JsonSerializer.Serialize(productVM);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://localhost:7081/api/products", content);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                    ModelState.AddModelError("", "Error");
                }
            }

            return View(productVM);
        }

        public async Task<IActionResult> Update(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            var model = new UpdateProductVM();

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();


                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);



                var responseProduct = await client.GetAsync($"https://localhost:7081/api/products/{id}");

                if (responseProduct.IsSuccessStatusCode)
                {
                    var jsonData = await responseProduct.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<UpdateProductVM>(jsonData, new JsonSerializerOptions
                    {
                        //proplarım camelcase oldugu ıcın jsondata da prop eslestiriyorum 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });


                    var responseCategory = await client.GetAsync("https://localhost:7081/api/categories");

                    if (responseCategory.IsSuccessStatusCode)
                    {
                        var jsonDataCategory = await responseCategory.Content.ReadAsStringAsync();

                        var data = JsonSerializer.Deserialize<List<CategoryListVM>>(jsonDataCategory, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });

                        //CategoryListVM deki proplarının ısımlerini geciyorum
                        if (result != null)
                        {
                            result.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data, "Id", "Definition");
                        }
                    }


                    return View(result);
                }
            }

            return RedirectToAction("List");

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVM productVM)
        {

            var data = TempData["Categories"]?.ToString();

            if (data != null)
            {
                //hatalı bir kayıtta tekrar selectlist dolduruyorun
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);

                productVM.Categories = new SelectList(categories, "Value", "Text",productVM.CategoryId);
            }

            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;



                if (token != null)
                {
                    var client = _httpClientFactory.CreateClient();


                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var jsonData = JsonSerializer.Serialize(productVM);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync("https://localhost:7081/api/products", content);

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

            return View(productVM);

        }
    }
}
