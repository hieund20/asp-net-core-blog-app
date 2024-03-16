using Blog.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Blog.UI.Models.DTO;
using Microsoft.Extensions.Options;

namespace Blog.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<ApiSettings> apiSettings;

        public HomeController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            this.httpClientFactory = httpClientFactory;
            this.apiSettings = apiSettings;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();

            //Get Post List
            List<PostDto> response = new List<PostDto>();
            try
            {
                var httpResponseMessage = await client.GetAsync($"{apiSettings.Value.ProductionUrl}/Posts");

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
