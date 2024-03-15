using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class AddCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
