using Microsoft.AspNetCore.Mvc;
using Blog.UI.Models.DTO;
using System.Text.Json;
using System.Text;
using Blog.UI.Models;

namespace Blog.UI.Controllers
{
    public class PostsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public PostsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<PostDto> response = new List<PostDto>();
            try
            {
                //Get All Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7203/api/Posts");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PostDto>>());

            }
            catch (Exception ex)
            {

            }

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var postResponse = await client.GetFromJsonAsync<PostDto>($"https://localhost:7203/api/Posts/{id.ToString()}");

            List<CommentDto> commentsResponse = new List<CommentDto>();
            try
            {
                //Get All Regions from Web API
                var httpResponseMessage = await client.GetAsync($"https://localhost:7203/api/Comments/GetByPostId/{id.ToString()}");

                httpResponseMessage.EnsureSuccessStatusCode();

                commentsResponse.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<CommentDto>>());

            }
            catch (Exception ex)
            {
            }

            var viewData = new PostCommentsView<PostDto, List<CommentDto>>
            {
                PostDetail = postResponse,
                Comments = commentsResponse
            };

            return View(viewData);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPostViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7203/api/Posts"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode(); //200 or 201

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<PostDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Posts");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<PostDto>($"https://localhost:7203/api/Posts/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7203/api/Posts/{request.PostId}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<PostDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Posts");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PostDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7203/api/Posts/{request.PostId}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Posts");

            }
            catch (Exception ex)
            {
                //Console
            }

            return View("Edit");
        }

    }
}
