using Blog.UI.Models;
using Blog.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;

namespace Blog.UI.Controllers
{
    public class PostTagsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<ApiSettings> apiSettings;

        public PostTagsController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            this.httpClientFactory = httpClientFactory;
            this.apiSettings = apiSettings;
        }

        public async Task<IActionResult> Index()
        {
            List<PostTagDto> response = new List<PostTagDto>();
            try
            {
                //Get All Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync($"{apiSettings.Value.ProductionUrl}/PostTags");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PostTagDto>>());
            }
            catch (Exception ex)
            {

            }

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var client = httpClientFactory.CreateClient();

            List<PostDto> postsResponse = new List<PostDto>();
            try
            {
                var httpResponseMessage = await client.GetAsync($"{apiSettings.Value.ProductionUrl}/Posts");

                httpResponseMessage.EnsureSuccessStatusCode();

                postsResponse.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PostDto>>());
            }
            catch (Exception ex)
            {

            }

            List<TagDto> tagsResponse = new List<TagDto>();
            try
            {
                var httpResponseMessage = await client.GetAsync($"{apiSettings.Value.ProductionUrl}/Tags");

                httpResponseMessage.EnsureSuccessStatusCode();

                tagsResponse.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<TagDto>>());
            }
            catch (Exception ex)
            {

            }

            var postTagViewData = new AddPostTagViewModel
            {
                Posts = postsResponse,
                Tags = tagsResponse
            };

            return View(postTagViewData);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPostTagViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var postTag = new PostTagDto
            {
                PostId = model.SelectedPostId,
                TagId = model.SelectedTagId
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{apiSettings.Value.ProductionUrl}/PostTags"),
                Content = new StringContent(JsonSerializer.Serialize(postTag), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode(); //200 or 201

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<PostTagDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "PostTags");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<PostTagDto>($"{apiSettings.Value.ProductionUrl}/PostTags/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{apiSettings.Value.ProductionUrl}/Tags/{request.TagId}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<TagDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Tags");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TagDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"{apiSettings.Value.ProductionUrl}/Tags/{request.TagId}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Tags");

            }
            catch (Exception ex)
            {
                //Console
            }

            return View("Edit");
        }
    }
}
