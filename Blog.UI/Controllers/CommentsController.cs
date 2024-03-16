using Blog.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Blog.UI.Models.DTO;
using Microsoft.Extensions.Options;

namespace Blog.UI.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<ApiSettings> apiSettings;

        public CommentsController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            this.httpClientFactory = httpClientFactory;
            this.apiSettings = apiSettings;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{apiSettings.Value.ProductionUrl}/Comments"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode(); //200 or 201

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<CommentDto>();

            if (response is not null)
            {
                return RedirectToAction("Detail", "Posts", new { id = model.PostId});
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListByPostId([FromQuery] Guid PostId)
        {
            List<CommentDto> response = new List<CommentDto>();
            try
            {
                //Get All Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync($"{apiSettings.Value.ProductionUrl}/Comments/GetByPostId/{PostId}");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<CommentDto>>());

            }
            catch (Exception ex)
            {

            }

            return View(response);
        }
    }
}
