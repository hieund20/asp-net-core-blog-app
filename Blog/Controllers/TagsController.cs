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
    public class TagsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITagRepository tagRepository;

        public TagsController(IMapper mapper, ITagRepository tagRepository)
        {
            this.mapper = mapper;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tagsDomainModel = await tagRepository.GetAllAsync();

            return Ok(mapper.Map<List<TagDto>>(tagsDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddTagRequestDto addTagRequest)
        {
            // Map DTO to Domain Model
            var tagDomainModel = mapper.Map<Tag>(addTagRequest);

            await tagRepository.CreateAsync(tagDomainModel);

            //Map Domain Model to DTO
            return Ok(mapper.Map<TagDto>(tagDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var tagDomainModel = await tagRepository.GetByIdAsync(id);

            if (tagDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            return Ok(mapper.Map<TagDto>(tagDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateTagRequestDto updateTagRequest)
        {
            //Map Dto to Domain model
            var tagDomainModel = mapper.Map<Tag>(updateTagRequest);

            tagDomainModel = await tagRepository.UpdateAsync(id, tagDomainModel);

            if (tagDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain model to Dto
            return Ok(mapper.Map<TagDto>(tagDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteTagDomainModel = await tagRepository.DeleteAsync(id);

            if (deleteTagDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<TagDto>(deleteTagDomainModel));
        }
    }
}
