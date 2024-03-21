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
    public class PostTagsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPostTagRepository postTagRepository;

        public PostTagsController(IMapper mapper, IPostTagRepository postTagRepository)
        {
            this.mapper = mapper;
            this.postTagRepository = postTagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var postTagsDomainModel = await postTagRepository.GetAllAsync();

            return Ok(mapper.Map<List<PostTagDto>>(postTagsDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPostTagRequestDto addPostTagRequest)
        {
            // Map DTO to Domain Model
            var postTagDomainModel = mapper.Map<PostTag>(addPostTagRequest);

            await postTagRepository.CreateAsync(postTagDomainModel);

            //Map Domain Model to DTO
            return Ok(mapper.Map<PostTagDto>(postTagDomainModel));
        }

        [HttpGet]
        [Route("{postId:Guid}/{tagId:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid postId, [FromRoute] Guid tagId)
        {
            var postTagDomainModel = await postTagRepository.GetByIdAsync(postId, tagId);

            if (postTagDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            return Ok(mapper.Map<PostTagDto>(postTagDomainModel));
        }

        [HttpGet]
        [Route("{postId:Guid}")]
        public async Task<IActionResult> GetAllByPostId([FromRoute] Guid postId)
        {
            var postTagsDomainModel = await postTagRepository.GetAllByPostIdAsync(postId);

            return Ok(mapper.Map<List<PostTagDto>>(postTagsDomainModel));
        }

        [HttpPut]
        [Route("{postId:Guid}/{tagId:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromRoute] Guid tagId, UpdatePostTagRequestDto updatePostTagRequest)
        {
            //Map Dto to Domain model
            var postTagDomainModel = mapper.Map<PostTag>(updatePostTagRequest);

            postTagDomainModel = await postTagRepository.UpdateAsync(postId, tagId, postTagDomainModel);

            if (postTagDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain model to Dto
            return Ok(mapper.Map<PostTagDto>(postTagDomainModel));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid postId, [FromRoute] Guid tagId)
        {
            var deletePostTagDomainModel = await postTagRepository.DeleteAsync(postId, tagId);

            if (deletePostTagDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<PostTagDto>(deletePostTagDomainModel));
        }
    }
}
