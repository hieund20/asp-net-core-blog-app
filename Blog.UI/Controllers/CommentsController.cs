using Blog.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Blog.UI.Models.DTO;

namespace Blog.UI.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CommentsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
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
                RequestUri = new Uri("https://localhost:7203/api/Comments"),
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

                var httpResponseMessage = await client.GetAsync($"https://localhost:7203/api/Comments/GetByPostId/{PostId}");

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
