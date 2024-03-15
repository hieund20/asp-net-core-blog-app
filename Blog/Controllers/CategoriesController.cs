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
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoriesDomainModel = await categoryRepository.GetAllAsync();

            return Ok(mapper.Map<List<CategoryDto>>(categoriesDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto addCategoryRequest)
        {
            // Map DTO to Domain Model
            var categoryDomainModel = mapper.Map<Category>(addCategoryRequest);

            await categoryRepository.CreateAsync(categoryDomainModel);

            //Map Domain Model to DTO
            return Ok(mapper.Map<CategoryDto>(categoryDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var categoryDomainModel = await categoryRepository.GetByIdAsync(id);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            return Ok(mapper.Map<CategoryDto>(categoryDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateCategoryRequestDto updateCategoryRequest)
        {
            //Map Dto to Domain model
            var categoryDomainModel = mapper.Map<Category>(updateCategoryRequest);

            categoryDomainModel = await categoryRepository.UpdateAsync(id, categoryDomainModel);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain model to Dto
            return Ok(mapper.Map<CategoryDto>(categoryDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteCategoryDomainModel = await categoryRepository.DeleteAsync(id);

            if (deleteCategoryDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<CategoryDto>(deleteCategoryDomainModel));
        }
    }
}
