using JwtApp.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace JwtApp.Mvc.Controllers
{
    public class AccountController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View(new UserLoginVM());
        }

        [HttpPost]

        public async Task<IActionResult> Login(UserLoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var content = new StringContent(JsonSerializer.Serialize(loginVM), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:7081/api/Auth/Login", content);

                if (response.IsSuccessStatusCode)
                {

                    var jsonData = await response.Content.ReadAsStringAsync();

                    var tokenModel = JsonSerializer.Deserialize<JwtResponseVM>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    if (tokenModel != null)
                    {
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        //tokenvm token okuyorum
                        var token = handler.ReadJwtToken(tokenModel.Token);

                        var claims = token.Claims.ToList();

                        if (tokenModel.Token != null)
                        {
                            
                            claims.Add(new Claim("accessToken", tokenModel.Token));
                        }
                   
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            //token expire süresince client hatırlar
                            IsPersistent = true,
                        };


                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                        return RedirectToAction("Index", "Home");

                    }


                }
                else
                {
                    ModelState.AddModelError("", "Username-Password invalid");
                }

            }
            return View(loginVM);
        }
    }
}
