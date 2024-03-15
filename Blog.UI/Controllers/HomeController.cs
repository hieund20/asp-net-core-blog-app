using Blog.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Blog.UI.Models.DTO;

namespace Blog.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();

            //Get Post List
            List<PostDto> response = new List<PostDto>();
            try
            {
                var httpResponseMessage = await client.GetAsync($"https://localhost:7203/api/Posts");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PostDto>>());

            }
            catch (Exception ex)
            {

            }

            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
