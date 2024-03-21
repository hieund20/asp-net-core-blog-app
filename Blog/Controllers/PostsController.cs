using AutoMapper;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPostRepository postRepository;

        public PostsController(IMapper mapper, IPostRepository postRepository)
        {
            this.mapper = mapper;
            this.postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] string? sort,
                                                [FromQuery] bool? isAscending,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 1000)
        {
            var postsDomainModel = await postRepository.GetAllAsync(filterOn, filterQuery, sort, isAscending ?? true, pageNumber, pageSize);

            return Ok(mapper.Map<List<PostDto>>(postsDomainModel));
        }

        [HttpGet]
        [Route("Total")]
        public async Task<IActionResult> GetTotal()
        {
            try
            {
                int totalPost = await postRepository.GetTotalAsync();
                return Ok(totalPost);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPostRequestDto addPostRequest)
        {
            // Map DTO to Domain Model
            var postDomainModel = mapper.Map<Post>(addPostRequest);

            await postRepository.CreateAsync(postDomainModel);

            //Map Domain Model to DTO
            return Ok(mapper.Map<PostDto>(postDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var postDomainModel = await postRepository.GetByIdAsync(id);

            if (postDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            return Ok(mapper.Map<PostDto>(postDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdatePostRequestDto updatePostRequest)
        {
            //Map Dto to Domain model
            var postDomainModel = mapper.Map<Post>(updatePostRequest);

            postDomainModel = await postRepository.UpdateAsync(id, postDomainModel);

            if (postDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain model to Dto
            return Ok(mapper.Map<PostDto>(postDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletePostDomainModel = await postRepository.DeleteAsync(id);

            if (deletePostDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<PostDto>(deletePostDomainModel));
        }
    }
}
