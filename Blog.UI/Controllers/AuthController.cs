using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Blog.UI.Models.DTO;

namespace Blog.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7203/api/Auth/Login"),
                Content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode(); //200 or 201

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<LoginResponseDto>();

            if (response is not null)
            {
                var httpUserResponseMessage = await client.GetFromJsonAsync<IdentityUser>($"https://localhost:7203/api/Users/GetUserFromToken?jwtToken={response.JwtToken}");

                if (httpUserResponseMessage is not null)
                {
                    HttpContext.Session.SetString("jwtToken", response.JwtToken.ToString());
                    HttpContext.Session.SetString("CurrentUserName", httpUserResponseMessage.UserName.ToString());
                    HttpContext.Session.SetString("CurrentUserEmail", httpUserResponseMessage.Email.ToString());
                    HttpContext.Session.SetString("CurrentUserId", httpUserResponseMessage.Id.ToString());


                    //Call API lấy role data từ user, lưu trong Session
                    var httpRolesResponseMessage = await client.GetFromJsonAsync<List<string>>($"https://localhost:7203/api/Users/GetRolesFromToken?jwtToken={response.JwtToken}");
                    if (httpRolesResponseMessage is not null)
                    {
                        HttpContext.Session.SetString("CurrentUserRoles", JsonSerializer.Serialize(httpRolesResponseMessage));
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            return View("Login Faild");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
