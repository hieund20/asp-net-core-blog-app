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

        public async Task<IActionResult> Index(int page = 1)
        {
            var client = httpClientFactory.CreateClient();

            //Get Product Total Count
            PaginationModel pagination = new PaginationModel();
            try
            {
                var totalResponse = await client.GetFromJsonAsync<int>($"{apiSettings.Value.ProductionUrl}/Posts/Total");
                pagination.TotalPage = (int)Math.Ceiling((decimal)totalResponse / 6);
            }
            catch (Exception ex)
            {

            }

            //Set Current Page of Pagination
            if (page == 0)
            {
                pagination.CurrentPage = 1;
            }
            else if (page >= pagination.TotalPage)
            {
                pagination.CurrentPage = pagination.TotalPage;
            }

            //Get Post List
            List<PostDto> response = new List<PostDto>();
            try
            {
                var httpResponseMessage = await client.GetAsync($"{apiSettings.Value.ProductionUrl}/Posts?pageNumber={pagination.CurrentPage}&pageSize=6");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PostDto>>());

            }
            catch (Exception ex)
            {

            }

            //Get Post's PostTags
            try
            {
                foreach (var post in response)
                {
                    List<PostTagDto> postTagsResponse = new List<PostTagDto>();
                    var httpResponseMessage = await client.GetAsync($"{apiSettings.Value.ProductionUrl}/PostTags/{post.PostId}");
                    httpResponseMessage.EnsureSuccessStatusCode();
                    postTagsResponse.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PostTagDto>>());

                    post.PostTags = postTagsResponse;
                }
            }
            catch (Exception ex)
            {

            }

            var viewData = new HomeViewModel<List<PostDto>, PaginationModel>
            {
                Posts = response,
                Pagination = pagination
            };

            return View(viewData);
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
