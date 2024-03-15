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
    public class CommentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICommentRepository commentRepository;

        public CommentsController(IMapper mapper, ICommentRepository commentRepository)
        {
            this.mapper = mapper;
            this.commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var commentsDomainModel = await commentRepository.GetAllAsync();

            return Ok(mapper.Map<List<CommentDto>>(commentsDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCommentRequestDto addCommentRequest)
        {
            // Map DTO to Domain Model
            var commentDomainModel = mapper.Map<Comment>(addCommentRequest);

            await commentRepository.CreateAsync(commentDomainModel);

            //Map Domain Model to DTO
            return Ok(mapper.Map<CommentDto>(commentDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var commentDomainModel = await commentRepository.GetByIdAsync(id);

            if (commentDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            return Ok(mapper.Map<CommentDto>(commentDomainModel));
        }

        [HttpGet]
        [Route("GetByPostId/{PostId:Guid}")]
        public async Task<IActionResult> GetAllByPostId([FromRoute] Guid PostId)
        {
            var commentsDomainModel = await commentRepository.GetAllByPostIdAsync(PostId);

            return Ok(mapper.Map<List<CommentDto>>(commentsDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateCommentRequestDto updateCommentRequest)
        {
            //Map Dto to Domain model
            var commentDomainModel = mapper.Map<Comment>(updateCommentRequest);

            commentDomainModel = await commentRepository.UpdateAsync(id, commentDomainModel);

            if (commentDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain model to Dto
            return Ok(mapper.Map<CommentDto>(commentDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteCommentDomainModel = await commentRepository.DeleteAsync(id);

            if (deleteCommentDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<CommentDto>(deleteCommentDomainModel));
        }
    }
}
