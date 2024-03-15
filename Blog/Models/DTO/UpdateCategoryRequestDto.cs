using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class UpdateCategoryRequestDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
